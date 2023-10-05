namespace EmployeeControl.Application.Features.Localization.Queries.SupportedLocales;

public class SupportedLocalesDto
{
    public ICollection<string> Locales { get; } = new List<string>();
}
