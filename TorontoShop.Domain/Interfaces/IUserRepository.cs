using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorontoShop.Domain.Model.Accounts;
using TorontoShop.Domain.ViewModel.Accounts;
using TorontoShop.Domain.ViewModel.Admin.Account;

namespace TorontoShop.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> IsUserPhoneExist(string phone);

        public Task RegisterUser(User user);

        public Task SaveChange();

        public Task <User?> GetUserByPhoneNumber(string phoneNumber);

        public void UpdateUser(User user);

        public Task<User> GetUserById(Guid userId);
        Task<FilterUserViewModel> FilterUsers(FilterUserViewModel filter);
        Task<EditUserFromAdmin> GetEditUserFromAdmin(Guid userId);

        Task<CreateOrEditRoleViewModel> GetEditRoleById(Guid roleId);
        Task<FilterRolesViewModel> FilterRoles(FilterRolesViewModel filter);
        Task CreateRole(Role role);
        void UpdateRole(Role role);
        Task<Role> GetRoleById(Guid id);
        Task RemoveAllPermissionSelectedRole(Guid roleId);
        Task AddPermissionToRole(List<Guid> selectedPermission, Guid roleId);






    }
}
