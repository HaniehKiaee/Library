using Common.Constants;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasMany(book => book.Chapters)
                   .WithOne()
                   .HasForeignKey(chapter=> chapter.BookId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(book => book.Title).HasColumnType(DbConst.Type.NVarchar_200);
            builder.Property(book => book.Published_Date).HasColumnType(DbConst.Type.varchar_8);

            builder.ToTable(DbConst.Table.Book, DbConst.Schema.Library);
            builder.HasQueryFilter(book => !book.IsDeleted);
        }
    }
}
