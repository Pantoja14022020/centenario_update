
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_JR.RAcuerdoReparatorio;

namespace SIIGPP.Datos.M_JR.RAcuerdoReparatorio
{
    public class CoordinacionDistritoMap : IEntityTypeConfiguration<CoordinacionDistrito>
    {
        public void Configure(EntityTypeBuilder<CoordinacionDistrito> builder)
        {
            builder.ToTable("JR_COORDINACIONDISTRITOS")
                .HasKey(a => a.IdCoordinacionDistritos);


            builder.Property(a => a.IdCoordinacionDistritos)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.UsuarioId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();


            builder.Property(a => a.DistritoId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();

            builder
           .Property(a => a.IdCoordinacionDistritos)
           .HasDefaultValueSql("newId()");


        }
    }
}
