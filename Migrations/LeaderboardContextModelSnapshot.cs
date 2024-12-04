﻿// <auto-generated />
using System;
using Leaderboard.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Leaderboard.Migrations
{
    [DbContext(typeof(LeaderboardContext))]
    partial class LeaderboardContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Leaderboard.Models.Score", b =>
                {
                    b.Property<Guid>("PlayerId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.Property<string>("playerName")
                        .HasColumnType("TEXT");

                    b.HasKey("PlayerId", "Timestamp");

                    b.ToTable("Scores");
                });
#pragma warning restore 612, 618
        }
    }
}
