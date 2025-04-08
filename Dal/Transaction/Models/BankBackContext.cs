//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;
//using Work_Bank_api.Dal;

//namespace Work_Bank_api.Dal.Transaction.Models;

//public partial class BankBackContext : DbContext
//{
//    public BankBackContext()
//    {
//    }

//    public BankBackContext(DbContextOptions<BankBackContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<Transaction> Transactions { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        => optionsBuilder.UseSqlServer("Name=DB_Connection");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Transaction>(entity =>
//        {
//            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
//            entity.Property(e => e.EnglishName).HasMaxLength(15);
//            entity.Property(e => e.HebrewName).HasMaxLength(20);
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
