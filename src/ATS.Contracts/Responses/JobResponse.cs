namespace ATS.Contracts.Responses;

public class JobResponse
{
    public required Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}
