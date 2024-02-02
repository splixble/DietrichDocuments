using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebCoreSongs.Models;

namespace WebCoreSongs.Data
{
    public class WebCoreSongsContext : DbContext
    {
        public WebCoreSongsContext (DbContextOptions<WebCoreSongsContext> options)
            : base(options)
        {
        }
    }
}
