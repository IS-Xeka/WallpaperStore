using CSharpFunctionalExtensions;

namespace WallpaperStore.Core.Models;

public class Wallpaper
{
    private const uint MAX_TITLE_LENGTH = 50;
    private const uint MAX_DESCRIPTION_LENGTH = 300;
    public Guid Id { get; private set; }
    public Guid OwnerId { get; private set; }
    public User? Owner { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    private readonly List<Category> _categories = [];
    public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();
    private Wallpaper(Guid id, string title, string description, string url, decimal price, Guid ownerId)
    {
        Id = id; 
        Title = title; 
        Description = description; 
        Url = url; 
        Price = price;
        OwnerId = ownerId;
    }

    public static Result<Wallpaper> Create(Guid id, string title, string description, string url, decimal price, Guid ownerId)
    {
        var errors = new List<string>();

        if(string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
        {
            errors.Add($"Title can not be empty and must be less than {MAX_TITLE_LENGTH} characters");
        }
        if (string.IsNullOrEmpty(description) || description.Length > MAX_DESCRIPTION_LENGTH)
        {
            errors.Add($"Description can not be empty and must be less than {MAX_TITLE_LENGTH} characters");
        }
        if (string.IsNullOrEmpty(url))
        {
            errors.Add($"Url can not be empty");
        }
        if (price < 0)
        {
            errors.Add($"Price can not be below than 0");
        }
        if (errors.Any())
            return Result.Failure<Wallpaper>(string.Join("; ", errors));

        return Result.Success(new Wallpaper(id, title, description, url, price, ownerId));
    }

    public Result AddCategory(Category category)
    {
        if(category == null)
            return Result.Failure($"Category is null. {nameof(category)}");
        if (_categories.Any(c => c.Equals(category)))
            return Result.Failure($"Category has been added. {category}");

        _categories.Add(category);
        return Result.Success();
    }

    public Result RemoveCategory(Category category)
    {
        var removeCtaegory = _categories.FirstOrDefault(c => c.Equals(category));

        if (category == null)
            return Result.Failure($"Category is null. {nameof(category)}");

        _categories.Remove(removeCtaegory);
        return Result.Success();
    }
}
