using Microsoft.EntityFrameworkCore;
using Plugin.NetStandardStorage;
using ProjetMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetMobile.Services
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Manga> Mangas { get; set; }
        public DbSet<UserManga> UserMangas { get; set; }



        public static void Init()
        {
            using (MyDbContext database = new MyDbContext())
            {
                database.Database.EnsureCreated();
                database.Database.Migrate();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = CrossStorage.FileSystem.LocalStorage.FullPath
                + @"\"
                + String.Format("{0}.db3", "MyDatabase");

            optionsBuilder.UseSqlite($"Filename={path}");
        }

        protected void CreateModel(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Ignore<Manga>();                       //TEST 3 WORKAROUND OK
            //builder.Entity<UserProfile>().Ignore(u => u.Tool);//TEST 3: WORKAROUND OK
            builder.Entity<Manga>().Ignore(u => u.Image);
            //builder.Entity<UserProfile>().Ignore(u => u.Tool);//TEST 2: ERROR
        }
    }
}
