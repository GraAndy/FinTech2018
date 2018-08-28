using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinTech.Models
{
    [Table("Card")]
    public class Card
    {
        [Key]
        [Display(Name = "Идентификатор")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Идентификатор")]
        public string code { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string password { get; set; }

        [Display(Name = "Организация")]
        public Organization organization { get; set; }

        [Display(Name = "Работник")]
        public Employee employee { get; set; }

    }
}