using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimalVisionAPI.Data;
using OptimalVisionAPI.DTOs;
using OptimalVisionAPI.Models;
using OptimalVisionAPI.Services;

namespace OptimalVisionAPI.Controllers;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IEmailSender _email;

    public AuthController(AppDbContext db, IEmailSender email)
    {
        _db = db;
        _email = email;
    }

    // 1️⃣ SEND OTP
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
    {
        var student = await _db.Student.FirstOrDefaultAsync(x => x.Email == dto.Email);
        if (student == null)
            return BadRequest("Email not found");

        // Generate OTP
        var otp = new Random().Next(100000, 999999).ToString();

        // Save OTP
        var record = new PasswordResetOtp
        {
            Email = dto.Email,
            Otp = otp,
            ExpiryTime = DateTime.UtcNow.AddMinutes(10)
        };

        _db.PasswordResetOtps.Add(record);
        await _db.SaveChangesAsync();

        // Send Email
        await _email.SendAsync(dto.Email, "Your Password Reset Code", $"Your OTP is: {otp}");

        return Ok(new { Message = "OTP sent" });
    }

    // 2️⃣ VERIFY OTP
    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto dto)
    {
        var otpRecord = await _db.PasswordResetOtps
            .Where(x => x.Email == dto.Email && x.Otp == dto.Otp)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        if (otpRecord == null)
            return BadRequest("Invalid OTP");

        if (otpRecord.ExpiryTime < DateTime.UtcNow)
            return BadRequest("OTP expired");

        return Ok(new { Message = "OTP verified" });
    }

    // 3️⃣ RESET PASSWORD
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        var student = await _db.Student.FirstOrDefaultAsync(x => x.Email == dto.Email);
        if (student == null)
            return BadRequest("User not found");

        // Hash password
        student.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

        await _db.SaveChangesAsync();

        return Ok(new { Message = "Password reset successful" });
    }
}
