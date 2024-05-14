using Microsoft.EntityFrameworkCore;
using TorontoShop.Domain.Interfaces;
using TorontoShop.Domain.Model.Accounts;
using TorontoShop.Domain.ViewModel.Admin.Account;
using TorontoShop.Domain.ViewModel.Paging;
using TorontoShop.Infa.Data.Context;

namespace TorontoShop.Infa.Data.Repository
{
    public class UserRepository: IUserRepository
    {
        #region constractor
        private readonly ShopDbContext _context;
        public UserRepository(ShopDbContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<bool> IsUserPhoneExist(string phone)
        {
            return await _context.Users.AsQueryable().AnyAsync(user => user.PhoneNumber == phone);
        }

        public async Task RegisterUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();

        }

        public async Task<User?> GetUserByPhoneNumber(string phoneNumber)
        {
           return await _context.Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public async Task<User> GetUserById(Guid userId)
        {
            return await _context.Users.AsQueryable()
                .SingleOrDefaultAsync(c => c.Id == userId);
        }

        public async Task<FilterUserViewModel> FilterUsers(FilterUserViewModel filter)
        {
            var query = _context.Users.AsQueryable();


            if (!string.IsNullOrEmpty(filter.PhoneNumber))
            {
                query = query.Where(c => c.PhoneNumber == filter.PhoneNumber);
            }


            #region paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.CountForShowAfterAndBefor);
            var allData = await query.Paging(pager).ToListAsync();
            #endregion

            return filter.SetPaging(pager).SetUsers(allData);
        }

        public async Task<EditUserFromAdmin> GetEditUserFromAdmin(Guid userId)
        {
            return await _context.Users.AsQueryable()
                .Where(c => c.Id == userId)
                .Select(x => new EditUserFromAdmin
                {
                    UserId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    UserGender = x.Gender,
                    RopleId = x.UserRoles.Where(role =>role.UserId==userId ).Select(role =>role.RoleId).ToList()
                }).SingleOrDefaultAsync();
        }

        public async Task<CreateOrEditRoleViewModel> GetEditRoleById(Guid roleId)
        {
            return await _context.Roles.AsQueryable()
                .Include(c => c.RolePermissions)
                .Where(c => c.Id == roleId)
                .Select(x => new CreateOrEditRoleViewModel
                {
                    Id = x.Id,
                    RoleTitle = x.RoleTitle,
                    SelectedPermission = x.RolePermissions.Select(c => c.PermissionId).ToList()

                }).SingleOrDefaultAsync();
        }

        public async Task<FilterRolesViewModel> FilterRoles(FilterRolesViewModel filter)
        {
            var query = _context.Roles.AsQueryable();

            #region filter
            if (!string.IsNullOrEmpty(filter.RoleName))
            {
                query = query.Where(c => EF.Functions.Like(c.RoleTitle, $"%{filter.RoleName}%"));
            }
            #endregion

            #region paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.CountForShowAfterAndBefor);
            var allData = await query.Paging(pager).ToListAsync();
            #endregion

            return filter.SetPaging(pager).SetRoles(allData);
        }

        public async Task CreateRole(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
        }

        public async Task<Role> GetRoleById(Guid id)
        {
            return await _context.Roles.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task RemoveAllPermissionSelectedRole(Guid roleId)
        {
            var allRolePermissions = await _context.RolePermissions.Where(c => c.RoleId == roleId).ToListAsync();

            if (allRolePermissions.Any())
            {
                _context.RolePermissions.RemoveRange(allRolePermissions);
            }
        }

        public async Task AddPermissionToRole(List<Guid> selectedPermission, Guid roleId)
        {
            if (selectedPermission != null && selectedPermission.Any())
            {
                var rolePermissions = new List<RolePermission>();

                foreach (var permissionId in selectedPermission)
                {
                    rolePermissions.Add(new RolePermission
                    {
                        PermissionId = permissionId,
                        RoleId = roleId,

                    });
                }

                await _context.RolePermissions.AddRangeAsync(rolePermissions);
            }
        }

        public async Task<List<Role>> GetAllActiveRoles()
        {
            return await _context.Roles .AsQueryable().Where(role =>!role.IsDeleted ).ToListAsync();
        }

        public async Task<List<Permission>> GetAllActivePermission()
        {
            return await _context.Permissions.AsQueryable().Where(permission => !permission.IsDeleted).ToListAsync();
        }

        public async Task RemoveAllUserSelectedRole(Guid userId)
        {
            var allUserRoles = await _context.UserRoles.AsQueryable().Where(c => c.UserId == userId).ToListAsync();

            if (allUserRoles.Any())
            {
                _context.UserRoles.RemoveRange(allUserRoles);

                await _context.SaveChangesAsync();
            }
        }

        public async Task AddUserToRole(List<Guid> selectedRole, Guid userId)
        {
            if (selectedRole != null && selectedRole.Any())
            {
                var userRoles = new List<UserRole>();

                foreach (var roleId in selectedRole)
                {
                    userRoles.Add(new UserRole
                    {
                        RoleId = roleId,
                        UserId = userId
                    });
                }

                await _context.UserRoles.AddRangeAsync(userRoles);
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<Role>> GetAllActiveUserRole()
        {
            throw new NotImplementedException();
        }

        public bool CheckPermission(Guid permissionId, string phoneNumber)
        {
            Guid userId = _context.Users.AsQueryable().Single(c => c.PhoneNumber == phoneNumber).Id;

            var userRole = _context.UserRoles.AsQueryable()
                .Where(c => c.UserId == userId).Select(r => r.RoleId).ToList();


            if (!userRole.Any())
                return false;


            var permissions = _context.RolePermissions.AsQueryable()
                .Where(c => c.PermissionId == permissionId).Select(c => c.RoleId).ToList();


            return permissions.Any(c => userRole.Contains(c));
        }
    }
}
