using Shop.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Shop.Core.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByEmailAsync(string email);
}
