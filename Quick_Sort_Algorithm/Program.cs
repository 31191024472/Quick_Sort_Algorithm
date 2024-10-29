//using System;
//using System.Diagnostics;
//using System.Text;


//namespace Quick_Sort
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.InputEncoding = Encoding.UTF8;
//            Console.OutputEncoding = Encoding.UTF8;

//            int numIterations = 2000;  // Số lần chạy lại thuật toán 
//            double totalMilliseconds = 0;

//            for (int i = 0; i < numIterations; i++)
//            {
//                int[] a = new int[] { 1, 9, 77, 9, 24, 67, 213, 134, 3245, 2, 3 };  // Mảng cần sắp xếp
//                Timing timing = new Timing();

//                timing.startTime();
//                QuickSort(a, 0, a.Length - 1);
//                timing.StopTime();

//                totalMilliseconds += timing.ResultInMilliseconds();
//            }

//            Console.WriteLine($"\nThời gian trung bình thuật toán chạy 2000 trên cùng 1 phần tử : {totalMilliseconds / numIterations} ms");
//            Console.ReadLine();
//        }

//        public static void Swap(int[] a, int i, int j)
//        {
//            int t = a[i];
//            a[i] = a[j];
//            a[j] = t;
//        }

//        private static int Partition(int[] a, int l, int r)
//        {
//            int ndx = l;
//            int pivot = a[l];
//            for (int i = l + 1; i <= r; i++)
//            {
//                if (a[i] < pivot)
//                {
//                    ndx++;
//                    Swap(a, ndx, i);
//                }

//            }
//            Swap(a, ndx, l);
//            return ndx;
//        }


//        private static void QuickSort(int[] a, int l, int r)
//        {
//            if (l < r)
//            {
//                var pi = Partition(a, l, r);
//                QuickSort(a, l, pi - 1);
//                QuickSort(a, pi + 1, r);
//            }
//        }

//    }
//    public class Timing
//    {
//        private TimeSpan startingTime;
//        private TimeSpan duration;

//        public Timing()
//        {
//            startingTime = new TimeSpan(0);
//            duration = new TimeSpan(0);
//        }

//        public void StopTime()
//        {
//            duration = Process.GetCurrentProcess().Threads[0].UserProcessorTime.Subtract(startingTime);
//        }

//        public void startTime()
//        {
//            GC.Collect();
//            GC.WaitForPendingFinalizers();
//            startingTime = Process.GetCurrentProcess().Threads[0].UserProcessorTime;
//        }

//        public double ResultInMilliseconds()
//        {
//            return duration.TotalMilliseconds;
//        }
//    }

//}