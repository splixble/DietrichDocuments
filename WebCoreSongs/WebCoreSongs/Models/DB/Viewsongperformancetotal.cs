using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCoreSongs.Models.DB;

public partial class Viewsongperformancetotal
{
    public long? Total { get; set; }

    [Key]
    public int SongId { get; set; }

    public string? TitleAndArtist { get; set; }

    public DateOnly? FirstPerformed { get; set; }

    public DateOnly? LastPerformed { get; set; }
}
