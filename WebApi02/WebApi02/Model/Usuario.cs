using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApi02.Model
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Username")]
        [StringLength(20)]
        public string Username { get; set; }
        
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
    }
}
