using LibraryManagementEFCore.Models;
using Microsoft.EntityFrameworkCore;
namespace LibraryManagementEFCore.Data;
public class LibraryDbContext : DbContext {
public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }
public DbSet<Book> Books => Set<Book>();
public DbSet<Member> Members => Set<Member>();
public DbSet<BorrowRecord> BorrowRecords => Set<BorrowRecord>();
protected override void OnModelCreating(ModelBuilder modelBuilder) {
base.OnModelCreating(modelBuilder);
modelBuilder.Entity<Book>().HasIndex(b => b.ISBN).IsUnique();
modelBuilder.Entity<Member>().HasIndex(m => m.Email).IsUnique();
modelBuilder.Entity<Book>().Property(b => b.Price).HasPrecision(10, 2);
modelBuilder.Entity<BorrowRecord>().HasOne(br => br.Book).WithMany(b => b.BorrowRecords).HasForeignKey(br => br.BookId);
modelBuilder.Entity<BorrowRecord>().HasOne(br => br.Member).WithMany(m => m.BorrowRecords).HasForeignKey(br => br.MemberId);
}
}
