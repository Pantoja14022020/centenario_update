using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Estrucutra;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Estructura
{
    public class ErrorReplicacionMap : IEntityTypeConfiguration<ErrorReplicacion>
    {
        public void Configure(EntityTypeBuilder<ErrorReplicacion> builder)
        { 
            builder.ToTable("CN_ERRORREPLICACION")
                   .HasKey(a => a.IdReplicacion);
            builder
           .Property(a => a.RegistroErrorId)
           .HasColumnType("UNIQUEIDENTIFIER ")
           .IsRequired();
            builder
           .Property(a => a.DistritoId)
           .HasColumnType("UNIQUEIDENTIFIER ")
           .IsRequired();
            builder
           .Property(a => a.IdReplicacion)
           .HasDefaultValueSql("newId()");
        }
    }
}
