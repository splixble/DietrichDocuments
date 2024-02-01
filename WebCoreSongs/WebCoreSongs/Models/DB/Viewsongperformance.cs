using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCoreSongs.Models.DB;

public partial class Viewsongperformance
{
    [Key]
    public int PerformanceId { get; set; }

    public int Song { get; set; }

    public string? Comment { get; set; }

    public string? TitleAndArtist { get; set; }

    public DateOnly PerformanceDate { get; set; }

    public bool DidIlead { get; set; }

    public int? PerformanceYear { get; set; }

    public string? PerformanceQtr { get; set; }

    public string? PerformanceMonth { get; set; }

    public string VenueName { get; set; } = null!;

    public int Venue { get; set; }
}
