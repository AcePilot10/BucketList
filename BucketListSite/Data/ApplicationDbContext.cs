using System;
using System.Collections.Generic;
using System.Text;
using BucketList.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BucketListSite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        new public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { 
        }
    }
}