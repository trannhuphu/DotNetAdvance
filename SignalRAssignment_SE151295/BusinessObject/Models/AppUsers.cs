using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class AppUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { set; get; }
        public string FullName { set; get; }
        public string Address { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public ICollection<Posts> Posts { set; get; }
    }
}
