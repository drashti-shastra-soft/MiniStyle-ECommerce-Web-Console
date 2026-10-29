using System;

namespace MiniStyleWebConsole.Domain
{
    public class Color : IAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HashCode { get; set; }
        public bool IsActive { get; set; } = true;
        public int AuditId { get; set; }
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public string Status { get; set; }
        public string Token { get; set; }
        public bool IsChecked { get; set; }
    }
}
