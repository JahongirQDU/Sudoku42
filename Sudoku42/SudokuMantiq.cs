using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku42
{
    internal class SudokuMantiq
    {
        private readonly int[,] maydon =new int[9,9];

        public void MaydonYuklash(int[,] boshlangich)
        {
            for (int qator = 0; qator < 9; qator++)
                for (int ustun = 0; ustun < 9;ustun++) 
                    maydon[qator, ustun]= boshlangich[qator,ustun];
        }


        public void QiymatSet(int qator, int ustun, int qiymat)
        {
            maydon[qator,ustun]=   qiymat;
        }
        public int QiymatGet(int qator, int ustun)
        { 
            return  maydon[qator,ustun];
        }


        public bool QatordaXatoBormi(int  qator, int ustun,int qiymat)
        {
            for (int i = 0; i < 9; i++)
                if (i != ustun && maydon[qator, i] == qiymat) return true;
            return false;
        }

        public bool UstundaXatoBormi(int qator, int ustun, int qiymat)
        {
            for (int i = 0; i < 9; i++)
                if (i != qator && maydon[i, ustun] == qiymat) return true;
            return false;
        }

        public bool BlokdaXatoBormi(int qator, int ustun, int qiymat)
        {
            int qatorBoshi = (qator / 3) * 3;
            int ustunBoshi = (ustun / 3) * 3;

            for(int i=qatorBoshi;i<qatorBoshi+3;i++)
                for(int j=ustunBoshi;j<ustunBoshi+3;j++)
                {
                    if (i == qator && j == ustun) continue;
                    if (maydon[i, j] == qiymat) return true;
                }
            return false;
        }

        public bool XatoBormi(int qator, int ustun, int qiymat)
        {
            return QatordaXatoBormi(qator, ustun, qiymat) ||
                    UstundaXatoBormi(qator, ustun, qiymat) ||
                    BlokdaXatoBormi(qator, ustun, qiymat);
        }

        public bool GalabaBormi()
        {
            for(int qator=0;qator<9;qator++)
                for(int ustun=0;ustun<9;ustun++)
                {
                    if (maydon[qator, ustun] == 0) return false;
                    if (XatoBormi(qator, ustun, maydon[qator, ustun])) return false;
                }

            return true;
        }


    }
}
