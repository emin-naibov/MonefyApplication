using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace MonefyApplication
{
    class App
    {
        List<Account> account_types = new List<Account>(); //types like cash or credit card
        Account current_account;
        List<Operation> operations = new List<Operation>();
        public App(Account account)
        {
            current_account = account;
            account_types.Add(account);
        }


        public string[] Main_Menu { get; set; } = { "Add Expence", "Add Income", "Spend Categories Overview","Income Categories Overview","Operations History", "Change Period", "Manage Accounts", "Manage Categories", "Export to CSV", "Exit" };


         public  void First_View()
        {
            //Account account = new Account("Credit Card", 0);  //default account for experiment then will be modified
            Interface.First_Menu_Interface(current_account);
            string result = Interface.MenuNavigation(Main_Menu, 0, 8);//7 because main menu's view  has already 6 lines 
                switch (result)
                {
                case "Add Expence":
                    AddOperationMenu(current_account, "Expence");
                    break;
                case "Add Income":
                    AddOperationMenu(current_account, "Income");
                    break;
                case "Spend Categories Overview":
                    Spend_Categories_Overview();
                        break;
                case "Income Categories Overview":
                    Income_Categories_Overview();
                break;
                case "Operations History":
                    Operations_History();
                break;
                case "Manage Accounts":
                    ManageAccounts(current_account);
                    break;
                case "Manage Categories":
                    Manage_Categories();
                    break;
                default:
                    break;
                }
            //Interface.Thread_Operations(10000);
            First_View();
        }
        public void AddOperationMenu(Account account,string type)
        {
            //List<string> categs = new List<string>();
            Console.Write("Amount: ");
            double amount = double.Parse(Console.ReadLine());
            string category="";
            Console.WriteLine("Choose category: ");
            Console.WriteLine();
            if (type == "Expence")
            {
                //foreach (var item in Categories.Spend_Categories)
                //{
                //    categs.Add(item.Key);
                //}
                category = Interface.MenuNavigation(account.Spend_Categories.Keys.ToArray(), 0, 3);//as menunavigation takes string[]
                account.Expences += amount;
                account.Total_balance -= amount;
                account.Spend_Categories[category] += amount;
            }
            else if (type=="Income")
            {
                //foreach (var item in Categories.Income_Categories)
                //{
                //    categs.Add(item.Key);
                //}
                category = Interface.MenuNavigation(account.Income_Categories.Keys.ToArray(), 0, 3);
                account.Incomes += amount;
                account.Total_balance += amount;
                account.Income_Categories[category] += amount;
            }
           
            AddOperation(account, amount, "AZN", DateTime.Now, category, type); //add anyways
        }
        public void AddOperation(Account account,double amount, string currency, DateTime expence_date, string category,string operation_type )
        {
            Operation expence = new Operation(account,amount,currency,expence_date,category,operation_type);//to list history
            operations.Add(expence);
            First_View();//return back to main menu

        }
        public void Spend_Categories_Overview()
        {
            double spend_total = current_account.Spend_Categories.Sum(x => x.Value);
            if(spend_total==0)
            {
                Console.WriteLine("Your spent amount is :0");
                foreach (var item in current_account.Spend_Categories)
                {
                    Console.WriteLine($"{item.Key} {item.Value}");
                }
            }
            else
            {
               foreach (var item in current_account.Spend_Categories)
               {
                   Console.WriteLine($"{item.Key} - {item.Value * 100 / spend_total} %");//to show percentage per category
               }

            }
            if (Console.ReadKey().Key == ConsoleKey.Enter)
                Console.Clear();
                First_View();

        }
        public void Income_Categories_Overview()
        {
            double income_total = current_account.Income_Categories.Sum(x => x.Value);
            if (income_total == 0)
            {
                Console.WriteLine("Your income is: 0");
                foreach (var item in current_account.Income_Categories)
                {
                    Console.WriteLine($"{item.Key} {item.Value}");
                }
            }
            else
            {
                foreach (var item in current_account.Income_Categories)
                {
                    Console.WriteLine($"{item.Key} - {item.Value * 100 / income_total} %");//to show percentage per category
                }
            }
            if (Console.ReadKey().Key == ConsoleKey.Enter)
                Console.Clear();
                First_View();
        }
        public void Operations_History()
        {
           List<Operation> opers=operations.Where((x) => x.Acc == current_account).ToList();
            opers.Sort((x, y) => DateTime.Compare(x.Operation_Date, y.Operation_Date));//sort opeartions by date using lambda expression
            foreach (var item in opers)
            {
                //dont use .ToString() because I want Operation Type to has another foreground color
                Console.Write($"{item.Operation_Date} \n Account: {item.Acc.Name} \n Type:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(item.Operation_Type);
                Console.ResetColor();
                Console.WriteLine($" Amount: {item.Amount} {item.Currency}\n Category: {item.Category}");
                Console.WriteLine();
            }
            if (Console.ReadKey().Key == ConsoleKey.Enter)
                Console.Clear();
                First_View();

        }
        public void Manage_Categories()
        {
            Console.WriteLine("Choose type of Categories:");
            Console.WriteLine();
            string result = Interface.MenuNavigation(new string[] { "Spend", "Income" }, 0, 2);
            Console.WriteLine("Choose type of Operation:");
            Console.WriteLine();
            string operation_type = Interface.MenuNavigation(new string[] { "Add Category", "Delete Category" }, 0, 2);
            switch (result)
            {
                case "Spend":
                    if (operation_type == "Add Category")
                    {
                        Console.WriteLine("Write the name of Category:");
                        try
                        {
                            current_account.Spend_Categories.Add(Console.ReadLine(), 0);
                        }
                        catch (Exception)
                        {

                            Console.WriteLine("Category name type is not right!");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Choose Category:");
                        string category_to_delete = Interface.MenuNavigation(current_account.Spend_Categories.Keys.ToArray(), 0, 1);
                        current_account.Expences -= current_account.Spend_Categories[category_to_delete];
                        current_account.Total_balance += current_account.Spend_Categories[category_to_delete];
                        current_account.Spend_Categories.Remove(category_to_delete);
                    }
                    break;
                case "Income":
                    if (operation_type == "Add Category")
                    {
                        Console.WriteLine("Write the name of Category:");
                        try
                        {
                            current_account.Income_Categories.Add(Console.ReadLine(), 0);
                        }
                        catch (Exception)
                        {

                            Console.WriteLine("Category name type is not right!");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Choose Category:");
                        string category_to_delete = Interface.MenuNavigation(current_account.Income_Categories.Keys.ToArray(), 0, 1);
                        current_account.Incomes -= current_account.Income_Categories[category_to_delete];
                        current_account.Total_balance -= current_account.Income_Categories[category_to_delete];
                        current_account.Income_Categories.Remove(category_to_delete);
                    }
                    break;
                default:
                    break;
            }
            Console.Clear();
        }
        public void ManageAccounts(Account account)
        {
            Console.WriteLine($"You are now in {account.Name} account");
            Console.WriteLine("Accounts list: \n");
            List<string> names = new List<string>();
            foreach (var item in account_types)
            {
                Console.WriteLine($"{ item.Name}");
                names.Add(item.Name);
            }
            Console.WriteLine();

            var result = Interface.MenuNavigation(new string[] { "Switch account", "Add account" }, 0, account_types.Count + 4);
            if (result=="Switch account")
            {
                Switch_acc(names);
            }
            else
            {
                Add_acc();

            }
        }
        public void Switch_acc(List<string> names)
        {
            Console.WriteLine("Select account you want to switch:");
            Console.WriteLine();
            var result = Interface.MenuNavigation(names.ToArray(),0,2);
            foreach (var item in account_types)
            {
                if(item.Name==result)
                {
                    current_account = item;
                    First_View();
                }

            }

        }
        public void Add_acc()
        {
            Console.WriteLine("New account name:  ");
            string newAccName=Console.ReadLine();
           var result=account_types.Exists((x) => x.Name == newAccName);
            if (!result)
            {
                Console.WriteLine("New account balance: ");
                var newAccBalance = double.Parse(Console.ReadLine());
                Account account = new Account(newAccName, newAccBalance);
                account_types.Add(account);
                Interface.Thread_Operations(500);
                Console.Clear();
                First_View();
            }
            else
            {
                Console.WriteLine("This account already exists");
                Interface.Thread_Operations(2000);
                Console.Clear();
                First_View();
            }
        }
    }
}
