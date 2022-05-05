using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;   

namespace VkBot
{
    public class Game
    {
        public int Id { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public DateTime DateGame { get; set; }
        //public string Group { get; set }
        public string Links { get; set; }
        public bool Completed { get; set; } = false;

        public ICollection<Betting> Bettings { get; set; }
        public Game()
        {
            Bettings = new List<Betting>();
        }
    }
    public class Betting
    {
        public int Id { get; set; }
        public long? VkId { get; set; }
        public string ScoreGame { get; set; }
        public long ScoreUser { get; set; }
        public DateTime DateBetting { get; set; }

        public int? GameId { get; set; }
        public Game Game { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public long? VkId { get; set; }
        public long? Score { get; set; } = 0;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Last2Name { get; set; }
        public DateTime DatePredskazanie { get; set; }
    }



    public class MyContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Betting> Bettings { get; set; }
        public DbSet<User> Users { get; set; }

        public MyContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseSqlite(@"Data Source=GameDB2.db;");
        }
    }
}
