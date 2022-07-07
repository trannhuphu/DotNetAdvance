using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Posts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostID { set; get; }
        public int AuthorID { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        [Required(ErrorMessage = "Title is required!")]
        public string Title { set; get; }

        [Required(ErrorMessage = "Content is required!")]
        public string Content { set; get; }

        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("^[01]$", ErrorMessage = "Please enter status 0 or 1")]
        public int PublishStatus { set; get; }
        public int CategoryID { set; get; }
        public virtual PostCategories PostCategories {set; get;} 
        public virtual AppUsers AppUsers {set; get;} 

    }
}
