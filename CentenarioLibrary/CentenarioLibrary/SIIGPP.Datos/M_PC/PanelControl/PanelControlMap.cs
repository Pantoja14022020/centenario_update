using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Panel.M_PanelControl;

namespace SIIGPP.Datos.M_PC.M_PanelControl
{
    class PanelControlMap : IEntityTypeConfiguration<PanelControl>
    {
        public void Configure(EntityTypeBuilder<PanelControl> builder)
        {
            builder.ToTable("PC_PANELCONTROL")
                   .HasKey(a => a.Id);
            builder.Property(a => a.Nombre)
                   .HasColumnType("nvarchar(800)");
            builder.Property(a => a.Abrev)
                   .HasColumnType("nvarchar(10)");
            builder.Property(a => a.Icono)
                   .HasColumnType("nvarchar(200)");
            builder.Property(a => a.Ruta)
                   .HasColumnType("nvarchar(MAX)");

            builder.Property(a => a.Id)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();

            builder
           .Property(a => a.Id)
           .HasDefaultValueSql("newId()");
        }
    }
}
