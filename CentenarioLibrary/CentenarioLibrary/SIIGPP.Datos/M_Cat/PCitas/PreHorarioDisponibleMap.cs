using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.PRegistro;
using SIIGPP.Entidades.M_Cat.PCitas;
using System;
using System.Collections.Generic;
using System.Text;


namespace SIIGPP.Datos.M_Cat.PCitas
{
    public class PreHorarioDisponibleMap : IEntityTypeConfiguration<PreHorarioDisponible>
    {
        public void Configure(EntityTypeBuilder<PreHorarioDisponible> builder)
        {
            builder.ToTable("PRE_HORARIODISPONIBLE").HasKey(a => a.idHorarioDisponible);
            builder.Property(a => a.idHorarioDisponible).HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL").IsRequired();
            builder.Property(a => a.idHorarioDisponible).HasDefaultValueSql("newId()");
            builder.Property(a=>a.AgenciaId).HasColumnType("UNIQUEIDENTIFIER");
        }
    }
}
