namespace EmployeeControl.Application.Common.Exceptions;

public class ConfigurationBadParameterException : Exception
{
    public ConfigurationBadParameterException()
    {
    }

    public ConfigurationBadParameterException(string message)
        : base(message)
    {
    }
}
