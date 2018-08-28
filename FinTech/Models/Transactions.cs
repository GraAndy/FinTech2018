using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinTech.Models
{
    [Table("Transactions")]
    public class Transactions
    {
        [Key]
        [Display(Name = "Идентификатор")]
        public int Id { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "От")]
        public Clients Client { get; set; }

        [Display(Name = "Кому")]
        public Employee Employee { get; set; }

        [Required]
        [Display(Name = "Сумма")]
        public int Money { get; set; }

        [Display(Name = "Начислено бону")]
        public Organization organization { get; set; }
    }
}