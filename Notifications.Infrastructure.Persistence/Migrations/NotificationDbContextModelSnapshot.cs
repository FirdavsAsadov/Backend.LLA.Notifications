﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notifications.Infrastructure.Persistence.DataBase;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Notifications.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(NotificationDbContext))]
    partial class NotificationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Notifications.Infrastructure.Domain.Entiteis.NotificationHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(129536)
                        .HasColumnType("character varying(129536)");

                    b.Property<Guid>("ReceiverUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SenderUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TempalateId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TempalateId");

                    b.ToTable("NotificationHistories", (string)null);

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Notifications.Infrastructure.Domain.Entiteis.NotificationTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(129536)
                        .HasColumnType("character varying(129536)");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("NotificationTemplates", (string)null);

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Notifications.Infrastructure.Domain.Entiteis.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Notifications.Infrastructure.Domain.Entiteis.UserSettings", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("Notifications.Infrastructure.Domain.Entiteis.EmailHistory", b =>
                {
                    b.HasBaseType("Notifications.Infrastructure.Domain.Entiteis.NotificationHistory");

                    b.Property<string>("ReceiverEmailAddress")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("SendEmailAddress")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Notifications.Infrastructure.Domain.Entiteis.SmsHistory", b =>
                {
                    b.HasBaseType("Notifications.Infrastructure.Domain.Entiteis.NotificationHistory");

                    b.Property<string>("ReceiverPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("SenderPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Notifications.Infrastructure.Domain.Entiteis.EmailTemplate", b =>
                {
                    b.HasBaseType("Notifications.Infrastructure.Domain.Entiteis.NotificationTemplate");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Notifications.Infrastructure.Domain.Entiteis.SmsTemplate", b =>
                {
                    b.HasBaseType("Notifications.Infrastructure.Domain.Entiteis.NotificationTemplate");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Notifications.Infrastructure.Domain.Entiteis.NotificationHistory", b =>
                {
                    b.HasOne("Notifications.Infrastructure.Domain.Entiteis.NotificationTemplate", "Template")
                        .WithMany("Histories")
                        .HasForeignKey("TempalateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Template");
                });

            modelBuilder.Entity("Notifications.Infrastructure.Domain.Entiteis.UserSettings", b =>
                {
                    b.HasOne("Notifications.Infrastructure.Domain.Entiteis.User", null)
                        .WithOne("UserSettings")
                        .HasForeignKey("Notifications.Infrastructure.Domain.Entiteis.UserSettings", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Notifications.Infrastructure.Domain.Entiteis.NotificationTemplate", b =>
                {
                    b.Navigation("Histories");
                });

            modelBuilder.Entity("Notifications.Infrastructure.Domain.Entiteis.User", b =>
                {
                    b.Navigation("UserSettings")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
