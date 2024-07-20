using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Libb
{
    internal class DosyaIslemleri
    {
        public string kutuphaneYolu = "C:\\Users\\User\\Desktop\\kutuphanee\\Libb\\Libb\\Kutuphane";
        public string kullaniciYolu = "C:\\Users\\User\\Desktop\\kutuphanee\\Libb\\Libb\\Kullanicilar";
        public string yetkiliYolu = "C:\\Users\\User\\Desktop\\kutuphanee\\Libb\\Libb\\Yetkililer";
        public string kitapOduncYolu = "C:\\Users\\User\\Desktop\\kutuphanee\\Libb\\Libb\\KitapOdunc";
        public string kitapYazmaYolu = "C:\\Users\\User\\Desktop\\kutuphanee\\Libb\\Libb\\KitapYazma";

        public bool varMiKullanici()
        {
            Menu menu = new Menu();
            bool kontrol = false;

            if(Directory.Exists($"{kullaniciYolu}\\{Menu.kullaniciAdi}_{Menu.sifre}"))
                kontrol = true;
            return kontrol;
        }
        public void kaydet()
        {
            Menu menu = new Menu();
            Directory.CreateDirectory($"{kullaniciYolu}\\{Menu.kullaniciAdi}_{Menu.sifre}");
        }
        public bool varMiYetkili()
        {
            Menu menu = new Menu();
            bool kontrol = false;
            if(Directory.Exists($"{yetkiliYolu}\\{Menu.kullaniciAdi}_{Menu.sifre}"))
                kontrol = true;
            return kontrol;
        }
        public void oduncIstek()
        {
            Menu menu = new Menu();
            string[] directories = Directory.GetDirectories(kutuphaneYolu);

            for (int i = 0; i < directories.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(directories[i])}");
            }

            Console.Write("Bir klasör seçiniz (numara giriniz): ");
            int numara = int.Parse(Console.ReadLine());

            if (numara > 0 && numara <= directories.Length)
            {
                string yol = directories[numara - 1];
                Console.WriteLine($"Seçilen Klasör: {yol}");

                string hedefYol = Path.Combine(kitapOduncYolu, $"{Path.GetFileName(yol)}_{Menu.kullaniciAdi}_{Menu.sifre}");

                Directory.Move(yol, hedefYol);
                Console.WriteLine("İsteğiniz yetkiliye iletildi");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Geçersiz seçim. Programdan çıkılıyor.");
            }
        }
        public void kitapIadesi()
        {
            Menu menu = new Menu();
            string[] directories = Directory.GetDirectories($"{kullaniciYolu}\\{Menu.kullaniciAdi}_{Menu.sifre}");

            for (int i = 0; i < directories.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(directories[i])}");
            }

            Console.Write("Bir kitap seçiniz (numara giriniz): ");
            int numara = int.Parse(Console.ReadLine());

            if (numara > 0 && numara <= directories.Length)
            {
                string yol = directories[numara - 1];
                Console.WriteLine($"Seçilen kitap: {yol}");

                string hedefYol = Path.Combine(kutuphaneYolu, Path.GetFileName(yol));
                if (Directory.Exists(hedefYol))
                {
                    Console.WriteLine("Hedef konumda aynı isimde bir klasör zaten mevcut. İşlem iptal edildi.");
                }
                else
                {
                    Directory.Move(yol, hedefYol);
                    Console.WriteLine("Kitap iadesi sağlandı.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Geçersiz seçim. Programdan çıkılıyor.");
            }
        }
        public void kitabiYaz() 
        {
            string yol = $"{kitapYazmaYolu}\\{Kitap.kitapAd}_{Kitap.yazar}.txt";

            using(StreamWriter streamWriter = new StreamWriter(yol))
            {
                streamWriter.WriteLine(Kitap.icerik);
            }
        }
        public void kitabıYukle()
        {
            Kitap kitap = new Kitap();
            string yol = $"{kitapYazmaYolu}\\{Kitap.kitapAd}_{Kitap.yazar}.txt";
            using(StreamReader streamReader = new StreamReader(Kitap.yol))
            {
                using(StreamWriter writer = new StreamWriter(yol))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string satir = streamReader.ReadLine();
                        writer.WriteLine(satir);
                    }
                }
            }

        }
        public void istekKutusu()
        {
            string[] files = Directory.GetFiles(kitapYazmaYolu);

            if (files.Length == 0)
            {
                Console.WriteLine("Kaynak klasörde hiç dosya yok.");
                return;
            }

            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
            }

            Console.Write("Bir dosya seçiniz (numara giriniz): ");
            int numara = int.Parse(Console.ReadLine());

            if (numara < 1 || numara > files.Length)
            {
                Console.WriteLine("Geçersiz seçim. Programdan çıkılıyor.");
                return;
            }

            string secilenYol = files[numara - 1];
            string dosyaAdi = Path.GetFileNameWithoutExtension(secilenYol);

            string icerik = File.ReadAllText(dosyaAdi);
            Console.WriteLine($"Seçilen dosyanın içeriği:\n{icerik}");

            Console.Write("Bu dosya onaylanıyor mu? (E/H): ");
            string onay = Console.ReadLine();

            if (onay.ToUpper() == "E")
            {
                string yeniYol = Path.Combine($"{kutuphaneYolu}", dosyaAdi);
                Directory.CreateDirectory(yeniYol);

                string[] kelimeler = icerik.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int kelimeSayisi = 0;
                int sayfaSayisi = 1;
                StringWriter writer = new StringWriter();

                foreach (string word in kelimeler)
                {
                    writer.Write(word + " ");
                    kelimeSayisi++;

                    if (kelimeSayisi == 200)
                    {
                        File.WriteAllText(Path.Combine(yeniYol, $"Sayfa{sayfaSayisi}.txt"), writer.ToString());
                        writer.GetStringBuilder().Clear();
                        kelimeSayisi = 0;
                        sayfaSayisi++;
                    }
                }

                if (kelimeSayisi > 0)
                {
                    File.WriteAllText(Path.Combine(yeniYol, $"Sayfa{sayfaSayisi}.txt"), writer.ToString());
                }

                File.Delete(secilenYol);
                Console.WriteLine("Kitap kütüphaneye eklendi.");
            }
            else
            {

                File.Delete(secilenYol);
                Console.WriteLine("Kitap silindi.");
            }
        }
        public void kitapVer()
        {
            string[] files = Directory.GetFiles(kitapOduncYolu);

            if (files.Length == 0)
            {
                Console.WriteLine("Kaynak klasörde hiç dosya yok.");
                return;
            }

            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
            }

            Console.Write("Bir dosya seçiniz (numara giriniz): ");
            int numara = int.Parse(Console.ReadLine());

            if (numara < 1 || numara > files.Length)
            {
                Console.WriteLine("Geçersiz seçim. Programdan çıkılıyor.");
                return;
            }

            string secilenYol = files[numara - 1];
            string dosyaAdi = Path.GetFileNameWithoutExtension(secilenYol);

            string[] parcalar = secilenYol.Split('_');
            string kitapAd = parcalar[0];
            string yazar = parcalar[1];
            string kullaniciAdi = parcalar[2];
            string sifre = parcalar[3];

            string yolHedef = $"C:\\Users\\User\\Desktop\\kutuphanee\\Libb\\Libb\\Kullanicilar\\{kullaniciAdi}_{sifre}";
            string yolKaynak = $"C:\\Users\\User\\Desktop\\kutuphanee\\Libb\\Libb\\KitapOdunc\\{secilenYol}";

            Directory.Move(yolKaynak,yolHedef);
        }
    }
}
