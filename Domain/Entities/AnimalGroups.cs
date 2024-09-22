namespace Domain.Entities
{
    public class AnimalGroups
    {
        public Guid Id { get; set; }
        public Guid AnimalId { get; set; }
        public Guid GroupId { get; set; }
        public virtual Animal? Animal { get; set; }
        public virtual Group? Group { get; set; }
    }
}
