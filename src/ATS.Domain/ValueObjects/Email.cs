using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ATS.Domain.ValueObjects;

public partial record Email
{
    private static readonly Regex BasicEmailRegex = EmailRegex();

    public string Value { get; init; }

    private Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be null or empty.", nameof(value));

        value = value.Trim();

        if (!BasicEmailRegex.IsMatch(value))
            throw new ArgumentException("Invalid email format.", nameof(value));

        Value = value.ToLower();
    }

    public static Email Create(string value) => new(value);

    public override string ToString() => Value;

    public static implicit operator string(Email email) => email.Value;

    [GeneratedRegex(@"^[^@\s]+@([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$", RegexOptions.IgnoreCase | RegexOptions.Compiled)]
    private static partial Regex EmailRegex();
}
