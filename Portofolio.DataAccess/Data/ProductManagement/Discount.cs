﻿

namespace Portofolio.DataAccess.Data.ProductManagement
{
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifitedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}

