using Common.Constants;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ErrorConfig : IEntityTypeConfiguration<Error>
    {
        public void Configure(EntityTypeBuilder<Error> builder)
        {
            builder.Property(a => a.ClientIp).HasColumnType(DbConst.Type.varchar_64);
            builder.Property(a => a.Method).HasColumnType(DbConst.Type.varchar_16);
            builder.Property(a => a.Path).HasColumnType(DbConst.Type.varchar_128);
            builder.Property(a => a.Data).HasColumnType(DbConst.Type.NVarchar_4000);
            builder.Property(a => a.ErrorMessage).HasColumnType(DbConst.Type.NVarchar_4000);

            builder.ToTable(DbConst.Table.Error, DbConst.Schema.Log);
        }
    }
}
