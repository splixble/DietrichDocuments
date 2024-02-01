using System;
using System.Collections.Generic;

namespace WebCoreSongs.Models.DB;

public partial class Song
{
    public string? TitlePrefix { get; set; }

    public string Title { get; set; } = null!;

    public string? Code { get; set; }

    public string? Comment { get; set; }

    public int? PageNumberOriginal { get; set; }

    public string? Category { get; set; }

    public int Id { get; set; }

    public int? Artist { get; set; }

    public string? SongKey { get; set; }

    public string? SongbookOnly { get; set; }

    public string? PageNumber { get; set; }

    public string? OriginalKey { get; set; }

    public bool Intablet { get; set; }

    public string? DiffPdfname { get; set; }

    public bool SetlistAddable { get; set; }
}
