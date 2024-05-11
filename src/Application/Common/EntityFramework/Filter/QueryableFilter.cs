namespace EmployeeControl.Application.Common.EntityFramework.Filter;

public record QueryableFilter(string PropertyName, string RelationalOperator, string Value, string LogicalOperator);
