﻿// <auto-generated />
using System;
using GamerRater.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GamerRater.Api.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GamerRater.Model.GameCover", b =>
                {
                    b.Property<int>("id");

                    b.Property<int>("game");

                    b.Property<int>("height");

                    b.Property<string>("image_id");

                    b.Property<string>("url");

                    b.Property<int>("width");

                    b.HasKey("id");

                    b.ToTable("GameCover");
                });

            modelBuilder.Entity("GamerRater.Model.GameRoot", b =>
                {
                    b.Property<int>("id");

                    b.Property<int>("Category");

                    b.Property<int>("Cover");

                    b.Property<int>("Created_at");

                    b.Property<int?>("GameCoverid");

                    b.Property<int?>("GameCoverid1");

                    b.Property<string>("Name");

                    b.Property<int?>("PlatformId");

                    b.Property<double>("Popularity");

                    b.Property<string>("Slug");

                    b.Property<string>("Storyline");

                    b.Property<string>("Summary");

                    b.Property<double>("Total_rating");

                    b.Property<int>("Updated_at");

                    b.Property<string>("Url");

                    b.HasKey("id");

                    b.HasIndex("GameCoverid");

                    b.HasIndex("GameCoverid1");

                    b.HasIndex("PlatformId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("GamerRater.Model.Platform", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("GamerRater.Model.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GameRootid");

                    b.Property<int?>("Gameid");

                    b.Property<string>("Review");

                    b.Property<int>("Stars");

                    b.Property<int?>("UserId");

                    b.Property<int?>("UserId1");

                    b.Property<DateTime>("date");

                    b.HasKey("Id");

                    b.HasIndex("GameRootid");

                    b.HasIndex("Gameid");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("GamerRater.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<int?>("GroupId");

                    b.Property<string>("LastName");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GamerRater.Model.UserGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Group");

                    b.HasKey("Id");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("GamerRater.Model.GameRoot", b =>
                {
                    b.HasOne("GamerRater.Model.GameCover", "GameCover")
                        .WithMany()
                        .HasForeignKey("GameCoverid");

                    b.HasOne("GamerRater.Model.GameCover")
                        .WithMany()
                        .HasForeignKey("GameCoverid1");

                    b.HasOne("GamerRater.Model.Platform")
                        .WithMany("Games")
                        .HasForeignKey("PlatformId");
                });

            modelBuilder.Entity("GamerRater.Model.Rating", b =>
                {
                    b.HasOne("GamerRater.Model.GameRoot")
                        .WithMany("Ratings")
                        .HasForeignKey("GameRootid");

                    b.HasOne("GamerRater.Model.GameRoot", "Game")
                        .WithMany()
                        .HasForeignKey("Gameid");

                    b.HasOne("GamerRater.Model.User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId");

                    b.HasOne("GamerRater.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("GamerRater.Model.User", b =>
                {
                    b.HasOne("GamerRater.Model.UserGroup", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId");
                });
#pragma warning restore 612, 618
        }
    }
}
