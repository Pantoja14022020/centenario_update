using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_ControlAcceso.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_ControlAcceso.Menus
{
    public class SubModuloRolMap : IEntityTypeConfiguration<SubModuloRol>
    {
        public void Configure(EntityTypeBuilder<SubModuloRol> builder)
        {
            builder.ToTable("CA_SUBMODULOS_ROL")
                    .HasKey(a => a.IdSubModuloRol);
            
            builder.Property(a => a.IdSubModuloRol)
                    .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                    .IsRequired();
            builder.Property(a => a.IdSubModuloRol)
                    .HasDefaultValueSql("newId()");

            builder.Property(a => a.ModuloId)
                    .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                    .IsRequired();

            builder.Property(a => a.ModuloRolId)
                    .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                    .IsRequired();
        }
    }
}
