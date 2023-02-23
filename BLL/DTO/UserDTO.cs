using DAL.Domain.Models;

namespace BLL.DTO
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public virtual TrophyDTO? Trophy { get; set; }
    }
}
