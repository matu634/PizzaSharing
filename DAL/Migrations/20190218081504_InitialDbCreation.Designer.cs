﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190218081504_InitialDbCreation")]
    partial class InitialDbCreation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("OrganizationId");

                    b.HasKey("CategoryId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Change", b =>
                {
                    b.Property<int>("ChangeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ChangeName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ChangeId");

                    b.ToTable("Changes");
                });

            modelBuilder.Entity("Domain.Identity.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Domain.Identity.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("UserNickname");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Domain.Loan", b =>
                {
                    b.Property<int>("LoanId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsPaid");

                    b.Property<int>("LoanGiverId");

                    b.Property<int>("LoanTakerId");

                    b.Property<int>("ReceiptParticipantId");

                    b.HasKey("LoanId");

                    b.HasIndex("LoanGiverId");

                    b.HasIndex("LoanTakerId");

                    b.HasIndex("ReceiptParticipantId")
                        .IsUnique();

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("Domain.LoanRow", b =>
                {
                    b.Property<int>("LoanRowId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Involvement");

                    b.Property<bool>("IsPaid");

                    b.Property<int>("LoanId");

                    b.Property<int>("ReceiptRowId");

                    b.HasKey("LoanRowId");

                    b.HasIndex("LoanId");

                    b.HasIndex("ReceiptRowId");

                    b.ToTable("LoanRows");
                });

            modelBuilder.Entity("Domain.Organization", b =>
                {
                    b.Property<int>("OrganizationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("OrganizationName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("OrganizationId");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Domain.Price", b =>
                {
                    b.Property<int>("PriceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ChangeId");

                    b.Property<int?>("ProductId");

                    b.Property<DateTime>("ValidFrom");

                    b.Property<DateTime>("ValidTo");

                    b.Property<decimal>("Value");

                    b.HasKey("PriceId");

                    b.HasIndex("ChangeId");

                    b.HasIndex("ProductId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("Domain.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.ProductInCategory", b =>
                {
                    b.Property<int>("ProductInCategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<int>("ProductId");

                    b.HasKey("ProductInCategoryId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductInCategories");
                });

            modelBuilder.Entity("Domain.Receipt", b =>
                {
                    b.Property<int>("ReceiptId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<int>("ReceiptManagerId");

                    b.HasKey("ReceiptId");

                    b.HasIndex("ReceiptManagerId");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("Domain.ReceiptParticipant", b =>
                {
                    b.Property<int>("ReceiptParticipantId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppUserId");

                    b.Property<int>("ReceiptId");

                    b.HasKey("ReceiptParticipantId");

                    b.HasIndex("AppUserId");

                    b.HasIndex("ReceiptId");

                    b.ToTable("ReceiptParticipants");
                });

            modelBuilder.Entity("Domain.ReceiptRow", b =>
                {
                    b.Property<int>("ReceiptRowId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int>("ProductId");

                    b.Property<int>("ReceiptId");

                    b.Property<decimal>("RowDiscount");

                    b.HasKey("ReceiptRowId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ReceiptId");

                    b.ToTable("ReceiptRows");
                });

            modelBuilder.Entity("Domain.ReceiptRowChange", b =>
                {
                    b.Property<int>("ReceiptRowChangeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChangeId");

                    b.Property<int>("ReceiptRowId");

                    b.HasKey("ReceiptRowChangeId");

                    b.HasIndex("ChangeId");

                    b.HasIndex("ReceiptRowId");

                    b.ToTable("ReceiptRowChanges");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Domain.Category", b =>
                {
                    b.HasOne("Domain.Organization", "Organization")
                        .WithMany("Categories")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Loan", b =>
                {
                    b.HasOne("Domain.Identity.AppUser", "LoanGiver")
                        .WithMany("GivenLoans")
                        .HasForeignKey("LoanGiverId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Identity.AppUser", "LoanTaker")
                        .WithMany("TakenLoans")
                        .HasForeignKey("LoanTakerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.ReceiptParticipant", "ReceiptParticipant")
                        .WithOne("Loan")
                        .HasForeignKey("Domain.Loan", "ReceiptParticipantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.LoanRow", b =>
                {
                    b.HasOne("Domain.Loan", "Loan")
                        .WithMany("LoanRows")
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.ReceiptRow", "ReceiptRow")
                        .WithMany("RowParticpantLoanRows")
                        .HasForeignKey("ReceiptRowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Price", b =>
                {
                    b.HasOne("Domain.Change", "Change")
                        .WithMany("Prices")
                        .HasForeignKey("ChangeId");

                    b.HasOne("Domain.Product", "Product")
                        .WithMany("Prices")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Domain.ProductInCategory", b =>
                {
                    b.HasOne("Domain.Category", "Category")
                        .WithMany("ProductsInCategory")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Product", "Product")
                        .WithMany("ProductInCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Receipt", b =>
                {
                    b.HasOne("Domain.Identity.AppUser", "ReceiptManager")
                        .WithMany("ManagedReceipts")
                        .HasForeignKey("ReceiptManagerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.ReceiptParticipant", b =>
                {
                    b.HasOne("Domain.Identity.AppUser", "AppUser")
                        .WithMany("ReceiptParticipants")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Receipt", "Receipt")
                        .WithMany("ReceiptParticipants")
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.ReceiptRow", b =>
                {
                    b.HasOne("Domain.Product", "Product")
                        .WithMany("ReceiptRows")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Receipt", "Receipt")
                        .WithMany("ReceiptRows")
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.ReceiptRowChange", b =>
                {
                    b.HasOne("Domain.Change", "Change")
                        .WithMany("ReceiptRowChanges")
                        .HasForeignKey("ChangeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.ReceiptRow", "ReceiptRow")
                        .WithMany("ReceiptRowChanges")
                        .HasForeignKey("ReceiptRowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Domain.Identity.AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Domain.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Domain.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Domain.Identity.AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Domain.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
