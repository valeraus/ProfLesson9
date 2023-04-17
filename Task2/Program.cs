using System;
using System.Threading;

namespace Task2
{
    /*
    Створити клас, який дозволяє стежити за ресурсами, що використовуються програмою.
    Використовуйте його для моніторингу роботи програми, а саме: 
    користувач може вказати прийнятні рівні споживання ресурсів (пам'яті), 
    а методи класу дадуть попередження, коли кількість фактично використовуваних ресурсів наближається до максимально допустимого рівня.
     */
    class MonitorMemory
    {
        readonly int memoryLimit;

        public MonitorMemory(int memoryLimit)
        {
            this.memoryLimit = memoryLimit;
        }

        bool IsMemoryLimitExceeded()
        {
            return this.memoryLimit < GC.GetTotalMemory(false);
        }

        public void WarnIfMemoryLimitExceeded(object errorMessage)
        {
            if (IsMemoryLimitExceeded())
            {
                Console.WriteLine("{0}", errorMessage);
            }
        }

    }

    class LargeObject
    {
        int[] array = new int[100000000]; // 100 000 000 Б * 4 = 400 000 000 Б = 390 625 КБ = 381 МБ

        public void Method(int i)
        {
            Console.WriteLine(i);
        }
    }

    class Program
    {
        static void Main()
        {

            Timer timer = new Timer(new MonitorMemory(100000000).WarnIfMemoryLimitExceeded, "Warning memory out", 0, 200);

            LargeObject[] array = new LargeObject[1000];

            for (int i = 0; i < array.Length; i++)
            {
                new LargeObject().Method(i);
            }

            Console.ReadKey();
        }
    }
}
