using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_JR.RRegistros;
using SIIGPP.Entidades.M_JR.RSolicitanteRequerido;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_JR.RSolicitanteRequerido
{
    public class RegistroMap : IEntityTypeConfiguration<Registro>
    {
        public void Configure(EntityTypeBuilder<Registro> builder)
        {
            builder.ToTable("JR_REGISTROS")
              .HasKey(a => a.IdRegistro);

            builder.Property(a => a.EnvioId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();

            builder
           .Property(a => a.IdRegistro)
           .HasDefaultValueSql("newId()");
        }
    }
}
