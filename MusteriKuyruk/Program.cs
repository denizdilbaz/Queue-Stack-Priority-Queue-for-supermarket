using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusteriKuyruk
{
    class Musteri
    {

        public string musteri_adi;
        public int urun_sayisi;

        public Musteri(string musteri_adi, int urun_sayisi)
        {
            this.musteri_adi = musteri_adi;
            this.urun_sayisi = urun_sayisi;

        }
        public override string ToString()
        {
            return  "Müşteri Adı:" + musteri_adi + "\t,\t" + "Ürün sayısı: " + urun_sayisi ;
        }
    }
    class Stack
    {

        private int boyut;    // dizimizin uzunluğunu belirlemek için kullanacağımız değişken
        private Musteri[] dizim;    // dizimiz musteri tipinde eleman alacağı için musteri tipinde dizi olusturduk
        private int üst;     // yığında üste ekleme yaptığımız ve silerken de üstten sildiğimiz için yığının en üstünü belirtmek için bir değişken belirledik


        public Stack(int boyut)
        {  // stack için constructor metotumuz . Olusturken dizinin boyutunu belirlememiz gerektiği için int tipinde parametre alıyor.
            this.boyut = boyut;
            dizim = new Musteri[boyut]; // maxSize boyutunda musteri tipinde dizimiz için bellekte yer açılıyor
            üst = -1;    // stack boş durumda olduğu için top değişkeninin değeri -1 
        }
        public void push(Musteri m)
        {   // push metotuna eklemek istediğimiz musteri tipindeki değeri parametre olarak giriyoruz.
            dizim[++üst] = m;           // ekleme metotu stack'in en üstüne ekleme yapmalı. Top değişkeninin değerini -1'den başlatmıştık o yüzden önce 1 arttırıp ondan sonra ekleme yapılıyor.
        }

        public Musteri pop()
        {          // Silerken bir parametre girmiyoruz çünkü en üstten sileceğiz. top değerini siliyoruz ve değerini 1 azaltıyoruz.

            return dizim[üst--];
        }
        public Musteri EnUstteki()
        {             // stackte en üstteki değeri döndüren metot

            return dizim[üst];
        }

        public bool BosMu()
        {             // top == -1 ise yani stackin boş ise true döndürür.

            return (üst == -1);
        }

        public bool DoluMu()
        {           // eğer stack eleman sayısı maxSize ise doludur ve true döndürür.

            return (üst == boyut);
        }
    }
    class Kuyruk
    {
        private int maxSize;
        private Musteri[] kuyruk_dizisi;
        private int ön;     //front
        private int arka;   //rear
        private int eleman_sayisi;


        public Kuyruk(int maxSize)
        {
            this.maxSize = maxSize;
            kuyruk_dizisi = new Musteri[maxSize];
            ön = 0;             // çıkarılacak elemanın konumunu tutucak değişken
            arka = -1;          // eklenecek elemanın konumunu tutacak değişken
            eleman_sayisi = 0;

        }

        public void ekle(Musteri m)       // ekleme metodumuz musteri tipinde parametre almaktadır.
        {
            if (arka == maxSize - 1)        // eğer eklenecek elemanımızın konumu dizi boyutunu aşacaksa tekrardan başlangıça dönüyoruz. 
                arka = -1;
            kuyruk_dizisi[++arka] = m;    // arkayı bir arttırıp eklenecek elemanı dizinin arka değerinin konumuna ekliyoruz.
            eleman_sayisi++;            // eleman sayısını 1 arttırdık.
        }
        public Musteri sil() // silme metotumuz parametre almıyor
        {
            Musteri temp = kuyruk_dizisi[ön++]; // silinecek değerimizi musteri tipinde olusturduğumuz bir değişkene atıyoruz.
            if (ön == maxSize)          //eğer dizinin sonundaki bir değeri silersek tekrardan dizinin en başına dönüyoruz.)
                ön = 0;
            eleman_sayisi--;         // eleman sildiğimiz için eleman sayısını bir azaltıyoruz
            return temp;            // sildiğimiz değeri döndürüyoruz.
        }                       // görüldüğü üzere bu metotta elemanı diziden çıkartma yapıyoruz ama pointerlarımızı değiştiriyoruz ve eğer bu çıkarılan elemanın yerine 
                                //eleman eklenecek olursa üzerine yazıyoruz ve dizinin bellekte ayrılmış o alanının yeni değeri eklenen o eleman oluyor.
                                // eğer list yapısını kullansaydık üstüne yazma yerine listeden silme işlemi yapabilirdik.
        public Musteri Peek()         // peek metotu bize ilk çıkarılacak elemanı döndürür
        {
            return kuyruk_dizisi[ön];
        }
        public bool isEmpty()       // eleman sayısı 0'sa kuyruk boştur ve isEmpty metotu bize true değerini döndürür
        {
            return (eleman_sayisi == 0);
        }
        public bool isFull()            // eğer eleman sayısı dizinin boyutuna eşitse yani dolu olduğunda isFull metotu true değerini döndürür
        {
            return (eleman_sayisi == maxSize);
        }
    }

    class OncelikliKuyruk
    {

        public List<Musteri> liste; // müşteri tipinde elemanları tutacağımız listemizi oluşturduk ama bellekte yer açmadık.

        public OncelikliKuyruk()
        {

            liste = new List<Musteri>();        // oluşturduğumuz liste için bellekte yer açtık.
        }


        public void ekle(Musteri musteri)
        {

            liste.Add(musteri);         // ekleme listenin sonuna yanı o(1) zamanlı yapılacağı için ekle metotunda müşteri tipinde girilen parametreyi liste sonuna ekliyoruz.
        }

        public Musteri sil()
        {

            int max = liste.ElementAt(0).urun_sayisi, maxIndex = 0; // listenin ilk elemanının urun sayısını max değeri olarak ve ilk elemanın konumu yani 0 'ı maximumun konumu olarak aldık.
            for (int i = 1; i < liste.Count; ++i)               //  max değeri 0 aldığımız için i'yi 1den başlattık ve liste eleman sayısı kadar dolaşacak
            {
                if (liste.ElementAt(i).urun_sayisi > max)           // eğer listenin i değerindeki elemanın urun sayısı max değerinden fazlaysa yeni max eleman listenin i. elemanı oluyor
                {                                                   // maximumun konumu da i değeri oluyor.
                    max = liste.ElementAt(i).urun_sayisi;
                    maxIndex = i;
                }
            }
            Musteri m = liste[maxIndex];            // siliceğimiz için değerini kaybetmemek adına oluşturduğumuz müşteri tipindeki değişkene atama yapıyoruz.
            liste.RemoveAt(maxIndex);               // listeden siliyoruz
            return m;                               // değeri döndürüyoruz

        }

        public bool bosMu()                 // eğer listede eleman yoksa true değerini döndürüyor.
        {

            return (liste.Count == 0);
        }
    }

    class ArtanOncelikliKuyruk
    {

        public List<Musteri> liste;         // müşteri nesnelerini tutacak bir liste tanımladık ama bellekte yer açmadık.    
        public int toplam = 0;              // işlem sürelerini hesaplamada kullanacağımız 3 adet int tipinde değişkeni oluşturduk.
        public int toplamlarin_toplami = 0;
        public int sayac = 0;
        public List<Musteri> yedek;         // sure hesaplamada liste'nin değerlerinde değişiklik yapmamak için yedek bir liste oluşturduk ama bellekte yer açmadık.

        public ArtanOncelikliKuyruk()
        {

            liste = new List<Musteri>();        // parametre almayan yapılandırıcı metotta liste için bellekte yer açtık.

        }


        public void ekle(Musteri musteri)
        {

            liste.Add(musteri);             // ekleme yeri için herhangi bir sınırlama olmadığ için yine sona eklemeli yaptım. Müşteri tipindeki parametre listenin sonuna ekleniyor.

        }

        public Musteri sil()
        {

            int min = liste.ElementAt(0).urun_sayisi, minIndex = 0;     // listenin ilk elemanının urun sayısını en küçük olarak aldım. Ve ilk eleman olduğu için en küçüğün indeksini 0 aldım.
            for (int i = 1; i < liste.Count; ++i)                       // for döngüsü sayesinde bütün listedeki elemanlar ile en küçük değer karşılaştırılıyor. Eğer karşılaştırılan
            {                                                           // değer en küçükten küçükse artık en küçük değişkenimizin değeri o oluyor ve en küçüğün indeski i değeri oluyor
                if (liste.ElementAt(i).urun_sayisi < min)
                {
                    min = liste.ElementAt(i).urun_sayisi;
                    minIndex = i;
                }
            }
            Musteri m = liste[minIndex];            // listeden silme işlemi yapılacağı için silinecek elemanın değerleri kaybolmaması için değer bir değişkene atanıyor

            liste.RemoveAt(minIndex);               // en küçüğün indeksini listeden siliyoruz.
            return m;                               // koruduğumuz elemanı da döndürüyoruz.

        }

        public bool bosMu()                     // listede eleman yoksa true değeri döndürücek
        {

            return (liste.Count == 0);
        }

        public double sure()                // ortalama işlem süresini hesaplayan double tipinde değer döndüren bir metot yazdık
        {

            yedek = new List<Musteri>(liste);       // en başta oluşturduğumuz yedek listesine , listemizi kopyaladık.

            while (yedek.Count != 0)                // yedek listemizde eleman kalmayana kadar döngümüz devam edicek.
            {
                int kucuk = yedek[0].urun_sayisi, kucuk_indeks = 0;
                for (int i = 1; i < yedek.Count; i++)
                {
                    if (yedek[i].urun_sayisi < kucuk)
                    {
                        kucuk = yedek[i].urun_sayisi;
                        kucuk_indeks = i;
                    }
                }
                toplam += kucuk;  //toplam minimum değerlerin toplamı olucak
                toplamlarin_toplami += toplam;  // toplamların toplamı ise sure işleminde bize lazım olan değişken. ve toplamların toplanması ile oluşuyor.
                yedek.RemoveAt(kucuk_indeks);       // urun sayısı olarak en küçük olan elemanı yedek listesinden siliyor.
                sayac++;                        // sayac yani silinen eleman sayısını 1 arttırıyoruz.
            }
            double sure = (double)toplamlarin_toplami / sayac;  // double tipinde süre ise toplamların toplamının sayaca bölünmesi ile bulunuyor.
            return sure;                            // sure değişkenini döndürüyoruz.
        }



    }

    class Program
    {
        public static void olusturVeYazdir(String[] MusteriAdi, int[] UrunSayisi)
        {
            ArrayList arraylistim = new ArrayList(); // genericlistleri tutmak için arraylist oluşturduk

            int musteri_sayaci, genericListsayaci = 0;  // müşteri sayısını ve genericlist sayısını tutmak için değişken oluşturduk.

            Musteri Musteri;    // musteri tipinde bir değişken tanımladık.

            List<Musteri> genericList;      // müşteri tipinde değer alıcak genericlist tanımladık ama bellekte yerini açmadık.
            Random random = new Random();  // eğer randomu for döngüsü içinde tanımlamış olsaydım bütün genericlistlerin boyutu birbirine eşit olmuş olacaktı.
            for (musteri_sayaci = 0; musteri_sayaci < MusteriAdi.Length;)   // döngümüz müşterilerin hepsini aldığımız zaman biticek.
            {

                genericList = new List<Musteri>();      // genericlistimiz için bellekte yer açtık.

                int genericListuzunlugu = random.Next(1, 5);      // randomla 1-5 arasında bir sayı aldık.
                for (int i = 0; i < genericListuzunlugu; i++)     // for döngüsü yardımı ile random metodu ile aldığımız sayı kadar kişiyi genericliste ekleyeceğiz.
                {
                    Musteri = new Musteri(MusteriAdi[musteri_sayaci], UrunSayisi[musteri_sayaci]);  // müşteri adı ve urun sayısı dizilerinden müsteri sayacının gösterdiği elemanları alıp müşteri sınıfı nesnesi oluşturuyoruz.
                    genericList.Add(Musteri);    //oluşturduğumuz müşteriyi genericliste ekliyoruz
                    musteri_sayaci++;           // sayacı 1 arttırıyoruz
                    if (musteri_sayaci == MusteriAdi.Length)        // eğer sayacımız müşteri sayısına denk olursa döngüyü bitiriyoruz.
                    {
                        break;
                    }
                }
                arraylistim.Add(genericList);       // oluşturduğumuz genericlisti arrayliste ekliyoruz.
                genericListsayaci++;                // genericlist sayacımızı 1 arttırıyoruz.

            }
            foreach (List<Musteri> item in arraylistim)     // foreach yardımı ile her müşterinin adını ve urun sayısını ekrana yazdırıyoruz.
            {
                foreach (Musteri item1 in item)
                {
                    Console.WriteLine("{0},{1}", item1.musteri_adi, item1.urun_sayisi);
                }
                Console.WriteLine();

            }
            Console.WriteLine("Generic Liste sayısı: " + genericListsayaci);  // genericlist sayısı sayacımızın gösterdiği değere denk geliyor
            Console.WriteLine("Ortalama Eleman Sayısı: " + (double)musteri_sayaci / arraylistim.Count);  // ortalama eleman sayısı da  müşteri sayısı / generic list sayısına eşit
            Console.WriteLine();
        }


        static void Main(string[] args)
        {
            String[] MusteriAdi = { "Ali", "Merve", "Veli", "Gülay", "Okan", "Zekiye", "Kemal", "Banu", "İlker", "Songül", "Nuri", "Deniz" };
            int[] UrunSayisi = { 8, 11, 16, 5, 15, 14, 19, 3, 18, 17, 13, 15 };
            olusturVeYazdir(MusteriAdi, UrunSayisi);


            Console.WriteLine("----------" + "Yığıt işlemi gerçekleştiriliyor" + "------------");
            Stack stack1 = new Stack(MusteriAdi.Length);

            for (int i = 0; i < MusteriAdi.Length; i++)
            {
                Musteri musteri = new Musteri(MusteriAdi[i], UrunSayisi[i]);
                stack1.push(musteri);
            }
            while (!stack1.BosMu())
            {
                Musteri mus_yaz = stack1.pop();
                Console.WriteLine(mus_yaz.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("----------" + "Kuyruk işlemi gerçekleştiriliyor" + "------------");
            Kuyruk s2 = new Kuyruk(MusteriAdi.Length);
            for (int i = 0; i < MusteriAdi.Length; i++)
            {
                Musteri musteri = new Musteri(MusteriAdi[i], UrunSayisi[i]);
                s2.ekle(musteri);
            }

            for (int i = 0; i < MusteriAdi.Length; i++)
            {
                Musteri mus_yaz = (Musteri)s2.sil();
                Console.WriteLine(mus_yaz.ToString());
            }
            Console.WriteLine();




            Console.WriteLine("----------" + "Azalan öncelikli kuyruk işlemi gerçekleştiriliyor" + "------------");
            OncelikliKuyruk s3 = new OncelikliKuyruk();
            for (int i = 0; i < MusteriAdi.Length; i++)
            {
                Musteri musteri = new Musteri(MusteriAdi[i], UrunSayisi[i]);
                s3.ekle(musteri);
            }

            for (int i = 0; i < MusteriAdi.Length; i++)
            {

                Musteri mus_yaz = (Musteri)s3.sil();
                Console.WriteLine(mus_yaz.ToString());
            }
            Console.WriteLine();



            Console.WriteLine("----------" + "Artan öncelikli kuyruk işlemi gerçekleştiriliyor" + "------------");

            ArtanOncelikliKuyruk s4 = new ArtanOncelikliKuyruk();
            for (int i = 0; i < MusteriAdi.Length; i++)
            {
                Musteri musteri = new Musteri(MusteriAdi[i], UrunSayisi[i]);
                s4.ekle(musteri);
            }
            double a = s4.sure();

            for (int i = 0; i < MusteriAdi.Length; i++)
            {

                Musteri mus_yaz = (Musteri)s4.sil();
                Console.WriteLine(mus_yaz.ToString());
                Console.WriteLine(mus_yaz.musteri_adi + " isimli müşterinin işlem süresi: " + mus_yaz.urun_sayisi);
            }
            Console.WriteLine("Tek kasa için ortalama işlem tamamlanma süresi: " + a);
            Console.ReadKey();
        }
    }
}
