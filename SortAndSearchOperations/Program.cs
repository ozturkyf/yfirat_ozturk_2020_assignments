using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAndSearchOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            #region Dizi Oluşturma
            int size = 100000;
            Random rnd = new Random();
            int[] arr = new int[size];
            var s1 = Stopwatch.StartNew();
            for (int i = 0; i < size; i++)
            {
                arr[i] = rnd.Next(1, size);

            }
            s1.Stop(); 
            
            Console.WriteLine("Dizi oluşturma süresi {0} ms", s1.Elapsed.TotalMilliseconds.ToString());
            #endregion
            Console.WriteLine("--------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            #region BubbleSort
            var s2 = Stopwatch.StartNew();
            BubbleSort(arr);
            s2.Stop();
            Console.WriteLine("Dizi sort süresi {0} ms", s2.Elapsed.TotalMilliseconds.ToString());
            #endregion
            Console.WriteLine("--------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            #region BinarySearch
            int searchElement = 10;
            var s3 = Stopwatch.StartNew();
            int result = binarySearch(arr, searchElement);
            if (result == -1)
            {
                Console.WriteLine("Aranan eleman bulunamadı: " + -1);
            }
            else
            {
                Console.WriteLine("Aranan eleman index no:  " + result);
            }
            s3.Stop();
            Console.WriteLine("Dizi arama süresi {0} ms", s3.Elapsed.TotalMilliseconds.ToString()); 
            #endregion
            Console.ReadLine();
        }

        public static void BubbleSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                bool deg = true;
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int tmp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = tmp;
                        deg = false;
                    }
                }

                if (deg)
                {
                    break;
                }
            }
        }

        public static int binarySearch(int[] arr, int deger)
        {
            int ilk = 0;
            int son = arr.Length - 1;

            for (int i = 0; i < arr.Length - 1; i++)
            {
                int ortanca = ilk + (son - ilk) / 2;

                if (arr[ortanca] == deger)
                {
                    return ortanca;
                }

                if (arr[ortanca] < deger)
                {
                    ilk = ortanca + 1;
                }
                else
                {
                    son = ortanca - 1;
                }
            }
            return -1;
        }
    }
}
