using System.Windows;

namespace FlashcardGUI
{
    public partial class AddCardWindow : Window
    {
        private FlashcardManager _manager;

        public AddCardWindow(FlashcardManager manager)
        {
            InitializeComponent();
            _manager = manager;
            LoadCategories();
        }

        private void LoadCategories()
        {
            CmbCategory.ItemsSource = _manager.GetCategories();

            if (CmbCategory.Items.Count > 0)
            {
                CmbCategory.SelectedIndex = 0;
            }
        }

        private void BtnToggleNewCategory_Click(object sender, RoutedEventArgs e)
        {
            if (PanelNewCategory.Visibility == Visibility.Visible)
            {
                PanelNewCategory.Visibility = Visibility.Collapsed;
            }
            else
            {
                PanelNewCategory.Visibility = Visibility.Visible;
                TxtNewCategory.Focus();
            }
        }

        private void BtnSaveCategory_Click(object sender, RoutedEventArgs e)
        {
            string newCat = TxtNewCategory.Text.Trim();

            if (!string.IsNullOrEmpty(newCat))
            {
                _manager.AddCategory(newCat);
                LoadCategories();

                foreach (Category c in CmbCategory.Items)
                {
                    if (c.Name.ToLower() == newCat.ToLower())
                    {
                        CmbCategory.SelectedItem = c;
                        break;
                    }
                }

                TxtNewCategory.Text = "";
                PanelNewCategory.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // '??' bedeutet: "Wenn der Wert links davon 'null' ist, dann nimm stattdessen 'General'"
            string cat = CmbCategory.SelectedValue?.ToString() ?? "General";

            string q = TxtQuestion.Text.Trim();
            string a = TxtAnswer.Text.Trim();

            if (string.IsNullOrEmpty(q) || string.IsNullOrEmpty(a))
            {
                MessageBox.Show("Please enter both a question and an answer.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _manager.AddCard(q, a, cat);

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