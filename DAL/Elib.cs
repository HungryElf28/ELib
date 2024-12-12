using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DomainModel
{
    public partial class Elib : DbContext
    {
        public Elib()
            : base("name=Elib")
        {
        }

        public virtual DbSet<authors> authors { get; set; }
        public virtual DbSet<books> books { get; set; }
        public virtual DbSet<genres> genres { get; set; }
        public virtual DbSet<offline_book> offline_book { get; set; }
        public virtual DbSet<reading_book> reading_book { get; set; }
        public virtual DbSet<review> review { get; set; }
        public virtual DbSet<tariff> tariff { get; set; }
        public virtual DbSet<user_tariff> user_tariff { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<administrators> administrators { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<authors>()
                .HasMany(e => e.books)
                .WithMany(e => e.authors)
                .Map(m => m.ToTable("book_author", "public").MapLeftKey("author_id").MapRightKey("book_id"));

            modelBuilder.Entity<books>()
                .HasMany(e => e.review)
                .WithRequired(e => e.books)
                .HasForeignKey(e => e.book_id);

            modelBuilder.Entity<books>()
                .HasMany(e => e.offline_book)
                .WithRequired(e => e.books)
                .HasForeignKey(e => e.book_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<books>()
                .HasMany(e => e.reading_book)
                .WithRequired(e => e.books)
                .HasForeignKey(e => e.book_id);

            modelBuilder.Entity<books>()
                .HasMany(e => e.users)
                .WithMany(e => e.books)
                .Map(m => m.ToTable("favour_book", "public").MapLeftKey("book_id").MapRightKey("user_id"));

            modelBuilder.Entity<genres>()
                .HasMany(e => e.books)
                .WithRequired(e => e.genres)
                .HasForeignKey(e => e.genreid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<genres>()
                .HasMany(e => e.tariff)
                .WithMany(e => e.genres)
                .Map(m => m.ToTable("genre_tariff", "public").MapLeftKey("genre_id"));

            modelBuilder.Entity<tariff>()
                .HasMany(e => e.user_tariff)
                .WithRequired(e => e.tariff)
                .HasForeignKey(e => e.tariff_id);

            modelBuilder.Entity<users>()
                .HasMany(e => e.offline_book)
                .WithRequired(e => e.users)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<users>()
                .HasMany(e => e.reading_book)
                .WithRequired(e => e.users)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<users>()
                .HasMany(e => e.review)
                .WithRequired(e => e.users)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<users>()
                .HasMany(e => e.user_tariff)
                .WithRequired(e => e.users)
                .HasForeignKey(e => e.user_id);
        }
    }
}
