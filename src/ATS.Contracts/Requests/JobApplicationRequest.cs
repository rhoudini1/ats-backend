namespace ATS.Contracts.Requests;

public class RegisterJobApplicationRequest
{
    public required Guid CandidateId { get; set; }
    public required Guid JobId { get; set; }
}
