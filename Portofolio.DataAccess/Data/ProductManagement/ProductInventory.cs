using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portofolio.DataAccess.Data.ProductManagement
{
    public class ProductInventory
    {
        public int Id { get; set; }
        public string Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifitedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
