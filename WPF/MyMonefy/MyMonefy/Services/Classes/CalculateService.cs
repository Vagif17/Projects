using MyMonefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonefy.Services.Classes;

class CalculateService : ICalculateService
{
    public string AddToExample(String Item)
    {
        switch (Item)
        {
            case "oneBtn":
                return "1";
                break;
            case "twoBtn":
                return "2";
                break;
            case "threeBtn":
                return "3";
                break;
            case "fourBtn":
                return "4";
                break;
            case "fiveBtn":
                return "5";
                break;
            case "sixBtn":
                return "6";
                break;
            case "sevenBtn":
                return "7";
                break;
            case "eightBtn":
                return "8";
                break;
            case "nineBtn":
                return "9";
                break;
            case "zeroBtn":
                return "0";
                break;
            case "plusBtn":
                return "+";
                break;
            case "minusBtn":
                return "-";
                break;
            case "multipleBtn":
                return "*";
                break;
            case "divideBtn":
                return "/";
                break;
        }
        return "";
    }
}
