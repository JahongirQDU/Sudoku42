using Microsoft.Maui.Devices;
namespace Sudoku42;

[QueryProperty(nameof(Daraja),"daraja")]
public partial class SudokuPage : ContentPage
{
	private String daraja;

	int xato = 0;
	
    private readonly SudokuMantiq mantiq = new SudokuMantiq();
	public string Daraja
	{
		get => daraja;
		set
		{   daraja = value;
			DarajaLabel.Text = "Daraja: " + daraja;
		}
	}
	private Grid? tanlanganKatak;
	private readonly Grid[,] kataklar = new Grid[9, 9];
	private readonly Label[,] matnlar = new Label[9, 9];


	public SudokuPage()
	{
		InitializeComponent();
		SudokuOynaMoslashtirish();
		SudokuYaratish();
		IndekslarTuzish();
	    BoshlangichSudokuJoylash();

		var panel = new RaqamPanelView();
        panel.RaqamTanlandi += RaqamBosildi;
        AsosiyLayout.Children.Insert(AsosiyLayout.Children.Count-2, panel);
   
		
    }

	private void SudokuOynaMoslashtirish()
	{
		var ekran = DeviceDisplay.MainDisplayInfo;
		double olcham = (ekran.Width / ekran.Density) * 0.85;
		SudokuGrid.WidthRequest = olcham;
		SudokuGrid.HeightRequest = olcham;
	}
	private void SudokuYaratish()
	{
		SudokuGrid.Children.Clear();
		for(int i=0;i<9;i++)
		{
			for(int j=0;j<9;j++)
			{
                var katak = new Grid { BackgroundColor = Colors.White };
				var matn = new Label
				{
					FontSize = 18,
					HorizontalOptions=LayoutOptions.Center,
					VerticalOptions=LayoutOptions.Center
                };
				katak.Children.Add(matn);
				katak.GestureRecognizers.Add(new TapGestureRecognizer
				{
					Command = new Command(() => KatakTanla(katak))
				});

				SudokuGrid.Children.Add(katak);
				Grid.SetRow(katak,i);
				Grid.SetColumn(katak,j);
            }
		}
	}

	private void IndekslarTuzish()
	{
		foreach(var element in SudokuGrid.Children)
		{
			if (element is not Grid katak) continue;
			if (katak.Children[0] is not Label matn) continue;

                int qator = Grid.GetRow(katak);
				int ustun = Grid.GetColumn(katak);
				kataklar[qator, ustun] = katak;
				matnlar[qator, ustun] = matn;		
		}
    }

 private  void BoshlangichSudokuJoylash()
	{
		int tur;
        if (DarajaPage2.DarajaXotira.Daraja == "Murakkab") tur = 3;
        else if (DarajaPage2.DarajaXotira.Daraja == "Normal") tur = 2;
        else tur = 1;

		SudokuBoshlangich.SudokuTuz(tur);

        mantiq.MaydonYuklash(SudokuBoshlangich.Jadval);
		for (int i=0;i<9;i++)
		{
			for(int j=0;j<9;j++)
			{
				int qiymat = SudokuBoshlangich.Jadval[i, j];
				if (qiymat == 0) continue;
				matnlar[i, j].Text = qiymat.ToString();
				kataklar[i, j].BackgroundColor = Colors.LightGray;
				kataklar[i, j].GestureRecognizers.Clear();
            }
        }
    }


	private void KatakTanla( Grid katak)
	{
		if (tanlanganKatak != null && tanlanganKatak.GestureRecognizers.Count > 0)
			tanlanganKatak.BackgroundColor = Colors.White;

		tanlanganKatak = katak;
		if (katak.GestureRecognizers.Count > 0)
			katak.BackgroundColor = Colors.LightBlue;
	}

	private void RaqamBosildi(int raqam)
	{
		if (tanlanganKatak == null) return;
		if (tanlanganKatak.GestureRecognizers.Count==0 ) return;

		int qator = Grid.GetRow(tanlanganKatak);
		int ustun = Grid.GetColumn(tanlanganKatak);

		matnlar[qator, ustun].Text = raqam.ToString();

		mantiq.QiymatSet(qator, ustun, raqam);
		XatoniTekshirish();
		YutuqBormi();
		
	}

	private async void XatoniTekshirish()
	{// Sevinch
		int qator = Grid.GetRow(tanlanganKatak);
		int ustun = Grid.GetColumn(tanlanganKatak);
		int qiymat = int.Parse(matnlar[qator, ustun].Text);
		if (mantiq.XatoBormi(qator, ustun, qiymat))
		{
			kataklar[qator, ustun].BackgroundColor = Colors.MistyRose;
			matnlar[qator, ustun].TextColor = Colors.Red;
            xato++;
            XatoLabel.Text = $"Xatolik: {xato}/3";

			if (xato == 3)
			{
				await DisplayAlert("Afsus", "Sudokuni yutqazdingiz", "OK");
				//Shell.Current.GoToAsync(nameof(AsosiyPage), typeof(AsosiyPage));
			}

        }
        else
		{
			kataklar[qator, ustun].BackgroundColor = Colors.White;
			matnlar[qator, ustun].TextColor = Colors.Black;
		}
		/////


		////for (int qator=0;qator<9;qator++)
		////	for(int ustun =0;ustun<9;ustun++)
		////	{
		////		var katak = kataklar[qator, ustun];
		////		var matn = matnlar[qator, ustun];
		////		if (!int.TryParse(matn.Text, out int qiymat) && qiymat == 0) continue;
                
		////		if (mantiq.XatoBormi(qator, ustun, qiymat) && katak.GestureRecognizers.Count>0)
				
  ////                  {
		////			//katak.BackgroundColor = Colors.MistyRose;
		////			//matn.TextColor = Colors.Red;

		////			xato++;
		////			XatoLabel.Text = $"Xatolik: {xato}/3";

  ////                  if (xato>3) await DisplayAlert("Afsus", "Sudokuni yutqazdingiz", "OK");

  ////              }
		////		else if(katak.GestureRecognizers.Count > 0)
  ////              {
		////			katak.BackgroundColor = Colors.White;
		////			matn.TextColor = Colors.Black;
		////		}
		////	}
    }


	private async void YutuqBormi()
	{
		if (!mantiq.GalabaBormi()) return;

		await DisplayAlert("Tabriklaymiz", "Sudokuni yutdingiz", "OK");
	}
}