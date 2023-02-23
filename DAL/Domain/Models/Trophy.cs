namespace DAL.Domain.Models
{
    public class Trophy : EntityBase
    {
        public int AnimalId { get; set; }
        public virtual Animal? Animal { get; set; }
        public DateTime DateOfMurder { get; set; }
    }
}
