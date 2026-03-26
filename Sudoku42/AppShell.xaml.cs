namespace Sudoku42
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AsosiyPage), typeof(AsosiyPage));
            Routing.RegisterRoute(nameof(DarajaPage2), typeof(DarajaPage2));
            Routing.RegisterRoute(nameof(SudokuPage), typeof(SudokuPage));
        }
    }
}
