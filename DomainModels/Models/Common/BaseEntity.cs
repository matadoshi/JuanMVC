using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModels.Models.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
    }
}
