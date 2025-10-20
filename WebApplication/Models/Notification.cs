using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    [Table("Notifications")]
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [StringLength(1000)]
        public string Message { get; set; }

        public virtual List<User> Users { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public override string ToString()
        {
            string userNames = Users != null ? string.Join(", ", Users.Select(u => u.Username)) : "No Users";
            return $"NOTIFICATION: {Id} | {Message} | {CreatedAt} | {userNames}";
        }
    }
}