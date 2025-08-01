using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_ServiciosPericiales;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_ServiciosPericiales
{
    public class ASPMap : IEntityTypeConfiguration<ASP>
    {
        public void Configure(EntityTypeBuilder<ASP> builder)
        {
            builder.ToTable("SP_ASP")
             .HasKey(a => a.IdASP);

            builder
           .Property(a => a.IdASP)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.AgenciaId)
           .HasColumnType("UNIQUEIDENTIFIER ")
           .IsRequired();
            builder
           .Property(a => a.ServicioPericialId)
           .HasColumnType("UNIQUEIDENTIFIER ")
           .IsRequired();
            builder
           .Property(a => a.IdASP)
           .HasDefaultValueSql("newId()");
        }
    }
}
