using System.Threading.Tasks;

namespace Sudoku42;

public partial class AsosiyPage : ContentPage
{
	public AsosiyPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(DarajaPage2));
    }
}