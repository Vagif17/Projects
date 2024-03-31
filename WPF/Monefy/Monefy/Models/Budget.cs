using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Models;

class Budget
{
    public int Expenses { get; set; }
    public int Income { get; set; }


    public Budget() {
        Expenses = 0;
        Income = 0;
    }
}

//class SoccerBudget
//{
//    public int SoccerExpenses { get; set; }
//    public int SoccerIncome { get; set; }


//}

//class Health
//{
//    public int HealthExpenses { get; set; }
//    public int HealthIncome { get; set; }

//}

//class Car
//{
//    public int CarExpenses { get; set; }
//    public int CarIncome { get; set; }
//}

//class Pets
//{
//    public int PetsExpenses { get; set; }
//    public int PetsIncome { get; set; }
//}

//class FoodBudget
//{
//    public int FoodExpenses { get; set; }
//    public int FoodIncome { get; set; }

//    public int GiftExpenses { get; set; }
//    public int GiftIncome { get; set; }

//    public int ClothesExpenses { get; set; }
//    public int ClothesIncome { get; set; }

    
//}
