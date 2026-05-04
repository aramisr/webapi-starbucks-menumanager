namespace Starbucks.MenuManager.API.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}
