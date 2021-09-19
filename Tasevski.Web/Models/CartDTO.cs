
using System.Collections.Generic;

namespace Tasevski.Web.Models;
public class CartDTO
{
    public CartHeaderDTO CartHeader { get; set; }
    public IEnumerable<CartDetailsDTO> CartDetails { get; set; }
}
