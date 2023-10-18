namespace EmployeeControl.Application.Common.Models.Emails;

public class ValidateEmailRegistrationViewModel
{
    public string? Code { get; set; }

    public string? Email { get; set; }

    public string? CompanyName { get; set; }

    public string? Url { get; set; }
}
