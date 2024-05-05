using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TorontoShop.Domain.Model.BaseEntities;

namespace TorontoShop.Domain.Model.Accounts
{
    public class Permission:BaseEntity
    {
        #region properties
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]

        public string Title { get; set; }
        public Guid? ParentId { get; set; }
        #endregion

        #region relations
        [ForeignKey("ParentId")]
        public ICollection<Permission> Permissions { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}
