using CSharpFunctionalExtensions;

namespace WallpaperStore.Core.Models
{
    public class Category : ValueObject
    {
        public const uint MAX_LENGTH = 50;
        public string Value { get; } = string.Empty;

        public Category(string value)
        {
            Value = value;
        }
        public static Result<Category> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Result.Failure<Category>($"Category value is null or empty {nameof(value)}");
            if (value.Length > MAX_LENGTH)
                return Result.Failure<Category>($"Category value length longer than 50 {nameof(value)}");
            return Result.Success(new Category(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value; 
        }
        public override string ToString() => Value;
    }
}
