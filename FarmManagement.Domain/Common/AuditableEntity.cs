namespace FarmManagement.Domain.Common;

public abstract class AuditableEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    // Soft delete
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted { get; set; }
}