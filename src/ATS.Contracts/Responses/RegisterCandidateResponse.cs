using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Contracts.Responses;

public class RegisterCandidateResponse
{
    public required Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
