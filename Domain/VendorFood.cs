using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class VendorFood
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorFoodID { get; set; }
        public int? VendorID { get; set; }
        public int? FoodID { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual Food Food { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
