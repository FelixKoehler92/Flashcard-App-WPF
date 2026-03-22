using FlashcardApp;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlashcardGUI
{
    public partial class LearnWindow : Window
    {
        private FlashcardManager _manager;
        private List<Flashcard> _cardsToLearn;
        private int _currentIndex = 0;
        private int _correctCount = 0;

        // Merkt wie viele Karten es GANZ AM ANFANG der Session waren
        private int _initialSessionCount = 0;

        public LearnWindow(FlashcardManager manager, int amount, string category)
        {
            InitializeComponent();
            _manager = manager;

            _cardsToLearn = _manager.GetCardsDueForReview(amount, category);

            // Speichern der Start-Anzahl für die Prozentrechnung am Ende
            _initialSessionCount = _cardsToLearn.Count;

            LoadCurrentCard();
        }

        private void LoadCurrentCard()
        {
            if (_currentIndex >= _cardsToLearn.Count)
            {
                double percentage = 0;

                if (_initialSessionCount > 0)
                {
                    // Prozentzahl wird strikt anhand der ursprünglichen Karten berechnet
                    percentage = ((double)_correctCount / _initialSessionCount) * 100;
                }

                string resultMessage = $"Great job! You finished this learning session.\n\n" +
                                       $"Perfect matches (1st try): {_correctCount} out of {_initialSessionCount}\n" +
                                       $"Accuracy: {Math.Round(percentage, 1)}%";

                MessageBox.Show(resultMessage, "Session Complete 🎉", MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true;
                this.Close();
                return;
            }

            var currentCard = _cardsToLearn[_currentIndex];

            // Wenn Karten ans Ende gehängt werden, steigt hier die ".Count" Zahl sichtbar an!
            TxtProgress.Text = $"Card {_currentIndex + 1} of {_cardsToLearn.Count}";
            TxtCategoryLevel.Text = $"[{currentCard.Category}] - Level {currentCard.Level}";

            TxtQuestion.Text = currentCard.Question;
            TxtAnswer.Text = currentCard.Answer;

            TxtUserInput.Text = "";
            TxtUserInput.IsEnabled = true;

            AnswerPanel.Visibility = Visibility.Hidden;
            RatingButtonsPanel.Visibility = Visibility.Hidden;
            BtnShowAnswer.Visibility = Visibility.Visible;

            TxtUserInput.Focus();
        }

        private void BtnShowAnswer_Click(object sender, RoutedEventArgs e)
        {
            BtnShowAnswer.Visibility = Visibility.Hidden;
            AnswerPanel.Visibility = Visibility.Visible;
            RatingButtonsPanel.Visibility = Visibility.Visible;

            TxtUserInput.IsEnabled = false;

            string userInput = TxtUserInput.Text.Trim().ToLower();
            string correctAnswer = _cardsToLearn[_currentIndex].Answer.Trim().ToLower();

            if (string.IsNullOrEmpty(userInput))
            {
                TxtFeedback.Text = "No input provided. Compare below:";
                TxtFeedback.Foreground = Brushes.Gray;
            }
            else if (userInput == correctAnswer)
            {
                TxtFeedback.Text = "Perfect Match! 🎉";
                TxtFeedback.Foreground = Brushes.LightGreen;

                // Es gibt nur Punkte, wenn die Karte zum ERSTEN Mal (ohne Extrarunden) richtig war
                if (_currentIndex < _initialSessionCount)
                {
                    _correctCount++;
                }
            }
            else
            {
                TxtFeedback.Text = "Not quite right. Compare below:";
                TxtFeedback.Foreground = Brushes.IndianRed;
            }
        }

        private void BtnRate_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.Tag != null)
            {
                int rating = Convert.ToInt32(clickedButton.Tag);
                var currentCard = _cardsToLearn[_currentIndex];

                // Speichert die Level in der Datenbank ab
                _manager.UpdateCardProgress(currentCard.Id, true, rating);

                // Wenn die Karte als Hard (1) oder Medium (2) bewertet wird...
                if (rating == 1 || rating == 2)
                {
                    // ... wird sie einfach ganz ans Ende des aktuellen Stapels gehängt!
                    _cardsToLearn.Add(currentCard);
                }

                _currentIndex++;
                LoadCurrentCard();
            }
        }
    }
}