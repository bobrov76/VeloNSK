using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class FolderContext : DbContext
        {
            public DbSet<FileModel> Files { get; set; }
            public FolderContext(DbContextOptions<FolderContext> options) : base(options)
            {
                Database.EnsureCreated();
            }
    }
}

