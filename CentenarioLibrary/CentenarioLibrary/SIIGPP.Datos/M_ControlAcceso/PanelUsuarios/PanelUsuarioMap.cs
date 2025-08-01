using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_ControlAcceso.PanelUsuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_ControlAcceso.PanelUsuarios
{
    public class PanelUsuarioMap : IEntityTypeConfiguration<PanelUsuario>
    {
        public void Configure(EntityTypeBuilder<PanelUsuario> builder)
        {
            builder.ToTable("CA_PANELUSUARIO")
                    .HasKey(a => a.IdPanelUsuario);

            builder.Property(a => a.IdPanelUsuario)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.UsuarioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.PanelControlId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdPanelUsuario)
           .HasDefaultValueSql("newId()");
        }
    }
}
