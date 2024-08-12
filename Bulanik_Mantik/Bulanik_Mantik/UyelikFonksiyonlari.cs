using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulanik_Mantik
{
    internal class UyelikFonksiyonlari
    {

        // Hassaslık için üyelik fonksiyonları
        public double HassaslikSağlam(double x)
        {
            if (x < -1.5 || x > 4)
                return 0.0;
            else if (x >= -1.5 && x < 2)
                return 1.0;
            else if (x >= 2 && x <= 4)
                return (-x/2) + 2;
            else
                return 0.0;
        }

        public double HassaslikOrta(double x)
        {
            
            if (x < 3 || x > 7)
                return 0.0;
            else if (x >= 3 && x < 5)
                return (x/2) - (1.5);
            else if (x >= 5 && x <= 7)
                return (-x/2) + 3.5;
            else
                return 0.0;
        }

        public double HassaslikHassas(double x)
        {
            if (x < 5.5 || x > 14)
                return 0.0;
            else if (x >= 5.5 && x < 8)
                return ((2 * x) / 5) - 2.2;
            else if (x >= 8 && x <= 12.5)
                return 1.0;
            else
                return 0.0;
        }

        // Miktar için üyelik fonksiyonları
        public double MiktarBüyük(double x)
        {
            if (x < 5.5 || x > 14)
                return 0.0;
            else if (x >= 5.5 && x < 8)
                return ((2 * x) / 5) - 2.2;
            else if (x >= 8 && x <= 12.5)
                return 1.0;
            else
                return 0.0;
        }

        public double MiktarOrta(double x)
        {
            if (x < 3 || x > 7)
                return 0.0;
            else if (x >= 3 && x < 5)
                return (x / 2) - (1.5);
            else if (x >= 5 && x <= 7)
                return (-x / 2) + 3.5;
            else
                return 0.0;
        }

        public double MiktarKüçük(double x)
        {
            if (x < -1.5 || x > 4)
                return 0.0;
            else if (x >= -1.5 && x < 2)
                return 1.0;
            else if (x >= 2 && x <= 4)
                return (-x / 2) + 2;
            else
                return 0.0;
        }

        // Kirlik için üyelik fonksiyonları
        public double KirlilikBüyük(double x)
        {
            if (x < 5.5 || x > 14)
                return 0.0;
            else if (x >= 5.5 && x < 8)
                return ((2 * x) / 5) - 2.2;
            else if (x >= 8 && x <= 12.5)
                return 1.0;
            else
                return 0.0;
        }

        public double KirlilikOrta(double x)
        {
            if (x < 3 || x > 7)
                return 0.0;
            else if (x >= 3 && x < 5)
                return (x / 2) - (1.5);
            else if (x >= 5 && x <= 7)
                return (-x / 2) + 3.5;
            else
                return 0.0;
        }

        public double KirlilikKüçük(double x)
        {
            if (x < -1.5 || x > 4)
                return 0.0;
            else if (x >= -1.5 && x < 2)
                return 1.0;
            else if (x >= 2 && x <= 4.5)
                return (9-(2*x))/5;
            else
                return 0.0;
        }
    }
}
