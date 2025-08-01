using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Captura;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.CapturaC
{
    public class CapturaMap : IEntityTypeConfiguration<Captura>
    {
        public void Configure(EntityTypeBuilder<Captura> builder)
        {
            builder.ToTable("CAT_CAPTURA")
                    .HasKey(a => a.IdCaptura);

            builder.Property(a => a.IdCaptura)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.RegistroTableroId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.UsuarioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.UModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.RemitioModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdCaptura)
           .HasDefaultValueSql("newId()");
            
            
            builder.HasOne(a => a.RHecho)
                   .WithMany()
                   .HasForeignKey(a => a.RHechoId);

            builder.HasOne(a => a.RegistroTableroI)
                   .WithMany()
                   .HasForeignKey(a => a.RegistroTableroId);

            builder.HasOne(a => a.Usuario)
                   .WithMany()
                   .HasForeignKey(a => a.UsuarioId);

            builder.HasOne(a => a.UModuloServicio)
                   .WithMany()
                   .HasForeignKey(a => a.UModuloServicioId);

            builder.HasOne(a => a.RemitioModuloServicio)
                   .WithMany()
                   .HasForeignKey(a => a.RemitioModuloServicioId);

        }
    }
}
