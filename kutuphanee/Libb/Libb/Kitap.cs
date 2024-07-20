using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libb
{
    internal class Kitap
    {
        public static string kitapAd;
        public static string yazar;
        public static string icerik;
        public static string yol;
        public void kitapOdunc()
        {
            Menu menu = new Menu();
            DosyaIslemleri dosyaIslemleri = new DosyaIslemleri();
            dosyaIslemleri.oduncIstek();
            menu.kullaniciMenu();
        }
        public void kitapIade()
        {
            Menu menu = new Menu();
            DosyaIslemleri dosyaIslemleri = new DosyaIslemleri();
            dosyaIslemleri.kitapIadesi();
            menu.kullaniciMenu();
        }
        public void kitapYaz()
        {
            Menu menu = new Menu();
            DosyaIslemleri dosyaIslemleri = new DosyaIslemleri();
            Console.WriteLine("Yazmak istenen kitabın adı :");
            kitapAd = Console.ReadLine().ToLower();
            Console.WriteLine("Yazarı:");
            yazar = Console.ReadLine().ToLower();
            Console.WriteLine("Kitabın içeriğini yazmaya başlayın . . . ");
            icerik = Console.ReadLine().ToLower();
            dosyaIslemleri.kitabiYaz();
            menu.kullaniciMenu();
        }
        public void kitapYukle()
        {
            Menu menu = new Menu();
            DosyaIslemleri dosyaIslemleri = new DosyaIslemleri();
            Console.WriteLine("Yazmak istenen kitabın adı :");
            kitapAd = Console.ReadLine().ToLower();
            Console.WriteLine("Yazarı:");
            yazar = Console.ReadLine().ToLower();
            Console.WriteLine("Kitabın dosya yolunu aralarında çift eğik çizgi olacal şekilde yazınız.");
            yol = Console.ReadLine().ToLower();
            dosyaIslemleri.kitabıYukle();
            menu.kullaniciMenu();
        }
        public void kitapIstekKontrol()
        {
            Menu menu = new Menu();
            DosyaIslemleri dosyaIslemleri = new DosyaIslemleri();
            dosyaIslemleri.istekKutusu();
            menu.kullaniciMenu();
        }
        public void kitapOduncVer()
        {
            Menu menu = new Menu();
            DosyaIslemleri dosyaIslemleri = new DosyaIslemleri();
            dosyaIslemleri.kitapVer();
            menu.kullaniciMenu();
        }
    }
}
