namespace EmployeeControl.Application.Common.EntityFramework.Filter;

public record RequestFilter(string PropertyName, string RelationalOperator, string Value, string LogicalOperator);
