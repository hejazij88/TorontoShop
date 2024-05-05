using TorontoShop.Domain.Model.BaseEntities;

namespace TorontoShop.Domain.Model.Accounts
{
    public class RolePermission:BaseEntity
    {
        #region properties
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
        #endregion

        #region relations
        public Role Role { get; set; }
        public Permission Permission { get; set; }
        #endregion
    }
}
