using FlashcardApp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlashcardGUI
{
    public class FlashcardManager
    {
        private readonly FlashcardContext _context;

        public FlashcardManager()
        {
            _context = new FlashcardContext();
            _context.Database.EnsureCreated();

            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "General" });
                _context.SaveChanges();
            }
        }

        public List<Category> GetCategories() => _context.Categories.ToList();

        public void AddCategory(string name)
        {
            if (!string.IsNullOrWhiteSpace(name) && !_context.Categories.Any(c => c.Name.ToLower() == name.ToLower()))
            {
                _context.Categories.Add(new Category { Name = name.Trim() });
                _context.SaveChanges();
            }
        }

        public void AddCard(string question, string answer, string category)
        {
            var newCard = new Flashcard(question, answer, category);
            _context.Flashcards.Add(newCard);
            _context.SaveChanges();
        }

        public List<Flashcard> GetAllCards() => _context.Flashcards.ToList();

        public bool DeleteCard(string id)
        {
            var card = _context.Flashcards.FirstOrDefault(c => c.Id == id);
            if (card != null)
            {
                _context.Flashcards.Remove(card);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Flashcard> GetCardsDueForReview(int maxAmount, string category = "All Categories")
        {
            var query = _context.Flashcards.Where(c => c.NextReviewDate <= DateTime.Now);
            if (category != "All Categories")
                query = query.Where(c => c.Category.ToLower() == category.ToLower());
            return query.OrderBy(c => c.Level).Take(maxAmount).ToList();
        }

        public void UpdateCardProgress(string id, bool success, int rating = 0)
        {
            var card = _context.Flashcards.FirstOrDefault(c => c.Id == id);
            if (card == null) return;

            // --- AGGRESSIVERE LOGIK ---
            if (!success || rating == 1)
            {
                card.Level = 1; // Hard: Kompletter Reset auf Anfang
            }
            else if (rating == 2)
            {
                card.Level = 2; // Medium: Setzt die Karte auf Level 2 zurück
            }
            else if (success || rating == 3)
            {
                card.Level++; // Easy: Karte wurde verstanden, Level steigt
            }

            int daysToAdd = card.Level switch
            {
                1 => 1,  // Level 1: Kommt direkt MORGEN wieder
                2 => 2,  // Level 2: Kommt schon in 2 TAGEN wieder
                3 => 5,  // Level 3: 5 Tage
                4 => 14, // Level 4: 2 Wochen
                _ => 30  // Level 5+: 1 Monat
            };

            card.NextReviewDate = DateTime.Now.AddDays(daysToAdd);
            _context.SaveChanges();
        }

        public int GetTotalCount() => _context.Flashcards.Count();

        public int GetDueCount(string category = "All Categories")
        {
            var query = _context.Flashcards.Where(c => c.NextReviewDate <= DateTime.Now);
            if (category != "All Categories")
                query = query.Where(c => c.Category.ToLower() == category.ToLower());
            return query.Count();
        }
    }
}