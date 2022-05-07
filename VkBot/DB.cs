﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;   

namespace VkBot
{
    public class Game
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public byte RightAnswer { get; set; }
    }
    
    public class User
    {
        public int Id { get; set; }
        public long? VkId { get; set; }
        public string Name { get; set; }
        public long? Score { get; set; } = 0;
        public long? NumSurv { get; set; } = 0; //Кол-во опросов в которых проголосовал
    }



    public class MyContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }

        public MyContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseSqlite(@"Data Source=GameDB4.db;");
        }
    }
}
