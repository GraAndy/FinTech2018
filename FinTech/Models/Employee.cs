﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinTech.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [Display(Name = "Идентификатор")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Отчетсво")]
        public string Patronymic { get; set; }

        [Display(Name = "Кошелек")]
        public Guid Wallet { get; set; }

        public Organization organization { get; set; }

    }
}