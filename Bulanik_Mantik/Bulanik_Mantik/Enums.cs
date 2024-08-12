using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulanik_Mantik
{
    public static class Enums
    {

        public enum Ciktilar
        {
            Donushizi,
            Sure,
            Deterjan
        }
       
   
        public enum Girdiler
        {
            Hassaslık,
            Miktar,
            Kirlilik

        }
        public enum Hassaslık
        {
            hassas,
            orta,
            sağlam
        }

        public enum Miktar
        {
            küçük,
            orta,
            büyük
        }
        public enum Kirlilik
        {
            küçük,
            orta,
            büyük
        }
        public enum DonusHizi
        {
            hassas,
            normal_hassas,
            orta,
            normal_güclü,
            güçlü
        }
        public enum Sure
        {
            kısa,
            normal_kisa,
            orta,
            normal_uzun,
            uzun
        }
        public enum Deterjan
        {
            cok_az,
            az,
            orta,
            fazla,
            cok_fazla
        }
    }
}
