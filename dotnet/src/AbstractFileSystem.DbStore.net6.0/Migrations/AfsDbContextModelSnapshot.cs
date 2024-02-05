﻿// <auto-generated />
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Abstraction.DbStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace System.Migrations
{
    [DbContext(typeof(AfsDbContext))]
    partial class AfsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("afs")
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("System.IO.Abstraction.DbStore.AllowedValueEntity", b =>
                {
                    b.Property<string>("AttributeName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(450)")
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

                    b.HasKey("AttributeName", "Value");

                    b.ToTable("AllowedValues", "afs");
                });

            modelBuilder.Entity("System.IO.Abstraction.DbStore.AttributeDescriptorEntity", b =>
                {
                    b.Property<string>("AttributeName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

                    b.Property<string>("AttributeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsManagedValueRange")
                        .HasColumnType("bit");

                    b.Property<bool>("RequiredOnCreation")
                        .HasColumnType("bit");

                    b.Property<bool>("Updatable")
                        .HasColumnType("bit");

                    b.HasKey("AttributeName");

                    b.ToTable("AttributeDescriptors", "afs");
                });

            modelBuilder.Entity("System.IO.Abstraction.DbStore.FileAttributeEntity", b =>
                {
                    b.Property<long>("FileId")
                        .HasColumnType("bigint")
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

                    b.Property<string>("AttributeName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

                    b.Property<string>("AttributeValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FileId", "AttributeName");

                    b.HasIndex("AttributeName");

                    b.ToTable("FileAttributes", "afs");
                });

            modelBuilder.Entity("System.IO.Abstraction.DbStore.FileContentBlobEntity", b =>
                {
                    b.Property<long>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("FileId"), 1L, 1);

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RawContent")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("FileId");

                    b.ToTable("FileContentBlobs", "afs");
                });

            modelBuilder.Entity("System.IO.Abstraction.DbStore.AllowedValueEntity", b =>
                {
                    b.HasOne("System.IO.Abstraction.DbStore.AttributeDescriptorEntity", "AttributeDescriptor")
                        .WithMany("AllowedValues")
                        .HasForeignKey("AttributeName")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AttributeDescriptor");
                });

            modelBuilder.Entity("System.IO.Abstraction.DbStore.FileAttributeEntity", b =>
                {
                    b.HasOne("System.IO.Abstraction.DbStore.AttributeDescriptorEntity", "AttributeDescriptor")
                        .WithMany()
                        .HasForeignKey("AttributeName")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("System.IO.Abstraction.DbStore.FileContentBlobEntity", "File")
                        .WithMany("Attributes")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AttributeDescriptor");

                    b.Navigation("File");
                });

            modelBuilder.Entity("System.IO.Abstraction.DbStore.AttributeDescriptorEntity", b =>
                {
                    b.Navigation("AllowedValues");
                });

            modelBuilder.Entity("System.IO.Abstraction.DbStore.FileContentBlobEntity", b =>
                {
                    b.Navigation("Attributes");
                });
#pragma warning restore 612, 618
        }
    }
}
