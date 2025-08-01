using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RCitatorioRecordatorio
{
    public class PUT_StatusAsistenciaViewModel
    {
        public Guid IdRCitatoriosRecordatorios { get; set; } 

        public Boolean StatusAsistencia { get; set; }
    }
}
