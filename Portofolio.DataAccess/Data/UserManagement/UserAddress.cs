using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portofolio.DataAccess.Data.UserManagement
{
    public class UserAddress
    {
        public int Id { get; set; }
        public string AddressLine { get; set; } = string.Empty;
        public string City { get; set; }
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
