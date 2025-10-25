using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        public virtual List<Notification> Notifications { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public override string ToString()
        {
            string notifications = Notifications != null ? string.Join(", ", Notifications.Select(n => n.Message)) : "No Notifications";
            return $"USER: {Id} | {Username} | {Email} | {CreatedAt} | {notifications}";
        }
    }
}