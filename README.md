# 🧠 Flashcard Learning APP - Spaced Repetition App

A modern, desktop-based Flashcard Application built with **C# / WPF** and **SQLite**. 
This app helps users learn efficiently using a custom Spaced Repetition algorithm (inspired by Anki) to ensure long-term memory retention.

---

## ✨ Features (Was die App kann)

- **Spaced Repetition Algorithm:** Karten werden basierend auf dem eigenen Feedback (`Hard`, `Medium`, `Easy`) in dynamischen Intervallen (1, 2, 5, 14, 30 Tage) wiederholt.
- **Anki-Style Learning Loop:** Falsch oder als mittelmäßig bewertete Karten werden automatisch an das Ende der aktuellen Lern-Session angehängt, bis sie wirklich sitzen.
- **Category Management:** Karten können in selbst erstellte Kategorien (z. B. "C# Basics", "Netzwerktechnik") einsortiert werden.
- **Targeted Sessions:** Lern-Sessions können gezielt auf einzelne Kategorien gefiltert werden.
- **Live Statistics:** Das Dashboard zeigt die Gesamtanzahl der Karten und die aktuell fälligen Karten. Am Ende jeder Session gibt es eine genaue Auswertung (Genauigkeit in %).
- **Modern Dark Mode UI:** Komplett maßgeschneidertes WPF-Design mit dynamischen Textfeldern, Custom Dropdowns, Drop-Shadows und Fluent-Icons.

---

## 📸 Screenshots

*(Füge hier per Drag & Drop ein Bild von deinem Dashboard ein)*
> **Dashboard:** Übersicht der Statistiken und Schnellzugriff auf alle Funktionen.

*(Füge hier per Drag & Drop ein Bild von deinem Lern-Fenster ein)*
> **Learning Session:** Fokus-Modus beim Lernen mit intelligentem Auto-Resize für lange Antworten.

---

## 🚀 Chronological Development Steps (Wie die App entstanden ist)

Dieses Projekt wurde schrittweise von Grund auf entwickelt. Hier ist der chronologische Ablauf der Architektur- und Feature-Implementierung:

### Step 1: Database & Architecture Setup
- Einrichtung von **Entity Framework Core** und einer lokalen **SQLite**-Datenbank.
- Erstellung der Models (`Flashcard.cs`, `Category.cs`) für den Code-First-Ansatz.
- Implementierung des `FlashcardManager.cs` als zentrale Schnittstelle (Business Logic) für alle CRUD-Operationen.

### Step 2: Dashboard & Core UI
- Entwicklung des Hauptfensters (`MainWindow.xaml`) in einem modernen Dark-Mode-Design.
- Einbindung von Microsoft `Segoe MDL2 Assets` für native UI-Icons.
- Implementierung von Live-Statistiken (Total Cards & Due for Review), die sich nach jeder Aktion automatisch aktualisieren.

### Step 3: CRUD Operations & Category System
- Entwicklung des `AddCardWindow` mit dynamischen Textfeldern (automatische Höhenanpassung via `ScrollViewer` und `TextWrapping`).
- Implementierung eines Custom-Styles (ControlTemplate) für WPF `ComboBoxes`, um den Windows-Standard zu überschreiben und sie in den Dark Mode zu integrieren.
- Funktion zum direkten Hinzufügen neuer Kategorien während der Kartenerstellung.
- Erstellung des `ManageWindow` mit einem WPF `DataGrid` zur tabellarischen Übersicht und Löschfunktion von Karten.

### Step 4: The Learning Engine (Spaced Repetition)
- Aufbau des `LearnWindow` für interaktive Lern-Sessions.
- Logik zur Abfrage der exakten Text-Übereinstimmung ("Perfect Match").
- Implementierung des dynamischen Intervall-Algorithmus: 
  - `Hard (1)`: Reset auf Level 1 (nächster Tag).
  - `Medium (2)`: Zurück auf Level 2 (in 2 Tagen).
  - `Easy (3)`: Level-Up (5, 14 oder 30 Tage).

### Step 5: Advanced Features & Polish
- **Anki-Loop:** Integration einer Endlosschleife für die aktuelle Session (schwer/mittel bewertete Karten werden ans Ende gehängt).
- **Session Filtering:** Einbau des `SelectCategoryWindow`, um Sessions vor dem Start auf spezielle Fächer einzugrenzen.
- **Post-Session Analytics:** Berechnung und Anzeige der exakten Genauigkeit (Accuracy in %) nach Abschluss einer Session.

---

## 🛠️ Technologies Used
- **Language:** C# (.NET)
- **Framework:** Windows Presentation Foundation (WPF)
- **Database:** SQLite
- **ORM:** Entity Framework Core (EF Core)
- **Architecture:** MVVM-inspired Structure (Separation of UI and Data Access)

---

## 💻 How to run locally

1. Clone this repository:
   ```bash
   git clone [HIER DEN LINK ZU DEINEM GITHUB REPO EINFÜGEN]
