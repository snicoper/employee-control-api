namespace EmployeeControl.Application.Common.Models.Emails;

public record RecoveryPasswordViewModel
{
    public string? SiteName { get; set; }

    public string? CallBack { get; set; }
}
