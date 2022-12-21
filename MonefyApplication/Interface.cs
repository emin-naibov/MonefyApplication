using System;
using System.Threading;

namespace MonefyApplication
{
    static class Interface
    {
        //flexible Menu method
        public static string MenuNavigation(string[] str, int cursor_begin, int cursor_end)
        {
            int choose = 0;
            Console.CursorVisible = false;
            while (true)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (choose == i) Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(str[i]);
                    Console.ResetColor();
                }
                Console.SetCursorPosition(cursor_begin, cursor_end);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        if (choose - 1 >= 0) choose--;
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        if (choose + 1 < str.Length) choose++;
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        Console.Clear();
                        return str[choose];
                }
            }
        }
        public static void Usual_Console_Operations()
        {
            Thread.Sleep(10000);
            Console.Clear();
           // First_View();
        }
            
            

  }
}
