﻿// <auto-generated />
using BankingApp.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BankingApp.Migrations
{
    [DbContext(typeof(BankContext))]
    partial class BankContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BankingApp.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<int>("FromAccountNo")
                        .HasColumnType("integer");

                    b.Property<int>("ToAccountNo")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FromAccountNo");

                    b.HasIndex("ToAccountNo");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BankingApp.Models.User", b =>
                {
                    b.Property<int>("AccountNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AccountNo"));

                    b.Property<double>("Balance")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AccountNo");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BankingApp.Models.Transaction", b =>
                {
                    b.HasOne("BankingApp.Models.User", "FromUser")
                        .WithMany("SentTransactions")
                        .HasForeignKey("FromAccountNo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_transaction_user_from");

                    b.HasOne("BankingApp.Models.User", "ToUser")
                        .WithMany("RecievedTransactions")
                        .HasForeignKey("ToAccountNo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_transaction_user_to");

                    b.Navigation("FromUser");

                    b.Navigation("ToUser");
                });

            modelBuilder.Entity("BankingApp.Models.User", b =>
                {
                    b.Navigation("RecievedTransactions");

                    b.Navigation("SentTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
