namespace EmployeeControl.Application.Common.EntityFramework.OrderBy.Exceptions;

[Serializable]
public class OrderFieldEntityNotFoundException : Exception
{
    public OrderFieldEntityNotFoundException(string name, object key)
        : base($"""Entity "{name}" ({key}) was not found for ordering.""")
    {
    }
}
