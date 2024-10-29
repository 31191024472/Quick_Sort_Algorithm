using System;
using System.Diagnostics;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        int numIterations = 1;  // Đặt thành 1 lần để dễ dàng theo dõi
        double totalMilliseconds = 0;

        // Mảng cố định để dễ quan sát
        Element[] a = new Element[]
        {
            new Element { Value = -11, Index = 0 },
            new Element { Value = 12, Index = 1 },
            new Element { Value = -42, Index = 2 },
            new Element { Value = 0, Index = 3 },
            new Element { Value = 90, Index = 4 },
            new Element { Value = 68, Index = 5 },
            new Element { Value = 6, Index = 6 },
            new Element { Value = -9, Index = 7 }
        };

        Console.WriteLine("Mảng ban đầu:");
        PrintArray(a);

        Timing timing = new Timing();
        timing.StartTime();
        QuickSort(a, 0, a.Length - 1);
        timing.StopTime();

        totalMilliseconds += timing.ResultInMilliseconds();

        Console.WriteLine("\nMảng sau khi sắp xếp:");
        PrintArray(a);

        // Kiểm tra tính ổn định
        if (!IsStable(a))
        {
            Console.WriteLine("Thuật toán không ổn định.");
        }

        Console.WriteLine($"\nThời gian trung bình thuật toán chạy: {totalMilliseconds} ms");
        Console.ReadLine();
    }

    // Lớp Element để lưu giá trị và chỉ số ban đầu
    class Element
    {
        public int Value { get; set; }
        public int Index { get; set; }
    }

    // Hàm kiểm tra tính ổn định
    static bool IsStable(Element[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i].Value == array[i - 1].Value && array[i].Index < array[i - 1].Index)
            {
                return false;  // Nếu phần tử phía sau có index nhỏ hơn => không ổn định
            }
        }
        return true;
    }

    // Hàm in mảng
    static void PrintArray(Element[] array)
    {
        foreach (var elem in array)
        {
            Console.WriteLine($"Value: {elem.Value}, Original Index: {elem.Index}");
        }
        Console.WriteLine();
    }

    // Hàm Swap với kiểu Element[] và in ra khi đổi chỗ
    private static void Swap(Element[] a, int i, int j)
    {
        Console.WriteLine($"Đổi chỗ: {a[i].Value} (Index {a[i].Index}) và {a[j].Value} (Index {a[j].Index})");
        Element t = a[i];
        a[i] = a[j];
        a[j] = t;
    }

    // Chọn pivot là phần tử giữa mảng và thực hiện Partition
    private static int Partition(Element[] a, int l, int r)
    {
        int mid = (l + r) / 2;
        Swap(a, l, mid);  // Đưa pivot về vị trí đầu tiên
        Element pivot = a[l];

        Console.WriteLine($"\nChọn pivot: {pivot.Value} (Index {pivot.Index})");

        int ndx = l;
        for (int i = l + 1; i <= r; i++)
        {
            if (a[i].Value < pivot.Value || (a[i].Value == pivot.Value && a[i].Index < pivot.Index))
            {
                ndx++;
                Swap(a, ndx, i);
            }
        }
        Swap(a, ndx, l);  // Đưa pivot về đúng vị trí
        return ndx;
    }

    // Hàm QuickSort
    private static void QuickSort(Element[] a, int l, int r)
    {
        if (l < r)
        {
            int pi = Partition(a, l, r);
            QuickSort(a, l, pi - 1);
            QuickSort(a, pi + 1, r);
        }
    }

    // Lớp Timing để đo thời gian thực thi
    public class Timing
    {
        private TimeSpan startingTime;
        private TimeSpan duration;

        public Timing()
        {
            startingTime = new TimeSpan(0);
            duration = new TimeSpan(0);
        }

        public void StopTime()
        {
            duration = Process.GetCurrentProcess().Threads[0].UserProcessorTime.Subtract(startingTime);
        }

        public void StartTime()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            startingTime = Process.GetCurrentProcess().Threads[0].UserProcessorTime;
        }

        public double ResultInMilliseconds()
        {
            return duration.TotalMilliseconds;
        }
    }
}
