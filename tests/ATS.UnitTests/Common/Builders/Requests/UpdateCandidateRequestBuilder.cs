using ATS.Contracts.Requests;
using Bogus;

namespace ATS.UnitTests.Common.Builders.Requests;

public class UpdateCandidateRequestBuilder
{
    private readonly Faker<UpdateCandidateRequest> _faker;

    public UpdateCandidateRequestBuilder()
    {
        _faker = new Faker<UpdateCandidateRequest>("pt_BR")
            .RuleFor(req => req.FullName, f => f.Name.FullName())
            .RuleFor(req => req.Email, (f, req) => f.Internet.Email(req.FullName));
    }

    public static UpdateCandidateRequestBuilder Init() => new();

    public UpdateCandidateRequestBuilder WithFullName(string fullName)
    {
        _faker.RuleFor(r => r.FullName, _ => fullName);
        return this;
    }

    public UpdateCandidateRequestBuilder WithEmail(string email)
    {
        _faker.RuleFor(r => r.Email, _ => email);
        return this;
    }

    public UpdateCandidateRequest Build() => _faker.Generate();
}
