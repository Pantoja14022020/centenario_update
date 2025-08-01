using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_ControlAcceso.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_ControlAcceso.Menus
{
    public class ModulosRolMap : IEntityTypeConfiguration<ModuloRol>
    {
        public void Configure(EntityTypeBuilder<ModuloRol> builder)
        {
            builder.ToTable("CA_MODULOS_ROL")
                    .HasKey(a => a.IdModuloRol);
            
            builder.Property(a => a.IdModuloRol)
                    .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                    .IsRequired();

            builder.Property(a => a.ModuloId)
                    .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                    .IsRequired();

            builder.Property(a => a.MenuPanelId)
                    .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                    .IsRequired();
            
            builder.Property(a => a.IdModuloRol)
                    .HasDefaultValueSql("newId()");
        }
    }
}
