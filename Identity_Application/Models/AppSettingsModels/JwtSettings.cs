﻿namespace Identity_Application.Models.AppSettingsModels;

public class JwtSettings
{
    public double TokenHourExpTime { get; set; } = 6;

    public string Securitykey { get; set; } = string.Empty;

    public string TokenIssuer { get; set; } = string.Empty;
}