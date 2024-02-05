using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace System.IO.Abstraction.DbStore {

  /// <summary> EntityFramework DbContext (based on schema version '1.0.0') </summary>
  public partial class AfsDbContext : DbContext{

    public const String SchemaVersion = "1.0.0";

    public DbSet<AllowedValueEntity> AllowedValues { get; set; }

    public DbSet<AttributeDescriptorEntity> AttributeDescriptors { get; set; }

    public DbSet<FileAttributeEntity> FileAttributes { get; set; }

    public DbSet<FileContentBlobEntity> FileContentBlobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);

#region Mapping

      modelBuilder.HasDefaultSchema("afs");

      //////////////////////////////////////////////////////////////////////////////////////
      // AllowedValue
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgAllowedValue = modelBuilder.Entity<AllowedValueEntity>();
      cfgAllowedValue.ToTable("AllowedValues");
      cfgAllowedValue.HasKey((e) => new {e.AttributeName, e.Value});
      cfgAllowedValue.Property((e) => e.AttributeName).ValueGeneratedNever().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);
      cfgAllowedValue.Property((e) => e.Value).ValueGeneratedNever().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

      // LOOKUP: >>> AttributeDescriptor
      cfgAllowedValue
        .HasOne((lcl) => lcl.AttributeDescriptor )
        .WithMany((rem) => rem.AllowedValues )
        .HasForeignKey(nameof(AllowedValueEntity.AttributeName))
        .OnDelete(DeleteBehavior.Restrict);

      //////////////////////////////////////////////////////////////////////////////////////
      // AttributeDescriptor
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgAttributeDescriptor = modelBuilder.Entity<AttributeDescriptorEntity>();
      cfgAttributeDescriptor.ToTable("AttributeDescriptors");
      cfgAttributeDescriptor.HasKey((e) => e.AttributeName);
      cfgAttributeDescriptor.Property((e) => e.AttributeName).ValueGeneratedNever().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

      //////////////////////////////////////////////////////////////////////////////////////
      // FileAttribute
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgFileAttribute = modelBuilder.Entity<FileAttributeEntity>();
      cfgFileAttribute.ToTable("FileAttributes");
      cfgFileAttribute.HasKey((e) => new {e.FileId, e.AttributeName});
      cfgFileAttribute.Property((e) => e.FileId).ValueGeneratedNever().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);
      cfgFileAttribute.Property((e) => e.AttributeName).ValueGeneratedNever().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

      // LOOKUP: >>> FileContentBlob
      cfgFileAttribute
        .HasOne((lcl) => lcl.File )
        .WithMany((rem) => rem.Attributes )
        .HasForeignKey(nameof(FileAttributeEntity.FileId))
        .OnDelete(DeleteBehavior.Restrict);

      // LOOKUP: >>> AttributeDescriptor
      cfgFileAttribute
        .HasOne((lcl) => lcl.AttributeDescriptor )
        .WithMany()
        .HasForeignKey(nameof(FileAttributeEntity.AttributeName))
        .OnDelete(DeleteBehavior.Restrict);

      //////////////////////////////////////////////////////////////////////////////////////
      // FileContentBlob
      //////////////////////////////////////////////////////////////////////////////////////

      var cfgFileContentBlob = modelBuilder.Entity<FileContentBlobEntity>();
      cfgFileContentBlob.ToTable("FileContentBlobs");
      cfgFileContentBlob.HasKey((e) => e.FileId);
      cfgFileContentBlob.Property((e) => e.FileId).UseIdentityColumn();

#endregion

      this.OnModelCreatingCustom(modelBuilder);
    }

    partial void OnModelCreatingCustom(ModelBuilder modelBuilder);

    protected override void OnConfiguring(DbContextOptionsBuilder options) {

      //reqires separate nuget-package Microsoft.EntityFrameworkCore.SqlServer
      options.UseSqlServer(_ConnectionString);

      //reqires separate nuget-package Microsoft.EntityFrameworkCore.Proxies
      options.UseLazyLoadingProxies();

      this.OnConfiguringCustom(options);

    }

    partial void OnConfiguringCustom(DbContextOptionsBuilder options);
    private String _ConnectionString = "Server=(localdb)\\MsSqlLocalDb;Database=AfsDbContext;Integrated Security=True;Persist Security Info=True;MultipleActiveResultSets=True;";

  }
}
