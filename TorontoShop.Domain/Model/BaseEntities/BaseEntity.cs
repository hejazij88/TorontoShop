using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorontoShop.Domain.Model.BaseEntities
{
    public class BaseEntity
    {
        [Key] public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }=DateTime.Now;
    }
}
