using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Estructura
{
    public class ModuloServicioMap : IEntityTypeConfiguration<ModuloServicio>
    {
        public void Configure(EntityTypeBuilder<ModuloServicio> builder)
        {
            
                builder.ToTable("C_MODULOSERVICIO")
                       .HasKey(a => a.IdModuloServicio);
            builder
             .Property(a => a.IdModuloServicio)
             .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
             .IsRequired();
            builder
             .Property(a => a.AgenciaId)
             .HasColumnType("UNIQUEIDENTIFIER ")
             .IsRequired();
            builder
           .Property(a => a.IdModuloServicio)
           .HasDefaultValueSql("newId()");

        }
    }

}
