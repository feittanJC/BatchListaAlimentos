using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class OrderDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailID { get; set; }
        public int? OrderID { get; set; }
        public int? VendorFoodID { get; set; }
        public virtual Order Order { get; set; }
        public virtual VendorFood VendorFood { get; set; }
    }
}
