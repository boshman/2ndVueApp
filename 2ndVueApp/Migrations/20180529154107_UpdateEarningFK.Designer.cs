﻿// <auto-generated />
using _2ndVueApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace _2ndVueApp.Migrations
{
    [DbContext(typeof(PTContext))]
    [Migration("20180529154107_UpdateEarningFK")]
    partial class UpdateEarningFK
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("_2ndVueApp.Models.Earner", b =>
                {
                    b.Property<int>("EarnerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .HasMaxLength(100);

                    b.HasKey("EarnerId");

                    b.ToTable("Earners");
                });

            modelBuilder.Entity("_2ndVueApp.Models.Earning", b =>
                {
                    b.Property<int>("EarningId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<DateTime>("EarnedDate");

                    b.Property<int>("EarnerId");

                    b.Property<int>("Points");

                    b.Property<int>("RuleId");

                    b.HasKey("EarningId");

                    b.HasIndex("EarnerId");

                    b.HasIndex("RuleId");

                    b.ToTable("Earnings");
                });

            modelBuilder.Entity("_2ndVueApp.Models.Rule", b =>
                {
                    b.Property<int>("RuleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<int>("Value");

                    b.HasKey("RuleId");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("_2ndVueApp.Models.Earning", b =>
                {
                    b.HasOne("_2ndVueApp.Models.Earner", "Earner")
                        .WithMany("Earnings")
                        .HasForeignKey("EarnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("_2ndVueApp.Models.Rule", "Rule")
                        .WithMany("Earnings")
                        .HasForeignKey("RuleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
