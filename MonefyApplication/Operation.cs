using System;

namespace MonefyApplication
{
    //to handle both expence and income operations
    class Operation
    {
        public Operation(Account acc, double amount, string currency, DateTime expence_date, string category,string operation_type)
        {
            Acc = acc;
            Amount = amount;
            Currency = currency;
            Operation_Date = expence_date;
            Category = category;
            Operation_Type = operation_type;
        }
        public string Operation_Type { get; set; }
        public Account Acc { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        //Date to make then history of operations
        public DateTime Operation_Date { get; set; }
        public string Category { get; set; }

        public override string ToString()
        {
            return $"{Operation_Date}: \n Account: {Acc.Name} \n Type:{Operation_Type}\n Amount: {Amount}{Currency} \n Category: {Category}";
        }
    }
}
