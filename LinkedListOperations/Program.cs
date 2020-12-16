using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListOperations
{
    class Program
    {
        const int size = 1000000;
        static void Main()
        {
            LinkedList<int> linkLst = DiziOlustur(size);
            IlkElemanEkleCikar(linkLst, 255);
            OrtadakiElemanEkleCikar(linkLst, 255);
            SonElemanEkleCikar(linkLst, 255);
            Console.Read();

        }

        public static LinkedList<int> DiziOlustur(int size)
        {
            AddedLine("--> Dizi Oluştur");
            Console.ForegroundColor = ConsoleColor.Green;
            var s1 = Stopwatch.StartNew();

            LinkedList<int> linkLst = new LinkedList<int>();

            for (int i = 1; i <= size; i++)
            {
                linkLst.AddLast(i);
            }
            s1.Stop();
            Console.WriteLine("Dizi oluşturma süresi {0} ms", s1.Elapsed.TotalMilliseconds.ToString());
            Console.ResetColor();
            return linkLst;
        }

        public static void IlkElemanEkleCikar(LinkedList<int> lst, int val)
        {
            AddedLine("--> İlk Eleman Ekle Çıkar");
            Console.ForegroundColor = ConsoleColor.Green;

            //İlk Eleman Ekle
            var s1 = Stopwatch.StartNew();
            lst.AddFirst(val);
            s1.Stop();
            //------------------
            double t1 = s1.Elapsed.TotalMilliseconds;
            Console.WriteLine("Dizi ilk eleman ekleme süresi: {0} ms",t1.ToString());
            
            //İlk Eleman Çıkar
            var s2 = Stopwatch.StartNew();
            lst.RemoveFirst();
            s2.Stop();
            //----------------
            double t2 = s2.Elapsed.TotalMilliseconds;
            Console.WriteLine("Dizi ilk eleman çıkarma süresi: {0} ms",t2.ToString());
            Console.WriteLine("Dizi ilk eleman ekleme/çıkarma süresi: {0} ms", t1 + t2);
            Console.ResetColor();
        }

        public static void OrtadakiElemanEkleCikar(LinkedList<int> lst, int val)
        {
            AddedLine("--> Ortadaki Eleman Ekle Çıkar");
            Console.ForegroundColor = ConsoleColor.Green;

            
            //Ortadaki eleman ekle
            var s1 = Stopwatch.StartNew();
            LinkedListNode<int> middle = lst.Find(lst.ElementAt(lst.Count / 2 - 1));
            lst.AddAfter(middle, val);
            s1.Stop();
            //------------------
            double t1 = s1.Elapsed.TotalMilliseconds;
            Console.WriteLine("Dizi Ortadaki eleman ekleme süresi: {0} ms", t1.ToString());

            //Ortadaki eleman çıkar
            var s2 = Stopwatch.StartNew();
            lst.Remove(middle.Next);
            s2.Stop();
            //---------------
            double t2 = s2.Elapsed.TotalMilliseconds;
            Console.WriteLine("Dizi Ortadaki eleman çıkarma süresi: {0} ms", t2.ToString());

            Console.WriteLine("Dizi Ortadaki eleman ekleme/çıkarma süresi: {0} ms", t1 + t2);
            Console.ResetColor();
        }

        public static void SonElemanEkleCikar(LinkedList<int> lst, int val)
        {
            AddedLine("--> Son Eleman Ekle Çıkar");
            Console.ForegroundColor = ConsoleColor.Green;

            //Son eleman ekle
            var s1 = Stopwatch.StartNew();
            lst.AddLast(val);
            s1.Stop();
            //-------------------
            double t1 = s1.Elapsed.TotalMilliseconds;
            Console.WriteLine("Dizi son eleman ekleme süresi: {0} ms", t1.ToString());

            //Son eleman çıkar
            var s2 = Stopwatch.StartNew();
            lst.RemoveLast();
            s2.Stop();
            //------------------
            double t2 = s2.Elapsed.TotalMilliseconds;
            Console.WriteLine("Dizi son eleman çıkarma süresi: {0} ms",t2.ToString());
            Console.WriteLine("Dizi son eleman ekleme/çıkarma süresi: {0} ms",t1 + t2);
            Console.ResetColor();
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
