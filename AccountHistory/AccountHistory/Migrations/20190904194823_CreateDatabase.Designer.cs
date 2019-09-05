﻿// <auto-generated />
using System;
using AccountHistory.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AccountHistory.Api.Migrations
{
    [DbContext(typeof(AccountHistoryContext))]
    [Migration("20190904194823_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AccountHistory.DataAccess.Models.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Operations");

                    b.HasData(
                        new { Id = 1, Name = "Credit" },
                        new { Id = 2, Name = "Debit" }
                    );
                });

            modelBuilder.Entity("AccountHistory.DataAccess.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<double>("Balance");

                    b.Property<DateTime>("Date");

                    b.Property<int>("OperationId");

                    b.HasKey("Id");

                    b.HasIndex("OperationId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("AccountHistory.DataAccess.Models.Transaction", b =>
                {
                    b.HasOne("AccountHistory.DataAccess.Models.Operation", "Operation")
                        .WithMany("Transactions")
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
