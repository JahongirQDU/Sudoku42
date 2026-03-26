using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku42
{
    internal class RaqamPanelView:Grid
    {
        public event Action<int>? RaqamTanlandi;
        public RaqamPanelView()
        {
            for (int i = 0; i < 9; i++)
                ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });


            for (int i=1;i<=9;i++)
            {
                var raqam = new Button
                {
                    Text = i.ToString(),
                    FontSize = 18,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                raqam.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => RaqamTanlandi?.Invoke(int.Parse(raqam.Text)))
                });

                Children.Add(raqam);
                Grid.SetColumn(raqam, i-1);
            }
        }
    }
}
