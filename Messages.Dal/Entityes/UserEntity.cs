
using System.ComponentModel.DataAnnotations;

namespace Messages.Dal.Entityes
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Length must be less then 50 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Length must be less then 50 characters")]
        public string Nick { get; set; }

        [Required]
        [MaxLength(130, ErrorMessage = "Length must be less then 50 characters")]
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }
    }
}
