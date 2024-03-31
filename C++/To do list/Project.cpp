#include <iostream>
using namespace std;

enum Prior {
    LOW = 1,
    MIDDLE,
    HIHG
};

struct Date
{
    int day{};
    int month{};
    int year{};
};

struct Deal
{
    char* name = new char [40] {};
    Prior priority{};
    char* description = new char[150] {};
    Date date;

    void view()
    {
        cout << "Deal's name : " << name << "\nPriority : ";
        switch (priority)
        {
        case LOW:
            cout << "Low";
            break;
        case MIDDLE:
            cout << "Middle";
            break;
        case HIHG:
            cout << "High";
            break;

        }
        cout << "\nDescription : " << description << "\nDate : " << date.day << "." << date.month << "." << date.year;
    }
};

Deal makeDeal() {
    cin.ignore();
    Deal d1;
    uint16_t tmp{};

    cout << "Enter name of deal : ";
    cin.getline(d1.name, 40);

    cout << "Enter priority (1 - Low, 2 - Middle, 3 - High) : ";
    while (tmp <= 0 || tmp > 3)
    {
        cin >> tmp;
        if (tmp <= 0 || tmp > 3)
            cout << "Error:Incorect variant; Try again :";
    }
    d1.priority = Prior(tmp);

    cin.ignore();
    cout << "Enter description : ";
    cin.getline(d1.description, 150);


    cout << "Enter date (00.00.0000 format) :\nDay : ";
    cin >> d1.date.day;


    cout << "Month : ";
    cin >> d1.date.month;


    cout << "Year : ";
    cin >> d1.date.year;

    return d1;
}

int main()
{
    Deal* f1;
    f1 = new Deal[20]{};
    uint16_t index{};
    uint16_t choose{};
    while (choose != 8)
    {
        cout << "\n1.Create new deal\n2.View all deals\n3.Delete deal\n4.Edit deal\n5.Sort deals\n6.Find\n7.Check date\n8.End \n:";
        cin >> choose;
        switch (choose)
        {


        case 1:
        {
            f1[index] = makeDeal();
            index++;
            break;
        }

        case 2:
        {
            if (index == 0)
            {
                cout << "List is empty now.";
                break;
            }
            cout << endl;
            for (size_t i = 0; i < index; i++)
            {
                cout << "--------------------\n";
                f1[i].view();
                cout << "\n--------------------\n";
            }
            cout << endl;
            break;
        }

        case 3:
        {
            if (index == 0)
            {
                cout << "List is empty now.";
                break;
            }
            uint16_t choiceToDelete{};
            cout << endl;
            for (size_t i = 0; i < index; i++)
            {
                cout << "--------------------\n";
                cout << "Index : " << i << endl;
                f1[i].view();
                cout << "\n--------------------\n";
            }
            cout << "\nEnter index to delete : ";
            cin >> choiceToDelete;

            Deal* tmp = new Deal[15]{};
            for (size_t i = 0; i < index; i++)
            {
                if (i != choiceToDelete) {
                    tmp[i] = f1[i];
                }
                else
                {
                    if (i + 1 < index) {
                        tmp[i] = f1[i + 1];
                    }
                    else
                    {
                        break;
                    }
                }
            }
            delete[] f1;

            f1 = new Deal[15]{};
            for (size_t i = 0; i < index; i++)
            {
                if (i != choiceToDelete)
                {
                    f1[i] = tmp[i];
                }
                else
                {
                    if (i + 1 < index)
                    {
                        f1[i] = tmp[i + 1];

                    }
                    else
                    {
                        break;
                    }
                }
            }
            index--;
            delete[] tmp;
            break;
        }
        case 4:
        {
            if (index == 0)
            {
                cout << "List is empty now.";
                break;
            }

            uint16_t choiceToEdit{};
            cout << endl;
            for (size_t i = 0; i < index; i++)
            {
                cout << "--------------------\n";
                cout << "Index : " << i << endl;
                f1[i].view();
                cout << "\n--------------------\n";
            }
            cout << "\nEnter index to edit : ";
            cin >> choiceToEdit;

            Deal editedDeal{};
            editedDeal = makeDeal();

            f1[choiceToEdit] = editedDeal;

            break;
        }

        case 5:
        {
            if (index == 0)
            {
                cout << "List is empty now.";
                break;
            }

            if (index == 1)
            {
                cout << "There is one deal in lsit.";
                break;
            }

            Deal* tmpSortedByDate = new Deal[index]{}; // Используется если будет сортировка по дате
            Deal* tmpSorted = new Deal[20]{};
            int choice{};
            int indSort{};
            cout << "1.Sort bu priority \n2.Sort by time \n :";
            cin >> choice;
            while (choice < 1 || choice > 3)
            {
                cout << "Error:: Incorect number!. Try again : ";
                cin >> choice;
            }
            if (choice == 1) {
                for (size_t i = 3; i != 0; i--)
                {
                    for (size_t j = 0; j < index; j++)
                    {
                        if (int(f1[j].priority) == i)
                        {
                            tmpSorted[indSort] = f1[j];
                            indSort++;
                        }
                    }
                }
                delete[] f1;

                f1 = new Deal[20]{};
                for (size_t i = 0; i < index; i++)
                {
                    f1[i] = tmpSorted[i];
                }

                cout << "\nSorted complete\n ";
                delete[] tmpSorted;
                break;
                int indSort = 0;
            }
            if (choice == 2)
            {
                Date maxDate = f1[0].date;
                Date minDate = f1[0].date;

                for (size_t i = 0; i < index; i++)
                {
                    if (f1[i].date.year < maxDate.year)
                    {
                        minDate = f1[i].date;
                    }

                    if (f1[i].date.year > minDate.year)
                    {
                        minDate = f1[i].date;
                    }
                }

                int tmpInd{};
                  for ( int y = minDate.year;  y < maxDate.year + 1;  y++)
                    {
                      for (size_t m = 1; m < 13; m++)
                      {
                          for (size_t d = 1; d < 32; d++)
                          {
                              for (size_t i = 0; i < index; i++)
                              {
                                  if (f1[i].date.year == y)
                                  {
                                      if (f1[i].date.month == m)
                                      {
                                          if (f1[i].date.day == d)
                                          {
                                              tmpSortedByDate[tmpInd] = f1[i];
                                              tmpInd++;

                                          }
                                      }
                                  }
                              }
                          }
                      }
                    }
                  tmpInd = 0;
                  cout << "\nSorted is completed!\n";
                  delete[] f1;
                  f1 = new Deal[20]{};
                for (size_t i = 0; i < index; i++)
                {
                    f1[i] = tmpSortedByDate[i];
                }
                delete[] tmpSortedByDate;
                break;

            }
                
        }

        case 6 :
        {
            if (index == 0)
            {
                cout << "List is empty now.";
                break;
            }

            int searchMethod{};
            cout << "\nChoose Search Method \n1.Name \n2.Priority \n3.Description \n4.Date \n: ";
            cin >> searchMethod;

            if (searchMethod == 1)
            {
                cin.ignore();
                char* tmpName = new char[40]{};
                cout << "Enter name for search : ";
                cin.getline(tmpName, 40);
                int correct{};
                int len{};

                while (tmpName[len] != '\0')
                {
                    len++;
                }

                for (size_t i = 0; i < index; i++)
                {
                    correct = 0;
                    for (size_t b = 0; b < len; b++)
                    {
                        if (int (tmpName[b]) == int (f1[i].name[b]) || int(tmpName[b]) == int(f1[i].name[b] + 32) || int(tmpName[b]) == int(f1[i].name[b] - 32))
                        {
                            correct++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (correct == len)
                    {
                        cout << "\n--------------------\n";
                        cout << "Index : " << i << endl;
                        f1[i].view();
                        cout << "\n--------------------\n";
                        break;
                    }
                }
            }

            if (searchMethod == 2)
            {
                int tmpPriori{};
                cout << "Enter priority (1 - Low, 2 - Middle, 3 - High) : ";
                while (tmpPriori <= 0 || tmpPriori > 3)
                {
                    cin >> tmpPriori;
                    if (tmpPriori <= 0 || tmpPriori > 3)
                        cout << "Error:Incorect variant; Try again :";
                }

                for (size_t i = 0; i < index; i++)
                {
                    if (f1[i].priority == tmpPriori)
                    {
                        cout << "\n--------------------\n";
                        cout << "Index : " << i << endl;
                        f1[i].view();
                        cout << "\n--------------------\n";
                    }
                }
            }

            if (searchMethod == 3)
            {
                cin.ignore();
                char* tmpDescription = new char [150]{};

                cout << "Enter Description : ";
                cin.getline(tmpDescription, 150);

                int len{};

                while (tmpDescription[len] != '\0')
                {
                    len++;
                }

                int textLen{};
                int correct{};

                for (size_t i = 0; i < index; i++)
                {
                    textLen = 0;
                    while (f1[i].description[textLen] != '\0')
                    {
                        textLen++;
                    }
                    for (size_t a = 0; a < textLen; a++)
                    {
                        correct = 0;
                        for (size_t c = 0; c < len; c++)
                        {
                            if (int(f1[i].description[a + c]) == int(tmpDescription[c]) || int(f1[i].description[a + c] + 32) == int(tmpDescription[c]))
                            {
                                correct++;
                            }
                        }
                        if (correct == len)
                        {
                            cout << "\n--------------------\n";
                            cout << "Index : " << i << endl;
                            f1[i].view();
                            cout << "\n--------------------\n";

                    }
                    
                    }
                }
            }

            if (searchMethod == 4)
            {
                Date tmpDate{};

                cout << "Enter date : \nDay : ";
                cin >> tmpDate.day;

                cout << "Month : ";
                cin >> tmpDate.month;

                cout << "Year : ";
                cin >> tmpDate.year;

                for (size_t i = 0; i < index; i++)
                {
                    if (tmpDate.year == f1[i].date.year && tmpDate.month == f1[i].date.month && tmpDate.day == f1[i].date.day)
                    {
                        cout << "\n--------------------\n";
                        cout << "Index : " << i << endl;
                        f1[i].view();
                        cout << "\n--------------------\n";
                    }
                }
            }
            break;
        }
          case 7:
          {
              int choice{};
              cout << "Check \n1.Day\n2.Week\n3.Month\n";
              cin >> choice;

              if (choice == 1)
              {
                  Date tmpDay{};
                  cout << "Enter date \nDay : ";
                  cin >> tmpDay.day;

                  cout << "Month : ";
                  cin >> tmpDay.month;

                  cout << "Year : ";
                  cin >> tmpDay.year;

                  for (size_t i = 0; i < index; i++)
                  {
                      if (f1[i].date.year == tmpDay.year && tmpDay.month == f1[i].date.month && f1[i].date.day == tmpDay.day)
                      {
                          cout << "\n--------------------\n";
                          cout << "Index : " << i << endl;
                          f1[i].view();
                          cout << "\n--------------------\n";
                      }
                  }
              }

              if (choice == 2)
              {
                  Date tmpWeek{};
                  cout << "Enter date \nDay : ";
                  cin >> tmpWeek.day;

                  cout << "Month : ";
                  cin >> tmpWeek.month;

                  cout << "Year : ";
                  cin >> tmpWeek.year;
                  
                  for (size_t i = 0; i < index; i++)
                  {


                      if (f1[i].date.year == tmpWeek.year && tmpWeek.month == f1[i].date.month)
                      {
                          if (f1[i].date.day >= tmpWeek.day && tmpWeek.day <= tmpWeek.day + 7)
                          {
                              cout << "\n--------------------\n";
                              cout << "Index : " << i << endl;
                              f1[i].view();
                              cout << "\n--------------------\n";
                          }
                      }
                  }
              }

              if (choice == 3)
              {
                  Date tmpMonth{};
                  cout << "Enter date \nDay : ";
                  cin >> tmpMonth.day;

                  cout << "Month : ";
                  cin >> tmpMonth.month;

                  cout << "Year : ";
                  cin >> tmpMonth.year;

                  for (size_t i = 0; i < index; i++)
                  {
                      if (f1[i].date.year == tmpMonth.year && tmpMonth.month == f1[i].date.month)
                      {
                          cout << "\n--------------------\n";
                          cout << "Index : " << i << endl;
                          f1[i].view();
                          cout << "\n--------------------\n";
                      }
                  }
              }

              break;
          }

        }
    }
        return 0;
}