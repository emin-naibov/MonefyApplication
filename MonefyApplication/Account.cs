using System.Collections.Generic;

namespace MonefyApplication
{
    class Account
    {
        public double Total_balance { get; set; }
        public string Name { get; set; }
        public double Expences { get; set; } = 0;
        public double Incomes { get; set; } = 0;
        public string Currency { get; set; } = "AZN";
        public Dictionary<string, double> Spend_Categories { get; set; } = new Dictionary<string, double>()
        {
            {"Bills",0},
            {"Car",0},
            {"Clothes",0 },
            {"Communications",0 },
            {"Eating out",0 },
            {"Enterteimant",0 },
            {"Food",0 },
            {"Gifts",0 },
            {"Health",0 },
            { "House",0 },
            { "Pets",0 },
            { "Sports",0 },
            { "Taxi",0 },
            { "Toiletry",0 },
            { "Transport",0 }
        };
        public Dictionary<string, double> Income_Categories { get; set; } = new Dictionary<string, double>()
        {
            {"Salary",0},
            {"Freelance",0},
            {"Other",0}
        };



        public Account(string name,double money)
        {
            Name = name;
            Total_balance = money;
        }
    }
}
