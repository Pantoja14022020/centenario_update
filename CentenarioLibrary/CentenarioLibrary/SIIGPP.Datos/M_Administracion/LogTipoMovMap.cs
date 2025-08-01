using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogTipoMovMap:IEntityTypeConfiguration<LogTipoMov>
    {
        public void Configure(EntityTypeBuilder<LogTipoMov> builder)
        {
            builder.ToTable("C_TIPO_MOV")
                .HasKey(a => a.IdMovimiento);

            builder.Property(a => a.IdMovimiento)
                .HasDefaultValueSql("newId()");

            builder.Property(a => a.IdMovimiento)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();
        }
    }
}