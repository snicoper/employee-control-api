namespace EmployeeControl.Application.Common.Interfaces;

public interface IRazorViewToStringRendererService
{
    Task<string> RenderViewToStringAsync(string viewName, object model, Dictionary<string, object?> viewData);
}
