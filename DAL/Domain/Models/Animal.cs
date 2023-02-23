namespace DAL.Domain.Models
{
    public class Animal : EntityBase
    {
        public int TypeId { get; set; }
        public virtual TypeAnimal? Type { get; set; }
        public int Age { get; set; }
    }
}
