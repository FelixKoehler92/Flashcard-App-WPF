using FlashcardApp;
using Microsoft.EntityFrameworkCore;

namespace FlashcardGUI
{
    public class FlashcardContext : DbContext
    {
        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=flashcards.db");
        }
    }
}