using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Libb
{
    internal class Menu
    {
        public static string kullaniciAdi = "";
        public static string sifre = "";

        public void calistir()
        {
            Console.WriteLine("1. Kullanıcı");
            Console.WriteLine("2. Görevli");
            int secim = Convert.ToInt32(Console.ReadLine());

            if (secim == 1)
            {
                kullaniciSecim();
            }
            else if (secim == 2)
            {
                gorevliGiris();
            }
            else
            {
                Console.WriteLine("Geçerli bir seçim yapınız!");
                calistir();
            }
        }
        public void kullaniciSecim()
        {
            Console.Clear();
            Console.WriteLine("1. Kayıt ol");
            Console.WriteLine("2. Giriş yap");
            int secim = Convert.ToInt32(Console.ReadLine());

            if (secim == 1)
            {
                kullaniciKayit();
            }
            else if (secim == 2)
            {
                kullaniciGiris();
            }
            else
            {
                Console.WriteLine("Geçerli bir seçim yapınız!");
                kullaniciSecim();
            }
        }
        public void kullaniciKayit()
        {
            Console.Clear();
            Console.WriteLine("Kullanıcı adınızı yazınız.(Boşluk kullanmayınız!)");
            kullaniciAdi = Console.ReadLine();
            Console.WriteLine("Şifrenizi yazınız.(Boşluk kullanmayınız!)");
            sifre = Console.ReadLine();

            DosyaIslemleri dosyaIslemleri = new DosyaIslemleri();
            if (!dosyaIslemleri.varMiKullanici())
            {
                dosyaIslemleri.kaydet();
                Console.WriteLine("Kayıt başarılı!");
                Console.WriteLine("Devam etmek için ENTER");
                Console.ReadLine();
                kullaniciGiris();
            }
            else
            {
                Console.WriteLine("Kullanıcı adı ve şifre kullanımda.");
                kullaniciKayit();
            }
        }
        public void kullaniciGiris()
        {
            Console.Clear();
            Console.WriteLine("Kullanıcı adınızı yazınız.(Boşluk kullanmayınız!)");
            kullaniciAdi = Console.ReadLine();
            Console.WriteLine("Şifrenizi yazınız.(Boşluk kullanmayınız!)");
            sifre = Console.ReadLine();
            DosyaIslemleri dosyaIslemleri = new DosyaIslemleri();
            if (dosyaIslemleri.varMiKullanici())
            {
                Console.WriteLine("Kullanıcı olarak giriş yapıldı");
                Console.ReadLine();
                kullaniciMenu();
            }
            else
            {
                Console.WriteLine("Yanlış kullanıcı adı veya şifre!");
                Console.ReadLine();
                kullaniciGiris();
            }
        }
        public void gorevliGiris()
        {
            Console.Clear();
            Console.WriteLine("Kullanıcı adınızı yazınız.(Boşluk kullanmayınız!)");
            kullaniciAdi = Console.ReadLine();
            Console.WriteLine("Şifrenizi yazınız.(Boşluk kullanmayınız!)");
            sifre = Console.ReadLine();
            DosyaIslemleri dosyaIslemleri = new DosyaIslemleri();

            if (dosyaIslemleri.varMiYetkili())
            {
                Console.WriteLine("Yetkili olarak giriş yapıldı.");
                Console.ReadLine();
                yetkiliMenu();
            }
            else
            {
                Console.WriteLine("Yanlış kullanıcı adı veya şifre");
                Console.ReadLine();
                gorevliGiris();
            }
        }
        public void kullaniciMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Kitap Ödünç Al");
            Console.WriteLine("2. Kitap İade Et");
            Console.WriteLine("3. Kitap Yaz");
            Console.WriteLine("4. Kitap Yükle");
            Console.WriteLine("5. Çıkış");
            int secim = Convert.ToInt32(Console.ReadLine());

            switch (secim)
            {
                case 1:
                    Kitap kitap1 = new Kitap();
                    kitap1.kitapOdunc();
                    kullaniciMenu();
                    break;
                case 2:
                    Kitap kitap2 = new Kitap();
                    kitap2.kitapIade();
                    kullaniciMenu();
                    break;
                case 3:
                    Kitap kitap3 = new Kitap();
                    kitap3.kitapYaz();
                    kullaniciMenu();
                    break;
                case 4:
                    Kitap kitap4 = new Kitap();
                    kitap4.kitapYukle();
                    kullaniciMenu();
                    break;
                case 5:
                    calistir();
                    break;
                default:
                    Console.WriteLine("Geçerli bir sayı tuşlayınız!");
                    kullaniciMenu();
                    break;
            }
        }
        public void yetkiliMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Kitap yükleme isteği");
            Console.WriteLine("2. Kitap ödünç alma isteği ");
            Console.WriteLine("3. Çıkış");
            int secim = Convert.ToInt32(Console.ReadLine());

            switch (secim)
            {
                case 1:
                    Kitap kitap1 = new Kitap();
                    kitap1.kitapIstekKontrol();
                    yetkiliMenu();
                    break;
                case 2:
                    Kitap kitap2 = new Kitap();
                    kitap2.kitapOduncVer();
                    yetkiliMenu();
                    break;
                case 3:
                    calistir();
                    break;
            }
        }

    }
}
