namespace EmployeeControl.Application.Common.Exceptions;

[Serializable]
public class ConfigurationNullParameterException : Exception
{
    public ConfigurationNullParameterException()
    {
    }

    public ConfigurationNullParameterException(string message)
        : base(message)
    {
    }
}
