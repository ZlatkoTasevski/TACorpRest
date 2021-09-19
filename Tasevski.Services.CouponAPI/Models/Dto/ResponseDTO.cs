
using System.Collections.Generic;

namespace Tasevski.Services.CouponAPI.Models.Dto;
public class ResponseDTO
{
    public bool IsSuccess { get; set; } = true;   
    public object? Result { get; set; }
    public string DisplayMessage { get; set; } = "";
    public List<string>? ErrorMessages { get; set; }
}
