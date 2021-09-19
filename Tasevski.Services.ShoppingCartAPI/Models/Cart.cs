
using System.Collections.Generic;

namespace Tasevski.Services.ShoppingCartAPI.Models;
public class Cart
{
    public CartHeader CartHeader { get; set; }
    public IEnumerable<CartDetails> CartDetails { get; set; }
}
