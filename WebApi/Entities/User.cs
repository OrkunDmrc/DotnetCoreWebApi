
namespace WebApi.Entities
{
    public class User{
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefleshToken { get; set; } = "null";
        public DateTime? RefleshTokenExpireDate { get; set; }
    }
}