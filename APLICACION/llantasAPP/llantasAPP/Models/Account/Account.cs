using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace llantasAPP.Models.Account
{
    public class Account
    {
        [Display(Name = "Usuario")]
        [Required]
        public string Usuario { get; set; }

        [Display(Name = "Contraseña")]
        [Required]
        public string Contrasena { get; set; }

        [Required]
        [Display(Name = "Oficina")]
        public int Oficina { get; set; }

        public string[] Roles { get; set; }
    }
}