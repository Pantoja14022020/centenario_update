using SIIGPP.Entidades.M_Cat.VehiculoImplicito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Vehiculos
{
    public class DevolucionVehiculoViewModel
    {
        public Guid IdDevolucionVehiculo { get; set; }
        public Guid VehiculoId { get; set; }
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

    public class CrearDevolucionVehiculoViewModel
    {
        public Guid VehiculoId { get; set; }
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

    public class EditarDevolucionVehiculoViewModel
    {
        public Guid IdDevolucionVehiculo { get; set; }
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
    }
}
