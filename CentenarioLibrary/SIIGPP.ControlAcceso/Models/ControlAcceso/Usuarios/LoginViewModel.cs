using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace SIIGPP.ControlAcceso.Models.ControlAcceso.Usuarios
{
    public class LoginViewModel
    {
        [Required] 
        public string usuario { get; set; }
        //[Required]
        public string password { get; set; }
        [Required]
        public  Guid ClaveP { get; set; }

    }
}
