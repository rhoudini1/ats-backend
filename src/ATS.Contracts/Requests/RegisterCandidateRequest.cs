namespace ATS.Contracts.Requests;

public class RegisterCandidateRequest
{
    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
