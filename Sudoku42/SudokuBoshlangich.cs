using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku42
{
    internal class SudokuBoshlangich
    {// Oson daraja
        public static readonly int[,] Oson =
        {
        {5,3,0, 0,7,0, 0,0,0},
        {6,0,0, 1,9,5, 0,0,0},
        {0,9,8, 0,0,0, 0,6,0},

        {8,0,0, 0,6,0, 0,0,3},
        {4,0,0, 8,0,3, 0,0,1},
        {7,0,0, 0,2,0, 0,0,6},

        {0,6,0, 0,0,0, 2,8,0},
        {0,0,0, 4,1,9, 0,0,5},
        {0,0,0, 0,8,0, 0,7,9}
    };

        // To‘liq yechim (tekshiruv / test uchun)
        public static readonly int[,] OsonYechim =
        {
        {5,3,4, 6,7,8, 9,1,2},
        {6,7,2, 1,9,5, 3,4,8},
        {1,9,8, 3,4,2, 5,6,7},

        {8,5,9, 7,6,1, 4,2,3},
        {4,2,6, 8,5,3, 7,9,1},
        {7,1,3, 9,2,4, 8,5,6},

        {9,6,1, 5,3,7, 2,8,4},
        {2,8,7, 4,1,9, 6,3,5},
        {3,4,5, 2,8,6, 1,7,0}
    };
        public static int[,] Jadval = new int[9, 9];

        public static int[,] SudokuTuz(int tur)
        {

            int[,] jadval = new int[9, 9];
            JadvalniTuldir(jadval);
            if (tur == 1) KataklarniOchirish(jadval, 40);
            else if (tur == 2) KataklarniOchirish(jadval, 35);
            else KataklarniOchirish(jadval, 30);

            Jadval = jadval;


            return jadval;

        }

        private static bool JadvalniTuldir(int[,] jadval)
        {
            for (int qator = 0; qator < 9; qator++)
            {
                for (int ustun = 0; ustun < 9; ustun++)
                {
                    if (jadval[qator, ustun] == 0)
                    {
                        var sonlar = Enumerable.Range(1, 9)
                                               .OrderBy(x => Guid.NewGuid())
                                               .ToList();

                        foreach (var son in sonlar)
                        {
                            if (JoylashMimkinmi(jadval, qator, ustun, son))
                            {
                                jadval[qator, ustun] = son;

                                if (JadvalniTuldir(jadval))
                                    return true;

                                jadval[qator, ustun] = 0;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool JoylashMimkinmi(int[,] jadval, int qator, int ustun, int son)
        {
            for (int u = 0; u < 9; u++)
            {
                if (u == ustun) continue;
                if (jadval[qator, u] == son) return false;
            }
            for (int q = 0; q < 9; q++)
            {
                if (q == qator) continue;
                if (jadval[q, ustun] == son) return false;
            }
            int blokQatorBoshi = (qator / 3) * 3;
            int blokUstunBoshi = (ustun / 3) * 3;
            for (int q = blokQatorBoshi; q < blokQatorBoshi + 3; q++)
            {
                for (int u = blokUstunBoshi; u < blokUstunBoshi + 3; u++)
                {
                    if (q == qator && u == ustun) continue;
                    if (jadval[q, u] == son) return false;
                }
            }
            return true;
        }



        private static void KataklarniOchirish(int[,] jadval, int BerilganKatakSoni)
        {
            int uchiriladiganKatakSoni = 81 - BerilganKatakSoni;
            Random tasodifiy = new Random();

            while (uchiriladiganKatakSoni > 0)
            {
                int qator = tasodifiy.Next(0, 9);
                int ustun = tasodifiy.Next(0, 9);
                if (jadval[qator, ustun] != 0)
                {
                    jadval[qator, ustun] = 0;
                    uchiriladiganKatakSoni--;
                }
            }
        }
    }
}
