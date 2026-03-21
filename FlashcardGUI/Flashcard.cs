using System;

namespace FlashcardApp
{
    public class Flashcard
    {
        public string Id { get; set; } // Eindeutige ID pro Karte für fehlerfreies Bearbeiten/Löschen
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Category { get; set; } // Das Fach oder Modul, zu dem die Karte gehört
        public int Level { get; set; }
        public DateTime NextReviewDate { get; set; } // Fälligkeitsdatum

        public Flashcard(string question, string answer, string category)
        {
            Id = Guid.NewGuid().ToString();
            Question = question;
            Answer = answer;
            Category = category;
            Level = 1;
            NextReviewDate = DateTime.Now; // Karte ist sofort fällig
        }
    }
}