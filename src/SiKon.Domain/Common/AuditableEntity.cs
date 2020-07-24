using System;

namespace SiKon.Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset Modified { get; set; }
        public string ModifiedBy { get; set; }
    }
}