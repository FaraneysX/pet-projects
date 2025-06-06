using LibraryApp.Domain.Entities;
using LibraryApp.Infrastructure.Data;

namespace LibraryApp.Infrastructure.Repositories;

public class UserRoleRepository(ApplicationDbContext dbContext)
    : BaseRepository<UserEntityRole>(dbContext), IUserRoleRepository
{
}