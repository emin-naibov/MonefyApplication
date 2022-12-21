using System.Collections.Generic;

namespace MonefyApplication
{
    //Account initialization
    class Account
    {
        public double Total_balance { get; set; }
        public string Name { get; set; }
        public double Expences { get; set; } = 0;
        public double Incomes { get; set; } = 0;
        public string Currency { get; set; } = "AZN";
       
        public Account(string name,double money)
        {
            Name = name;
            Total_balance = money;
        }
    }
}
