namespace CardBrands.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }

    public bool IsDeleted { get; protected set; }

    public DateTimeOffset CreatedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;

    public DateTimeOffset? EditedDate { get; set; }
    public string? EditedBy { get; set; }

    public DateTimeOffset? DeletedDate { get; set; }
    public string? DeletedBy { get; set; }

    public void SetCreated(string createdBy, DateTimeOffset now)
    {
        CreatedDate = now;
        CreatedBy = createdBy;
    }

    public void SetEdited(string editedBy, DateTimeOffset now)
    {
        EditedDate = now;
        EditedBy = editedBy;
    }

    public void MarkAsDeleted(string deletedBy, DateTimeOffset now)
    {
        IsDeleted = true;
        DeletedDate = now;
        DeletedBy = deletedBy;
    }
}
