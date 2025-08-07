using CSharpFunctionalExtensions;
using WallpaperStore.Core.Models;

namespace WallpaperStore.Application.Services
{
    public interface IUserService
    {
        Task<Result<Guid>> CreateAsync(User user, CancellationToken ct = default);
        Task<Result> DeleteAsync(Guid id, CancellationToken ct = default);
        Task<Result<List<User>>> GetAsync();
        Task<Result<User>> GetByIdAsync(Guid id);
        Task<Result<Guid>> UpdateAsync(Guid id, string name, CancellationToken ct = default);
    }
}