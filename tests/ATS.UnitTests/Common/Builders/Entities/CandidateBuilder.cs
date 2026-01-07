using ATS.Domain.Entities;
using Bogus;

namespace ATS.UnitTests.Common.Builders.Entities;

public class CandidateBuilder
{
    private readonly Faker<Candidate> _faker;

    public CandidateBuilder()
    {
        _faker = new Faker<Candidate>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.FullName, f => f.Name.FullName())
            .RuleFor(c => c.Email, f => f.Internet.Email());
    }

    public static CandidateBuilder Init() => new();

    public CandidateBuilder WithId(Guid id)
    {
        _faker.RuleFor(c => c.Id, _ => id);
        return this;
    }

    public Candidate Build() => _faker.Generate();
}
