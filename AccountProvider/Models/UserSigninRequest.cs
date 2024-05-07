﻿namespace AccountProvider.Models;

public class UserSigninRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}
