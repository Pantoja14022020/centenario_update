using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogRDHMap : IEntityTypeConfiguration<LogRDH>
    {
        public void Configure(EntityTypeBuilder<LogRDH> builder)
        {
            builder.ToTable("LOG_RDH")
                .HasKey(a => a.IdAdminRDH);

            builder.Property(a => a.IdAdminRDH)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.DelitoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.RHechoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            //builder.Property(a => a.PersonaId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();

            builder.Property(a => a.IdAdminRDH)
                .HasDefaultValueSql("newId()");
        }
    }
}