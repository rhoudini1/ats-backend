namespace ATS.Contracts.Responses;

public class JsonErrorResponse
{
    public IList<string> Errors { get; set; }

    public JsonErrorResponse(IList<string> errors) => Errors = errors;

    public JsonErrorResponse(string error)
    {
        Errors = [error];
    }
}
