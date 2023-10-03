namespace EmployeeControl.Application.Features.Culture.Queries.SupportedCultures;

public class SupportedCulturesDto
{
    public ICollection<string> Locales { get; } = new List<string>();
}
