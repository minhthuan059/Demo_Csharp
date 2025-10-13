using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Database.Entity
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }


        [StringLength(1000)]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"{Id} | {Username} | {Message} | {CreatedAt}";
        }
    }

}
