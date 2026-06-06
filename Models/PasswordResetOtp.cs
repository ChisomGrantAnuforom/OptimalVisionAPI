namespace OptimalVisionAPI.Models;

public class PasswordResetOtp
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Otp { get; set; } = string.Empty;
    public DateTime ExpiryTime { get; set; }
}
