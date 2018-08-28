using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinTech.Models
{
    [Table("Organization")]
    public class Organization
    {
        [Key]
        [Display(Name = "Идентификатор")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        public List<Employee> Employes { get; set; }
        public List<Present> Presents { get; set; }
        public List<Card> Cards { get; set; }

        public Organization()
        {
            Employes = new List<Employee>();
            Presents = new List<Present>();
            Cards = new List<Card>();
        }

    }
}