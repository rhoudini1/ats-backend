namespace ATS.Domain.Exceptions.Base;

public static class ErrorMessages
{
    public static class Errors
    {
        public const string UnknownError = "An unknown error has occurred. Please try again later.";
    }

    public static class Validation
    {
        public const string FullNameRequired = "Full name is required.";
        public const string FullNameTooShort = "Full name must be at least 3 characters long.";
        public const string FullNameTooLong = "Full name cannot exceed 100 characters.";

        public const string EmailRequired = "Email is required.";
        public const string EmailAlreadyRegistered = "This email is already registered.";
        public const string InvalidEmailFormat = "Invalid email format.";
    }
}
