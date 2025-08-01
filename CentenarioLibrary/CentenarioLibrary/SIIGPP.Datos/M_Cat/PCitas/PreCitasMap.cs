using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.PRegistro;
using SIIGPP.Entidades.M_Cat.PAtencion;
using SIIGPP.Entidades.M_Cat.PCitas;
using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Datos.M_Cat.PCitas
{
    public class PreCitasMap : IEntityTypeConfiguration<PreCitas>
    {
        public void Configure(EntityTypeBuilder<PreCitas> builder)
        {
            builder.ToTable("PRE_CITAS").HasKey(a => a.IdPCita);
            builder.Property(a => a.IdPCita).HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL").IsRequired();
            builder.Property(a => a.IdPCita).HasDefaultValueSql("newId()");
            builder.Property(a => a.AgenciaId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            builder.Property(a => a.PRegistroId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
        }
    }
}
