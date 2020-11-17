using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            //Dizi Oluşturma
            //int diziBoyutu;
            //Console.Write("Dizi boyutunu giriniz: ");
            //diziBoyutu = Int32.Parse(Console.ReadLine());
            int[] arr = DiziOlustur();

            // DiziResize(arr);

            //İlk Eleman Ekle/Çıkar
            IlkElemanEkleCikar(arr);
            ////Ortadaki elemanı Ekle/Çıkar
            OrtadakiElemanEkleCikar(arr);
            ////Son Eleman Ekle/Çıkar
            SonElemanEkleCikarEx(arr);

            //Eleman Silme
            //ElemanSil(arr,0);

            //Eleman Değiştirme
            //ElemanDegistir(arr, 0, 999);

            Console.ReadLine();
        }

        public static int[] DiziOlustur()
        {
            AddedLine("Dizi Oluştur");
            var t1 = Stopwatch.StartNew();

            int[] dizi = new int[1000000];
            for (int i = 0; i < dizi.Length; i++)
            {
                dizi[i] = i + 1;
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Dizi oluşturma süresi {0} ms", t1.ElapsedMilliseconds);
            Console.ResetColor();
            return dizi;
        }

        public static void DiziResize(int[] arr)
        {
            AddedLine("Dizi Resize");
            var t1 = Stopwatch.StartNew();

            Array.Resize(ref arr, arr.Length + 1);

            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }

            Array.Resize(ref arr, arr.Length - 1);

            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Dizi oluşturma süresi {0} ms", t1.ElapsedMilliseconds);
            Console.ResetColor();
            //AddedLine();

        }

        public static void IlkElemanEkleCikar(int[] arr)
        {

            AddedLine("İlk Eleman Ekle/Çıkar");

            //Console.Write("İlk eleman değerini giriniz: ");
            // int ilkEleman;
            // ilkEleman = Int32.Parse(Console.ReadLine());
            Console.WriteLine();
            //Console.Write("İlk eleman ekleme işlemi başladı.");
            //Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            //var t1 = Stopwatch.StartNew();
            DateTime t1 = DateTime.Now;

            var addArr = new int[arr.Length + 1];

            addArr[0] = -255;//ilkEleman;

            for (int i = 0; i < arr.Length; i++)
            {
                addArr[i + 1] = arr[i];
            }
            var first = t1 - DateTime.Now;
            Console.WriteLine("Dizi ilk eleman ekleme süresi: {0} ms", first);
            DateTime t2 = DateTime.Now;
            int[] rmvArr = new int[addArr.Length - 1];

            for (int i = 0; i < rmvArr.Length; i++)
            {
                rmvArr[i] = addArr[i + 1];
            }
            var second = t2 - DateTime.Now;
            Console.WriteLine("Dizi ilk eleman çıkarma süresi: {0} ms", second);
            Console.WriteLine("Dizi ilk eleman ekleme/çıkarma süresi: {0} ms", first + second);
            Console.ResetColor();
        }

        public static void SonElemanEkleCikar(int[] arr)
        {
            AddedLine("Son Eleman Ekle/Çıkar");

            //Console.Write("Son eleman değerini giriniz: ");
            //int sonEleman;
            //sonEleman = Int32.Parse(Console.ReadLine());
            //Console.WriteLine();
            //Console.Write("Son eleman ekleme işlemi başladı.");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            //var t1 = Stopwatch.StartNew();
            var t1 = DateTime.Now;

            int[] newArr = new int[1000001];
            newArr[1000000] = -255;//sonEleman;
            for (int i = 0; i < 1000000; i++)
            {
                newArr[i] = arr[i];
            }

            int[] newArr2 = new int[1000000];

            for (int i = 0; i < 999999; i++)
            {
                newArr2[i] = newArr[i];
            }

            Console.WriteLine("Dizi son eleman ekleme/çıkarma süresi: {0} ms", t1 - DateTime.Now);
            Console.ResetColor();
        }

        public static void SonElemanEkleCikarEx2(int[] arr)
        {
            AddedLine("Son Eleman Ekle/Çıkar");

            Console.Write("Son eleman değerini giriniz: ");
            int sonEleman;
            //  sonEleman = Int32.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Son eleman ekleme işlemi başladı.");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            //var t1 = Stopwatch.StartNew();
            var t1 = DateTime.Now;
            Array.Resize(ref arr, arr.Length + 1);

            arr[arr.Length - 1] = -255;//sonEleman;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                arr[i] = arr[i];
            }

            Array.Resize(ref arr, arr.Length - 1);

            Console.WriteLine("Dizi son eleman ekleme/çıkarma süresi: {0} ms", t1 - DateTime.Now);
            Console.ResetColor();
        }

        public static void SonElemanEkleCikarEx(int[] arr)
        {
            AddedLine("Son Eleman Ekle/Çıkar");

            //Console.Write("Son eleman değerini giriniz: ");
            //int sonEleman;
            //sonEleman = Int32.Parse(Console.ReadLine());
            //Console.WriteLine();
            //Console.Write("Son eleman ekleme işlemi başladı.");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            DateTime t1 = DateTime.Now; // Stopwatch.StartNew();

            int[] newArr = new int[arr.Length + 1];
            Array.Copy(arr, 0, newArr, 0, arr.Length);
            newArr[newArr.Length - 1] = -255;//sonEleman;
            var ilk = t1 - DateTime.Now;
            Console.WriteLine("Dizi son eleman ekleme süresi: {0} ms", ilk);
            DateTime t2 = DateTime.Now;
            Array.Resize(ref newArr, newArr.Length - 1);
            var son = t2 - DateTime.Now;
            Console.WriteLine("Dizi son eleman çıkarma süresi: {0} ms", son);

            Console.WriteLine("Dizi son eleman ekleme/çıkarma süresi: {0} ms", ilk + son);
            Console.ResetColor();
        }

        public static void OrtadakiElemanEkleCikar(int[] arr)
        {
            AddedLine("Ortadaki Eleman Ekle/Çıkar");

            // Console.Write("Ortadaki eleman değerini giriniz: ");
            //int ortadakiEleman;
            int ortadakiIndex;
            // ortadakiEleman = Int32.Parse(Console.ReadLine());
            //Console.WriteLine();
            //Console.Write("Ortadaki eleman ekleme işlemi başladı.");
            Console.WriteLine();

            int size = arr.Length;
            if (size % 2 != 0)
            {
                ortadakiIndex = (size - 1) / 2;
            }
            else
            {
                ortadakiIndex = size / 2;
            }


            Console.ForegroundColor = ConsoleColor.Green;
            //var t1 = Stopwatch.StartNew();
            DateTime t1 = DateTime.Now;
            int[] addArr = new int[size + 1];

            addArr[ortadakiIndex] = -255;//ortadakiEleman;

            for (int i = 0; i < ortadakiIndex; i++)
            {
                addArr[i] = arr[i];
            }

            for (int i = ortadakiIndex + 1; i < arr.Length; i++)
            {
                addArr[i] = arr[i - 1];
            }
            var first = t1 - DateTime.Now;
            Console.WriteLine("Dizi Ortadaki eleman ekleme süresi: {0} ms", first);
            // Array.Copy(arr, 0, newArr, 0, ortadakiIndex);
            // Array.Copy(arr, ortadakiIndex, newArr, ortadakiIndex + 1, arr.Length - ortadakiIndex);
            DateTime t2 = DateTime.Now;
            var rmvArr = new int[size];

            for (int i = 0; i < ortadakiIndex; i++)
            {
                rmvArr[i] = arr[i];
            }

            for (int i = ortadakiIndex + 1; i < arr.Length; i++)
            {
                rmvArr[i] = arr[i - 1];
            }
            var second = t2 - DateTime.Now;
            Console.WriteLine("Dizi Ortadaki eleman çıkarma süresi: {0} ms",second);
            Console.WriteLine("Dizi Ortadaki eleman ekleme/çıkarma süresi: {0} ms", first + second);
            Console.ResetColor();
        }

        public static void OrtadakiElemanEkleCikarEx(int[] arr)
        {
            AddedLine("Ortadaki Eleman Ekle/Çıkar");

            Console.Write("Ortadaki eleman değerini giriniz: ");
            int ortadakiEleman;
            int ortadakiIndex;
            //ortadakiEleman = Int32.Parse(Console.ReadLine());
            //Console.WriteLine();
            //Console.Write("Ortadaki eleman ekleme işlemi başladı.");
            Console.WriteLine();

            int size = arr.Length;
            if (size % 2 != 0)
            {
                ortadakiIndex = (size - 1) / 2;
            }
            else
            {
                ortadakiIndex = size / 2;
            }


            Console.ForegroundColor = ConsoleColor.Yellow;
            var t1 = DateTime.Now;//Stopwatch.StartNew();
            int[] newArr = new int[size + 1];

            newArr[ortadakiIndex] = -255;//ortadakiEleman;
            Array.Copy(arr, 0, newArr, 0, ortadakiIndex);
            Array.Copy(arr, ortadakiIndex, newArr, ortadakiIndex + 1, arr.Length - ortadakiIndex);

            var arr3 = new int[size];

            Array.Copy(newArr, 0, arr3, 0, ortadakiIndex);
            Array.Copy(newArr, ortadakiIndex, arr3, ortadakiIndex, arr.Length - ortadakiIndex);


            Console.WriteLine("Dizi Ortadaki eleman ekleme/çıkarma süresi: {0} ms", t1 - DateTime.Now);
            Console.ResetColor();
        }

        public static void ElemanSil(int[] arr, int index)
        {
            var t1 = Stopwatch.StartNew();
            Console.WriteLine();
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
            }
            Console.WriteLine();
            Console.WriteLine();

            Array.Resize(ref arr, arr.Length);

            Array.Clear(arr, index, 1);
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Dizi eleman silme süresi {0} ms", t1.ElapsedMilliseconds);
            AddedLine();
        }

        public static void ElemanDegistir(int[] arr, int index, int yeniDeger)
        {
            var t1 = Stopwatch.StartNew();
            Console.WriteLine();
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
            }
            Console.WriteLine();
            arr[index] = yeniDeger;
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
            }
            Console.WriteLine();
            Console.WriteLine("Dizi oluşturma süresi {0} ms", t1.ElapsedMilliseconds);
            AddedLine();
        }

        public static void AddedLine(string methodName = "")
        {
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            if (methodName != "")
            {
                Console.WriteLine("Metod: {0}", methodName);
            }
        }
    }
}
