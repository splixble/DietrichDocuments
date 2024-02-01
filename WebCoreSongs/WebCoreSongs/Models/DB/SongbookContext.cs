using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebCoreSongs.Models.DB;

namespace WebCoreSongs.Models.DB;

public partial class SongbookContext : DbContext
{
    public SongbookContext()
    {
    }

    public SongbookContext(DbContextOptions<SongbookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Viewsongperformancetotal> Viewsongperformancetotals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=tcp:dietrichsql.database.windows.net,1433;Initial Catalog=Songbook; TrustServerCertificate=False;Connection Timeout=60; User ID=dietrichroot;Password=al'gonQin88;  ApplicationIntent=ReadWrite; MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Viewsongperformancetotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewsongperformancetotals", "songbook");

            entity.Property(e => e.FirstPerformed).HasColumnName("firstPerformed");
            entity.Property(e => e.LastPerformed).HasColumnName("lastPerformed");
            entity.Property(e => e.SongId).HasColumnName("SongID");
            entity.Property(e => e.TitleAndArtist)
                .HasMaxLength(8000)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<WebCoreSongs.Models.DB.Viewsongperformance> Viewsongperformance { get; set; } = default!;
}
