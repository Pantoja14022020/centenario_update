using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_IL.Agendas;
using System;
using System.Collections.Generic;
using System.Text;
namespace SIIGPP.Datos.M_IL.Agendas
{
    public class SolicitudAudienciaMap :IEntityTypeConfiguration<SolicitudAudiencia>
    {
        public void Configure(EntityTypeBuilder<SolicitudAudiencia> builder)
        {
            builder.ToTable("IL_SOLICITUDES_AUDIENCIA").HasKey(a => a.IdSolicitudAudiencia);



            builder.Property(a => a.IdSolicitudAudiencia).HasDefaultValueSql("newId()");
        }
    }
}
