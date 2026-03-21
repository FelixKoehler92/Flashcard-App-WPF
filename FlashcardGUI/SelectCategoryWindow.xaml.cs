using System.Linq;
using System.Windows;

namespace FlashcardGUI
{
    public partial class SelectCategoryWindow : Window
    {
        // Diese Eigenschaft merkt sich, was der User ausgewählt hat
        public string SelectedCategory { get; private set; } = "All Categories";

        public SelectCategoryWindow(FlashcardManager manager)
        {
            InitializeComponent();

            // Lade alle Kategorien-Namen als Liste
            var categories = manager.GetCategories().Select(c => c.Name).ToList();

            // Option um alle Karten zu lernen, unabhängig von der Kategorie
            categories.Insert(0, "All Categories");

            CmbCategory.ItemsSource = categories;
            CmbCategory.SelectedIndex = 0;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            SelectedCategory = CmbCategory.SelectedItem?.ToString() ?? "All Categories";
            this.DialogResult = true;
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}