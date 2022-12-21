using System.Collections.Generic;

namespace MonefyApplication
{
    static class Categories
    {
        public static Dictionary<string, double> Spend_Categories = new Dictionary<string, double>() {
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
        public static Dictionary<string, double> Income_Categories = new Dictionary<string, double>()
        {
            {"Salary",0},
            {"Freelance",0},
            {"Other",0}
        };
    }
}
