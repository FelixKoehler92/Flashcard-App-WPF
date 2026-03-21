using System.Windows;

namespace FlashcardGUI
{
    public partial class MainWindow : Window
    {
        private FlashcardManager _manager;

        public MainWindow()
        {
            InitializeComponent();
            _manager = new FlashcardManager();
            UpdateDashboard();
        }

        private void UpdateDashboard()
        {
            TotalCardsText.Text = _manager.GetTotalCount().ToString();
            DueCardsText.Text = _manager.GetDueCount().ToString();
        }

        private void BtnAddCard_Click(object sender, RoutedEventArgs e)
        {
            AddCardWindow addWindow = new AddCardWindow(_manager);
            addWindow.Owner = this;

            if (addWindow.ShowDialog() == true)
            {
                UpdateDashboard();
            }
        }

        private void BtnStartLearning_Click(object sender, RoutedEventArgs e)
        {
            // Gibt es überhaupt IRGENDWELCHE fälligen Karten?
            if (_manager.GetDueCount() == 0)
            {
                MessageBox.Show("Awesome! No cards due for review right now. Come back later.", "All caught up");
                return;
            }

            // Öffnet das neue Fenster zur Kategorieauswahl
            SelectCategoryWindow catWindow = new SelectCategoryWindow(_manager);
            catWindow.Owner = this;

            if (catWindow.ShowDialog() == true)
            {
                string chosenCat = catWindow.SelectedCategory;

                // Prüfen, ob für genau diese Kategorie an Karten fällig sind
                if (_manager.GetDueCount(chosenCat) == 0)
                {
                    MessageBox.Show($"There are no cards due for '{chosenCat}' right now.", "All caught up");
                    return;
                }

                // Übergabe der ausgewählten Kategorie an das Lern-Fenster
                LearnWindow learnWindow = new LearnWindow(_manager, 10, chosenCat);
                learnWindow.Owner = this;

                if (learnWindow.ShowDialog() == true)
                {
                    UpdateDashboard();
                }
            }
        }

        private void BtnManageCards_Click(object sender, RoutedEventArgs e)
        {
            ManageWindow manageWindow = new ManageWindow(_manager);
            manageWindow.Owner = this;
            manageWindow.ShowDialog();
            UpdateDashboard();
        }
    }
}