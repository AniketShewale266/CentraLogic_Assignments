using Library_Management.DTO;
using Library_Management.Entities;

namespace Library_Management.Interface
{
    public interface IUserInterface
    {
        Task<UserEntity> CreateUser(UserEntity user);
        Task<string> LoginUser(UserEntity user);
    }
}
