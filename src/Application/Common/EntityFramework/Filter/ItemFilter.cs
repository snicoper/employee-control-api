namespace EmployeeControl.Application.Common.EntityFramework.Filter;

public record ItemFilter(string PropertyName, string RelationalOperator, string Value, string LogicalOperator);
