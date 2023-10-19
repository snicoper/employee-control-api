namespace EmployeeControl.Application.Common.EntityFramework.OrderBy.Exceptions;

[Serializable]
public class OrderFieldEntityNotFoundException(string name, object key)
    : Exception($"""Entity "{name}" ({key}) was not found for ordering.""");
