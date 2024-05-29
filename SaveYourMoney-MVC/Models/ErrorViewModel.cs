namespace SaveYourMoney_MVC.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }
    public List<string>? Errors { get; set; } 


    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}

