using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class User{
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string EmailName { get; set; }
        public string Password { get; set; }
        public string RefleshToken { get; set; }
        public DateTime? RefleshTokenExpireDate { get; set; }
    }
}