namespace w_escolas.Domain._abstractClasses;

public abstract class Entity
{
    public Guid Id { get; private set; }
    public string? CreatedBy { get; private set; }
    public DateTime? CreatedOn { get; private set; }
    public string? EditedBy { get; private set; }
    public DateTime? EditedOn { get; private set; }

    public Entity()
    {
        Id = Guid.NewGuid();
    }
}
