using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName="nvarchar(250")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(250")]
        public string Email { get; set; }
        //[Column(TypeName = "nchar(30")]
        //public string PasswordHash { get; set; }

    }
}
