namespace EmployeeControl.Application.Common.EntityFramework.Filter;

public class RequestFilter
{
    public string? PropertyName { get; set; }

    public string? RelationalOperator { get; set; }

    public string? Value { get; set; }

    public string? LogicalOperator { get; set; }
}
