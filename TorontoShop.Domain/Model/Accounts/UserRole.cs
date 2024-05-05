using TorontoShop.Domain.Model.BaseEntities;

namespace TorontoShop.Domain.Model.Accounts
{
    public class UserRole:BaseEntity
    {
        #region properties
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        #endregion

        #region relations
        public User User { get; set; }
        public Role Role { get; set; }
        #endregion
    }
}
