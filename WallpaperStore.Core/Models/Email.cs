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

    public static Email Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException($"Email can not be empty {nameof(value)}");
        }
        if(value.Length > MAX_LENGTH)
        {
            throw new ArgumentException($"Email must be less than {MAX_LENGTH} characters");
        }
        if (!Regex.IsMatch(value, EmailRegex) || !new EmailAddressAttribute().IsValid(value))
        {
            throw new ArgumentException("Invalid email format");
        }
        return new Email(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
