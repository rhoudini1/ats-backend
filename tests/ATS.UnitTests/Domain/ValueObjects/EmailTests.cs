using ATS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.UnitTests.Domain.ValueObjects;

public class EmailTests
{
    [Theory]
    [InlineData("user@example.com")]
    [InlineData("USER@EXAMPLE.COM")]
    [InlineData("user.name@domain.com")]
    [InlineData("user_name@sub.domain.co")]
    [InlineData("user+tag@gmail.com")]
    public void Create_ShouldSucceed_WhenValidEmail(string input)
    {
        var email = Email.Create(input);

        Assert.Equal(input.Trim().ToLower(), email.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_ShouldThrow_WhenNullOrEmpty(string input)
    {
        var exception = Assert.Throws<ArgumentException>(() => Email.Create(input));

        Assert.Equal("Email cannot be null or empty. (Parameter 'value')", exception.Message);
    }

    [Theory]
    [InlineData("user")]
    [InlineData("@mail.com")]
    [InlineData("user@")]
    [InlineData("user@mail")]
    [InlineData("user mail.com")]
    [InlineData("user@mail..com")]
    public void Create_ShouldThrow_WhenInvalidFormat(string input)
    {
        var exception = Assert.Throws<ArgumentException>(() => Email.Create(input));

        Assert.Equal("Invalid email format. (Parameter 'value')", exception.Message);
    }

    [Fact]
    public void Create_ShouldTrimInput()
    {
        var input = "  user@example.com  ";

        var email = Email.Create(input);

        Assert.Equal("user@example.com", email.Value);
    }

    [Fact]
    public void ToString_ShouldReturnEmailValue()
    {
        var email = Email.Create("user@example.com");

        var result = email.ToString();

        Assert.Equal("user@example.com", result);
    }

    [Fact]
    public void ImplicitConversionToString_ShouldReturnValue()
    {
        var email = Email.Create("user@example.com");

        string value = email;

        Assert.Equal("user@example.com", value);
    }

    [Fact]
    public void CompareDifferentEmails_ShouldNotBeEqual()
    {
        var email1 = Email.Create("user1@example.com");
        var email2 = Email.Create("user2@example.com");

        Assert.NotEqual(email1, email2);
    }
}
