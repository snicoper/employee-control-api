namespace EmployeeControl.Infrastructure.Exceptions;

public class EmailSenderException(string message) : Exception(message);
