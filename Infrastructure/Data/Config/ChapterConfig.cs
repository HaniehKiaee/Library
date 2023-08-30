using Common.Constants;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ChapterConfig : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.Property(chapter => chapter.Title).HasColumnType(DbConst.Type.NVarchar_200);
            //builder.Property(chapter => chapter.Text).HasColumnType("");

            builder.ToTable(DbConst.Table.Chapter, DbConst.Schema.Library);
            builder.HasQueryFilter(chapter => !chapter.IsDeleted);
        }
    }
}
