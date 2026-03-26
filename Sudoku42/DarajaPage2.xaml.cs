using System.Threading.Tasks;

namespace Sudoku42;

public partial class DarajaPage2 : ContentPage
{
	public DarajaPage2()
	{
		InitializeComponent();
	}
    public static class DarajaXotira
    {
        public static string Daraja = "oson";
    }
    private async Task SudokuOynagaUt(string daraja)
	{ 
        DarajaXotira.Daraja = daraja;
        await Shell.Current.GoToAsync($"{nameof(SudokuPage)}?daraja={daraja}"); }


    private async void Button_Oson(object sender, EventArgs e)
    {
		await SudokuOynagaUt("Oson");
    }
    private async void Button_Orta(object sender, EventArgs e)
    {
        await SudokuOynagaUt("Normal");
    }

         private async void Button_Murakkab(object sender, EventArgs e)
    {
        await SudokuOynagaUt("Murakkab");
    }

}
