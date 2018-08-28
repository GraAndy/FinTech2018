using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FinTech.Models
{
    [Table("Present")]
    public class Present
    {
        [Key]
        [Display(Name = "Идентификатор")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Стоимость в баллах")]
        public int Cost { get; set; }

        [Display(Name = "Кол-во использований")]
        public int NumberOfUsage { get; set; }

        public Organization organization { get; set; }

    }
}