namespace EmployeeControl.Application.Common.Models.Emails;

public record ValidateEmailRegistrationViewModel
{
    public string? SiteName { get; set; }

    public string? Email { get; set; }

    public string? CompanyName { get; set; }

    public string? UrlValidate { get; set; }
}
