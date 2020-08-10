using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApi02.Model
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Display(Name ="Username")]
        [StringLength(20)]
        public string Username { get; set; }
        public string Email { get; set; }
        
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
