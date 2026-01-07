using ATS.Contracts.Requests;
using Bogus;

namespace ATS.UnitTests.Common.Builders.Requests;

public class RegisterCandidateRequestBuilder
{
    private readonly Faker<RegisterCandidateRequest> _faker;

    public RegisterCandidateRequestBuilder()
    {
        _faker = new Faker<RegisterCandidateRequest>("pt_BR")
            .RuleFor(req => req.FullName, f => f.Name.FullName())
            .RuleFor(req => req.Email, (f, req) => f.Internet.Email(req.FullName));
    }

    public static RegisterCandidateRequestBuilder Init() => new();

    public RegisterCandidateRequestBuilder WithFullName(string fullName)
    {
        _faker.RuleFor(r => r.FullName, _ => fullName);
        return this;
    }

    public RegisterCandidateRequestBuilder WithEmail(string email)
    {
        _faker.RuleFor(r => r.Email, _ => email);
        return this;
    }

    public RegisterCandidateRequest Build() => _faker.Generate();
}
