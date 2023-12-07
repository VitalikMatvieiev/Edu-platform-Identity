﻿using Identity_Application.Models;

namespace Identity_Application.Interfaces;

public interface IAuthenticationService
{
    AuthenticationResult Register(string username, string email, string password);

    AuthenticationResult Login(string email, string password);
}