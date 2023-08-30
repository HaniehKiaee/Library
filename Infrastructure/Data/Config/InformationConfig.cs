using Common.Constants;
using Common.Enum;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Config
{
    public class InformationConfig : IEntityTypeConfiguration<Information>
    {
        public void Configure(EntityTypeBuilder<Information> builder)
        {
            builder.Property(a => a.ClientIp).HasColumnType(DbConst.Type.varchar_64);
            builder.Property(a => a.Method).HasColumnType(DbConst.Type.varchar_16);
            builder.Property(a => a.Path).HasColumnType(DbConst.Type.NVarchar_256);
            builder.Property(a => a.QueryString).HasColumnType(DbConst.Type.NVarchar_256);
            builder.Property(a => a.Data).HasColumnType(DbConst.Type.NVarchar_4000);

            builder.Property(a => a.Type).HasConversion(new EnumToStringConverter<ContextType>());
            builder.Property(a => a.Type).HasColumnType(DbConst.Type.varchar_16);

            builder.ToTable(DbConst.Table.Information, DbConst.Schema.Log);
        }
    }
}
