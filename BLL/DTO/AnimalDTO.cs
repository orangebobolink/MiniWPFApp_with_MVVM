namespace BLL.DTO
{
    public class AnimalDTO
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public TypeAnimalDTO? Type { get; set; }
    }
}
