using MicroShop.Web.Domain.Interfaces;
namespace MicroShop.Web.Domain.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
