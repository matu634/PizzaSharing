using System;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        
        //TODO: cascade delete
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Loan>()
                .HasOne(u => u.LoanGiver)
                .WithMany(user => user.GivenLoans)
                .HasForeignKey(loan => loan.LoanGiverId);
            
            builder.Entity<Loan>()
                .HasOne(u => u.LoanTaker)
                .WithMany(user => user.TakenLoans)
                .HasForeignKey(loan => loan.LoanTakerId);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Change> Changes { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanRow> LoanRows { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInCategory> ProductInCategories { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptParticipant> ReceiptParticipants { get; set; }
        public DbSet<ReceiptRow> ReceiptRows { get; set; }
        public DbSet<ReceiptRowChange> ReceiptRowChanges { get; set; }
    }
}