namespace FlashcardGUI
{
    public class Category
    {
        public int Id { get; set; }

        // Das '= string.Empty;' sagt dem Compiler: "Keine Panik, dieser Wert startet nie als 'null'."
        public string Name { get; set; } = string.Empty;
    }
}