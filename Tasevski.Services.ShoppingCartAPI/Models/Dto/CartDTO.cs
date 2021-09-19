
using System.Collections.Generic;

namespace Tasevski.Services.ShoppingCartAPI.Models.Dto;
public class CartDTO
{
    public CartHeaderDTO CartHeader { get; set; }
    public IEnumerable<CartDetailsDTO> CartDetails { get; set; }
}
