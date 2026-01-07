using ATS.Domain.Exceptions.Base;

namespace ATS.Domain.Exceptions;

public class ErrorOnValidationException : CustomException
{
    public IList<string> Messages { get; set; }

    public ErrorOnValidationException(IList<string> errorMessages)
    {
        Messages = errorMessages;
    }
}
