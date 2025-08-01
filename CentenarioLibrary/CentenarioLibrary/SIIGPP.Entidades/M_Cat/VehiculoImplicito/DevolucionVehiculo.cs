using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.Orientacion;

namespace SIIGPP.Entidades.M_Cat.VehiculoImplicito
{
    public class DevolucionVehiculo
    {
        public Guid IdDevolucionVehiculo { get; set; }
        public Guid VehiculoId { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public Boolean NucRelacionadaForaneaTF { get; set; }
        public string NucRelacionadaForanea { get; set; }
        public string NoOficio { get; set; }
        public string DevueltoA { get; set; }
        public string CalidadDevueltoA { get; set; }
        public string TipoDevolucion { get; set; }
        public string NombreCorralon { get; set; }
        public string UbicacionCorralon { get; set; }
        public DateTime FechaIngresoCorralon { get; set; }
        public string DirigidoA { get; set; }
        public string Ccp { get; set; }
        public string Usuario { get; set; }
        public string Puesto { get; set; }
        public string Agencia { get; set; }
        public string Subproc { get; set; }
        public string Distrito { get; set; }
        public Guid UsuarioId { get; set; }
        public Boolean FirmasVoBoTF { get; set; }
        public string FirmasVoBo { get; set; }
        public string TextoDevolucion { get; set; }
        public DateTime Fechasys { get; set; }
    }
}
