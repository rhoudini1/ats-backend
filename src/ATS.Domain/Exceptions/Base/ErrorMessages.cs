namespace ATS.Domain.Exceptions.Base;

public static class ErrorMessages
{
    public static class Errors
    {
        public const string UnknownError = "An unknown error has occurred. Please try again later.";
        public const string CandidateNotFound = "The informed candidate was not found.";
    }

    public static class Validation
    {
        public const string FullNameRequired = "Full name is required.";
        public const string FullNameTooShort = "Full name must be at least 3 characters long.";
        public const string FullNameTooLong = "Full name cannot exceed 100 characters.";

        public const string EmailRequired = "Email is required.";
        public const string EmailAlreadyRegistered = "This email is already registered.";
        public const string InvalidEmailFormat = "Invalid email format.";
        public const string EmailChangeNotAllowed = "Email change is not allowed.";

        public const string PageNumberNegative = "Page number must be greater than 0.";
        public const string PageSizeNegative = "Page size must be greater than 0.";
        public const string PageSizeExceeded = "Page size must not exceed 100.";
    }

    public static class DatabaseHealth
    {
        public const string Healthy = "Database connection is healthy.";
        public const string Unhealthy = "Database connection is unhealthy.";
        public const string Error = "An error occurred while checking database health.";
        public const string Timeout = "Timeout: database took too long to answer.";
    }
}
