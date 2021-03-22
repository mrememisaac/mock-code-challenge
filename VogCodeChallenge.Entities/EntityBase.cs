namespace VogCodeChallenge.Entities
{
    public class EntityBase : IEntityBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}