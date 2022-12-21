using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace MonefyApplication
{
    class App
    {
        List<Account> account_types = new List<Account>();//types like cash or credit card
        List<Operation> operations = new List<Operation>();
        public string[] Main_Menu { get; set; } = { "Add Expence", "Add Income", "Spend Categories Overview","Income Categories Overview","Operations History", "Change Period", "Manage Accounts", "Manage Categories", "Export to CSV", "Exit" };
         public  void First_View()
        {
            Account account = new Account("Credit Card", 0);  //default account for experiment then will be modified
            account_types.Add(account);
            Console.WriteLine($"Total Balance: {account_types[0].Total_balance}");
            Console.WriteLine($"Expences: {account_types[0].Expences}");
            Console.WriteLine($"Incomes: {account_types[0].Incomes}");
            Console.WriteLine($"Date: {DateTime.Today.ToShortDateString()} ");
            Console.WriteLine($"Currency: {account_types[0].Currency} \n\n");
            
                string result = Interface.MenuNavigation(Main_Menu, 0, 7);//7 because main menu's view  has already 6 lines 
                switch (result)
                {
                case "Add Expence":
                    AddOperationMenu(account_types[0], "Expence");
                    break;
                case "Add Income":
                    AddOperationMenu(account_types[0], "Income");
                    break;
                case "Spend Categories Overview":
                    double spend_total = Categories.Spend_Categories.Sum(x => x.Value);
                        foreach (var item in Categories.Spend_Categories)
                        {
                            Console.WriteLine($"{item.Key} - {item.Value*100/spend_total} %");//to show percentage per category
                        }
                    //Thread.Sleep(5000);
                    //Console.Clear();
                    //First_View();
                        break;
                case "Income Categories Overview":
                double income_total = Categories.Income_Categories.Sum(x => x.Value);
                foreach (var item in Categories.Income_Categories)
                    {
                        Console.WriteLine($"{item.Key} - {item.Value*100/income_total} %");
                    }
                //Thread.Sleep(5000);
                //Console.Clear();
                //First_View();
                break;
                case "Operations History":
                operations.Sort((x, y) => DateTime.Compare(x.Operation_Date, y.Operation_Date));//sort opeartions by date using lambda expression
                foreach (var item in operations)
                    {
                    //dont use .ToString() because I want Operation Type to has another foreground color
                    Console.Write($"{item.Operation_Date} \n Account: {item.Acc.Name} \n Type:");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(item.Operation_Type);
                    Console.ResetColor();
                    Console.WriteLine($" Amount: {item.Amount}{item.Currency}\n Category: {item.Category}"); 
                    Console.WriteLine();
                    }
                //Thread.Sleep(10000);
                //Console.Clear();
                //First_View();
                break;
                default:
                    break;
                }
            Thread.Sleep(10000);
            Console.Clear();
            First_View();
        }
        public void AddOperationMenu(Account account,string type)
        {
            List<string> categs = new List<string>();
            Console.Write("Amount: ");
            double amount = double.Parse(Console.ReadLine());
            string category="";
            Console.WriteLine("Choose category: ");
            Console.WriteLine();
            if (type == "Expence")
            {
                foreach (var item in Categories.Spend_Categories)
                {
                    categs.Add(item.Key);
                }
                category = Interface.MenuNavigation(categs.ToArray(), 0, 3);//as menunavigation takes string[]
                account.Expences += amount;
                Categories.Spend_Categories[category] += amount;
            }
            else if (type=="Income")
            {
                foreach (var item in Categories.Income_Categories)
                {
                    categs.Add(item.Key);
                }
                category = Interface.MenuNavigation(categs.ToArray(), 0, 3);
                account.Incomes += amount;
                Categories.Income_Categories[category] += amount;
            }
           
            AddOperation(account, amount, "AZN", DateTime.Now, category, type); //add anyways
        }
        public void AddOperation(Account account,double amount, string currency, DateTime expence_date, string category,string operation_type )
        {
            Operation expence = new Operation(account,amount,currency,expence_date,category,operation_type);//to list history
            operations.Add(expence);
            First_View();//return back to main menu

        }
    }
}
