using System;

namespace MonefyApplication
{
    class Program
    {
        //static List<string> Categories = new List<string>();
        static void Main(string[] args)
        {
            Account def_account = new Account("Credit Card", 0);
            App app = new App(def_account);
            app.First_View();
             
        }
    }
}
