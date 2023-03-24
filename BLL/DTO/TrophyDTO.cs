namespace BLL.DTO
{
    public class TrophyDTO
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public AnimalDTO? Animal { get; set; }
    }
}
