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
        public string Title { set; get; }
        public string Content { set; get; }
        public int PublishStatus { set; get; }
        public int CategoryID { set; get; }
        public virtual PostCategories PostCategories {set; get;} 
        public virtual AppUsers AppUsers {set; get;} 

    }
}
