namespace Identity_Application.Models.AppSettingsModels;

public class PasswordHashSettings
{
    public string PasswordHashPepper { get; set; } = string.Empty;

    public int Iteration { get; set; }
}