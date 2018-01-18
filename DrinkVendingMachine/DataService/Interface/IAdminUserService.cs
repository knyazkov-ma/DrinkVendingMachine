using DrinkVendingMachine.DTO;

namespace DrinkVendingMachine.DataService.Interface
{
    public interface IAdminUserService
    {
        AdminUserDTO GetById(long id);

        AdminUserDTO GetByName(string name);
    }
}