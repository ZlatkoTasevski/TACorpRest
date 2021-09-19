
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasevski.Services.ShoppingCartAPI.DbContexts;
using Tasevski.Services.ShoppingCartAPI.Models;
using Tasevski.Services.ShoppingCartAPI.Models.Dto;
using Tasevski.Services.ShoppingCartAPI.Repository.IRepository;

namespace Tasevski.Services.ShoppingCartAPI.Repository;
public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _db;
    private IMapper _mapper;

    public CartRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<bool> ApplyCoupon(string userId, string couponCode)
    {
        var cartFromDb = await _db.CartHeader.FirstOrDefaultAsync(u=>u.UserId == userId);
        cartFromDb.CouponCode = couponCode;
        _db.CartHeader.Update(cartFromDb);
        await _db.SaveChangesAsync();
        return true;
    }
    public async Task<bool> RemoveCoupon(string userId)
    {
        var cartFromDb = await _db.CartHeader.FirstOrDefaultAsync(u => u.UserId == userId);
        cartFromDb.CouponCode = "";
        _db.CartHeader.Update(cartFromDb);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ClearCart(string userId)
    {
        var cartHeaderFromDb = await _db.CartHeader.FirstOrDefaultAsync(u => u.UserId == userId);
        if (cartHeaderFromDb != null)
        {
            _db.CartDetails.RemoveRange(_db.CartDetails.Where(u => u.CartHeaderId == cartHeaderFromDb.CartHeaderId));
            _db.CartHeader.RemoveRange(cartHeaderFromDb);
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<CartDTO> CreateUpdateCart(CartDTO cartDTO)
    {
        Cart cart = _mapper.Map<Cart>(cartDTO);

        //prvo proveruvame dali produktot postoi vo baza, ako ne go kreirame!
        var productInDb = await _db.Products.FirstOrDefaultAsync(u => u.ProductId == cartDTO.CartDetails.FirstOrDefault().ProductId);
        if (productInDb == null)
        {
            _db.Products.Add(cart.CartDetails.FirstOrDefault().Product);
            await _db.SaveChangesAsync();
        }
        //proveruvame dali header is null
        var cardHeaderFromDb = await _db.CartHeader.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);
        //var cardHeaderFromDb = await _db.CartHeader.FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);
        if (cardHeaderFromDb == null)
        {
            //ako e null kreirame header i details
            _db.CartHeader.Add(cart.CartHeader);
            await _db.SaveChangesAsync();
            cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
            cart.CartDetails.FirstOrDefault().Product = null;
            _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
            await _db.SaveChangesAsync();
        }
        else
        {
            //ako header ne e null
            //gi proveruvame detalite dali imaat ist produkt
            var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                u.CartHeaderId == cardHeaderFromDb.CartHeaderId);

            if (cartDetailsFromDb == null)
            {
                //kreirame details
                cart.CartDetails.FirstOrDefault().CartHeaderId = cardHeaderFromDb.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null;
                _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                //ako imaat details samo pravime update na Count
                cart.CartDetails.FirstOrDefault().Product = null;
                cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
                cart.CartDetails.FirstOrDefault().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                _db.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
        }
        return _mapper.Map<CartDTO>(cart);
    }

    public async Task<CartDTO> GetCartByUserId(string userId)
    {
        Cart cart = new Cart
        {
            CartHeader = await _db.CartHeader.FirstOrDefaultAsync(u => u.UserId == userId)
        };

        cart.CartDetails = _db.CartDetails.Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId).Include(u => u.Product);

        return _mapper.Map<CartDTO>(cart);
    }

    public async Task<bool> RemoveFromCart(int cartDetailsId)
    {
        try
        {
            CartDetails cartDetails = await _db.CartDetails.FirstOrDefaultAsync(u => u.CartDetailsId == cartDetailsId);

            int totalCountOfCartItems = _db.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();

            _db.CartDetails.Remove(cartDetails);
            if (totalCountOfCartItems == 1)
            {
                var cardHeaderToRemove = await _db.CartHeader.FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);

                _db.CartHeader.Remove(cardHeaderToRemove);
            }
            await _db.SaveChangesAsync();
            return true;
        }
        catch(Exception e)
        {
            var dto = new ResponseDTO
            {
                DisplayMessage = "Error",
                ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                IsSuccess = false
            };
            return false;
        }
    }

}
