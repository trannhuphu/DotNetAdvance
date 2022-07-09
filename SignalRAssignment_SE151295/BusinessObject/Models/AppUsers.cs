using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class AppUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { set; get; }
        [Required(ErrorMessage = "FullName is required!")]
        [RegularExpression("^[^\\s]+(\\s+[^\\s]+)*$", ErrorMessage ="FullName must not space")]
        public string FullName { set; get; }
        [Required(ErrorMessage = "Address is required!")]
        [RegularExpression("^[^\\s]+(\\s+[^\\s]+)*$", ErrorMessage = "Address must not space")]
        public string Address { set; get; }
        [Required(ErrorMessage = "Email is required!")]
        public string Email { set; get; }
        [Required(ErrorMessage = "Password is required!")]
        public string Password { set; get; }
        [JsonIgnore]
        public ICollection<Posts> Posts { set; get; }
    }
}
