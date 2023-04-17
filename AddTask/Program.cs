using System;

namespace AddTask
{
    /*
    Створіть власний клас, об’єкти котрого будуть займати багато місця в пам’яті 
    (наприклад, в коді класу буде присутній великий масив) і реалізуйте для цього класу формалізований шаблон очищення.
     */
    class MyClass : IDisposable
    {
        private bool disposed = false;

        public void Dispose()
        {
            CleanUp(true);
            GC.SuppressFinalize(this);
        }

        ~MyClass()
        {
            Console.WriteLine("Finalise");
            CleanUp(false);
        }

        private void CleanUp(bool clean)
        {
            if (!this.disposed)
            {
                if (clean)
                {
                    Console.Write("Освобождение ресурсов");

                    for (int i = 0; i < 40; i++)
                    {
                        Console.Write("F");
                    }
                }
                Console.WriteLine("Finalized");
            }
            this.disposed = true;
        }
    }

    class Program
    {
        static void Main()
        {
            using (var myClass = new MyClass())
            {
                Console.WriteLine(myClass);
            }
            Console.WriteLine(new string('-', 20));


            var myClass2 = new MyClass();
            Console.WriteLine(myClass2);

            myClass2.Dispose();
            myClass2.Dispose();
            myClass2.Dispose();
            myClass2.Dispose();

            var myClass3 = new MyClass();

            Console.ReadKey();
            Console.WriteLine("Press any key to dispose");
        }
    }
}

