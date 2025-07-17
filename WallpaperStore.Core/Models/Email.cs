using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WallpaperStore.Core.Models;

[Owned]
public class Email: ValueObject
{
    public const uint MAX_LENGTH = 250;
    private static readonly string EmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    public string Value { get; set; } = string.Empty;

    public Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string value)
    {
        var errors = new List<string>();

        if (string.IsNullOrEmpty(value))
        {
            errors.Add($"Email can not be empty {nameof(value)}");
        }
        if(value.Length > MAX_LENGTH)
        {
            errors.Add($"Email must be less than {MAX_LENGTH} characters");
        }
        if (!Regex.IsMatch(value, EmailRegex) || !new EmailAddressAttribute().IsValid(value))
        {
            errors.Add("Invalid email format");
        }
        if (errors.Any())
            return Result.Failure<Email>(string.Join("; ", errors));

        return Result.Success(new Email(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
