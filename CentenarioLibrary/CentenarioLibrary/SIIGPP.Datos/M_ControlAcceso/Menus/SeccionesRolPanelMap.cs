using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_ControlAcceso.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_ControlAcceso.Menus
{
    public class SeccionesRolPanelMap : IEntityTypeConfiguration<SeccionesRolPanel>
    {
        public void Configure(EntityTypeBuilder<SeccionesRolPanel> builder)
        {
            builder.ToTable("CA_SECCIONES_ROL_PANEL")
                    .HasKey(a => a.IdMenuPanel);
            
            builder.Property(a => a.PanelControlId)
                    .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                    .IsRequired();

            builder.Property(a => a.SeccionId)
                    .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                    .IsRequired();

            builder.Property(a => a.RolId)
                    .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                    .IsRequired();
        }
    }
}
