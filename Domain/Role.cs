using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleID { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string RoleName { get; set; }
        public bool Enabled { get; set; }
        [NotMapped]
        public virtual ICollection<User> Users { get; set; }
    }
}
