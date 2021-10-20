
using System.Collections.Generic;

namespace Tasevski.Web.Models;
public class CartDTO
{
    public CartDTO()
    {
        Count = 1;
    }
    public CartHeaderDTO CartHeader { get; set; }
    public IEnumerable<CartDetailsDTO> CartDetails { get; set; }
    public int Count { get; set; }
}
