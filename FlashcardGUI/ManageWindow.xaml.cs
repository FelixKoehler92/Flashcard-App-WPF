using FlashcardApp;
using System.Windows;

namespace FlashcardGUI
{
    public partial class ManageWindow : Window
    {
        private FlashcardManager _manager;

        public ManageWindow(FlashcardManager manager)
        {
            InitializeComponent();
            _manager = manager;

            // Tabelle direkt beim Öffnen des Fensters befüllen
            LoadData();
        }

        private void LoadData()
        {
            // ItemsSource ist der Befehl in WPF, um einer Tabelle eine Liste zu übergeben
            GridCards.ItemsSource = _manager.GetAllCards();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Prüfen, ob der Nutzer überhaupt eine Zeile in der Tabelle angeklickt hat
            if (GridCards.SelectedItem is Flashcard selectedCard)
            {
                // Sicherheitsabfrage (MessageBox mit Yes/No Buttons)
                MessageBoxResult result = MessageBox.Show(
                    $"Do you really want to delete this card:\n\n'{selectedCard.Question}'?",
                    "Delete Card",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                // Nur wenn der User auf "Yes" klickt, wird gelöscht
                if (result == MessageBoxResult.Yes)
                {
                    _manager.DeleteCard(selectedCard.Id);

                    // Tabelle aktualisieren, damit die gelöschte Karte sofort verschwindet
                    LoadData();
                }
            }
            else
            {
                // Wenn nichts ausgewählt war, weisen wir den User darauf hin
                MessageBox.Show("Please select a card from the list first.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}