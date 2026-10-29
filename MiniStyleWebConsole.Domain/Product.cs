using System;

namespace MiniStyleWebConsole.Domain
{
    public class Product : IAuditable
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string ShortInfo { get; set; }
        public int SubCategoryId { get; set; }
        public int? MicroCategoryId { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public string SubCategory { get; set; }
        public string MicroCategory { get; set; }
        public string Status { get; set; }
        public int AuditId { get; set; }
        public string Token { get; set; }
    }
}
