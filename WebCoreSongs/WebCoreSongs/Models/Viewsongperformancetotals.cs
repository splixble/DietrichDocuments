﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCoreSongs.Models;

public partial class Viewsongperformancetotals
{
    public long? Total { get; set; }

    [Key]
    public int SongId { get; set; }

    public string TitleAndArtist { get; set; }

    public DateOnly? FirstPerformed { get; set; }

    public DateOnly? LastPerformed { get; set; }
}