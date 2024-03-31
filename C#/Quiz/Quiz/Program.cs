using System;
using System.Collections;
using System.Reflection;

Int32 CorrectChoice(Int32 Choice)
{
    while (true)
    {
        bool Choice_Check = Int32.TryParse(Console.ReadLine(), out Choice);
        if (!Choice_Check) { Console.WriteLine("Enter right choice : "); }
        if (Choice_Check) { break; }
    }
    return Choice;
}


Database MainDataBase = new Database();
Int32 Choice = -1;
while (Choice != 0)
{
    Console.WriteLine("Choose : \n0.End  \n1.Register\n2.Login \n3.Top Users  \n:");
    
    Choice = CorrectChoice(Choice);

    switch (Choice)
    {
        case 0:
            break;

        case 1:
            Console.Clear();
            
            Console.WriteLine("Choose : \n1.User \n2.Administration \n:");
            Int32 ChoiceOfUser = 0;
            ChoiceOfUser = CorrectChoice(ChoiceOfUser);

            switch (ChoiceOfUser)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Enter Login : ");
                    String LoginRegister = Console.ReadLine();

                    if (MainDataBase.Length() != 0)
                    {
                        while (MainDataBase.CheckLoginsInBase(LoginRegister))
                        {
                            Console.Clear();
                            Console.WriteLine("This Login is busy. Enter new Login:");
                            LoginRegister = Console.ReadLine();
                        }
                    }

                    Console.WriteLine("Enter Passwrod :");
                    String PasswordRegister = Console.ReadLine();

                    Console.WriteLine("Enter Date Of Birth : ");
                    String DateOfBirthRegister = Console.ReadLine();

                    User UserToAdd = new User(LoginRegister, PasswordRegister, DateOfBirthRegister);
                    MainDataBase.AddUser(UserToAdd);
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("Enter Login : ");
                    String AdminLoginRegister = Console.ReadLine();

                    if (MainDataBase.AdminLength() != 0)
                    {
                        while (MainDataBase.CheckAdminsInBase(AdminLoginRegister))
                        {
                            Console.Clear();
                            Console.WriteLine("This Login is busy. Enter new Login:");
                            AdminLoginRegister = Console.ReadLine();
                        }
                    }

                    Console.WriteLine("Enter Passwrod :");
                    String AdminPasswordRegister = Console.ReadLine();

                    Console.WriteLine("Enter Date Of Birth : ");
                    String AdminDateOfBirthRegister = Console.ReadLine();

                    Administration AdminToAdd = new Administration( AdminLoginRegister, AdminPasswordRegister,AdminDateOfBirthRegister);
                    MainDataBase.AddAdmin(AdminToAdd);
                    break;
            }
            Console.Clear();
            break;
        case 2:
            Console.Clear();
            Console.WriteLine("Choose :\n1.User\n2.Administration \n:");
            Int32 ChoiceLoginAccoutType = 0;
            ChoiceLoginAccoutType = CorrectChoice(ChoiceLoginAccoutType);

            switch (ChoiceLoginAccoutType)
            {
                case 1:

                    if (MainDataBase.Length() == 0)
                    {
                        Console.WriteLine("No registered accounts.\nBack to main menu...");
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    }
                    Console.Clear();

                    Console.WriteLine("Enter Login :");
                    String LoginToEnter = Console.ReadLine();

                    Int32 ChoiceOfLogin = 0;
                    while (!MainDataBase.CheckLoginsInBase(LoginToEnter))
                    {
                        if (ChoiceOfLogin == 1) { break; }
                        Console.WriteLine("This login is not registered.\n1.Back to main menu \n2.Try again");
                        ChoiceOfLogin = CorrectChoice(ChoiceOfLogin);

                        switch (ChoiceOfLogin)
                        {
                            case 1:
                                Console.WriteLine("Back to main menu...");
                                Thread.Sleep(1000);
                                Console.Clear();
                                break;

                            case 2:
                                Console.Clear();
                                Console.WriteLine("Enter Login :");
                                LoginToEnter = Console.ReadLine();
                                break;
                        }

                    }
                    if (ChoiceOfLogin == 1) { break; }

                    Console.WriteLine("Enter Password :");
                    String PasswordToEnter = Console.ReadLine();

                    Int32 ChoiceOfPassword = 0;
                    while (!MainDataBase.LogIn(LoginToEnter, PasswordToEnter))
                    {
                        if (ChoiceOfPassword == 1) { break; }
                        Console.Clear();
                        Console.WriteLine("Wrong password! \n1.Back to main menu \n2.Try again \n:");
                        ChoiceOfPassword = CorrectChoice(ChoiceOfPassword);

                        switch (ChoiceOfPassword)
                        {

                            case 1:
                                Console.WriteLine("Back to main menu...");
                                Thread.Sleep(1000);
                                Console.Clear();
                                break;

                            case 2:
                                Console.Clear();
                                Console.WriteLine("Enter Password : ");
                                PasswordToEnter = Console.ReadLine();
                                break;

                        }
                    }
                    if (ChoiceOfPassword == 1) { break; }

                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine($"Account :{LoginToEnter} \nChoose : \n0.Exit \n1.Change settings\n2.Start Quiz \n:");
                        Int32 ChoiceInLogin = 0;
                        ChoiceInLogin = CorrectChoice(ChoiceInLogin);

                        if (ChoiceInLogin == 0) { Console.Clear(); break; }

                        switch (ChoiceInLogin)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine("1.Change Password \n2.Change Date Of Birth \n:");
                                Int32 ChoiceForChange = 0;
                                while (ChoiceForChange == 0 || ChoiceForChange > 2) { ChoiceForChange = CorrectChoice(ChoiceForChange); }
                                switch (ChoiceForChange)
                                {
                                    case 1:
                                        Console.WriteLine("Enter new Password :");
                                        String NewPassword = Console.ReadLine();
                                        MainDataBase.ChangePassword(LoginToEnter, NewPassword);
                                        break;


                                    case 2:
                                        Console.WriteLine("Enter new Date Of Birth : ");
                                        String NewDateOfBirh = Console.ReadLine();
                                        MainDataBase.ChangeDateOfBirth(LoginToEnter, NewDateOfBirh);
                                        break;
                                }
                                Console.Clear();
                                break;

                            case 2:
                                Console.Clear() ;
                                Console.WriteLine("Choose : \n0.Exit \n1.Math Quiz \n2.Geography Quiz \n3.History Quiz \n4.Start Random Quiz \n5.Check My Results \n:");
                                Int32 ChoiceTypeQuiz = 0;
                                ChoiceTypeQuiz = CorrectChoice(ChoiceTypeQuiz);

                                if (ChoiceTypeQuiz == 0) { break; }
                                switch (ChoiceTypeQuiz)
                                {
                                    case 1:
                                        if(MainDataBase.IsMathEmpty())
                                        {
                                            Console.Clear();
                                            Console.WriteLine("There is no Quiz,only admins can add it");
                                            Thread.Sleep(1000);
                                            Console.Clear();
                                            break;
                                        }
                                        Console.Clear();
                                        MainDataBase.StartMathQuiz(LoginToEnter);
                                        
                                        break;


                                    case 2:
                                        if (MainDataBase.IsGeographyEmpty())
                                        {
                                            Console.Clear();
                                            Console.WriteLine("There is no Quiz,only admins can add it");
                                            Thread.Sleep(1000);
                                            Console.Clear();
                                            break;
                                        }

                                        MainDataBase.StartGeographyQuiz(LoginToEnter);

                                        break;



                                    case 3:
                                        if(MainDataBase.IsHistoryEmpty())
                                        {
                                            Console.Clear();
                                            Console.WriteLine("There is no Quiz,only admins can add it");
                                            Thread.Sleep(1000);
                                            Console.Clear();
                                            break;
                                        }

                                        MainDataBase.StartHistoryQuiz(LoginToEnter);

                                        break;

                                    case 4:
                                        if (MainDataBase.IsRandomEmpty())
                                        {
                                            Console.Clear();
                                            Console.WriteLine("There is no Quiz,only admins can add it");
                                            Thread.Sleep(1000);
                                            Console.Clear();
                                            break;
                                        }

                                        MainDataBase.StartRandomyQuiz(LoginToEnter);
                                        break;


                                    case 5:
                                        MainDataBase.CheckResults(LoginToEnter);
                                        break;


                                }

                                
                                break;
                        }
                    }

                    break;

                case 2:
                    if (MainDataBase.AdminLength() == 0)
                    {
                        Console.WriteLine("No registered accounts.\nBack to main menu...");
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    }
                    Console.Clear();

                    Console.WriteLine("Enter Login :");
                    String AdminLoginToEnter = Console.ReadLine();

                    Int32 AdminChoiceOfLogin = 0;
                    while (!MainDataBase.CheckAdminsInBase(AdminLoginToEnter))
                    {
                        if (AdminChoiceOfLogin == 1) { break; }
                        Console.WriteLine("This login is not registered.\n1.Back to main menu \n2.Try again");
                        AdminChoiceOfLogin = CorrectChoice(AdminChoiceOfLogin);

                        switch (AdminChoiceOfLogin)
                        {
                            case 1:
                                Console.WriteLine("Back to main menu...");
                                Thread.Sleep(1000);
                                Console.Clear();
                                break;

                            case 2:
                                Console.Clear();
                                Console.WriteLine("Enter Login :");
                                AdminLoginToEnter = Console.ReadLine();
                                break;
                        }

                    }
                    if (AdminChoiceOfLogin == 1) { break; }

                    Console.WriteLine("Enter Password :");
                    String AdminPasswordToEnter = Console.ReadLine();

                    Int32 AdminChoiceOfPassword = 0;
                    while (!MainDataBase.AdminLogIn(AdminLoginToEnter, AdminPasswordToEnter))
                    {
                        if (AdminChoiceOfPassword == 1) { break; }
                        Console.Clear();
                        Console.WriteLine("Wrong password! \n1.Back to main menu \n2.Try again \n:");
                        AdminChoiceOfPassword = CorrectChoice(AdminChoiceOfPassword);

                        switch (AdminChoiceOfPassword)
                        {

                            case 1:
                                Console.WriteLine("Back to main menu...");
                                Thread.Sleep(1000);
                                Console.Clear();
                                break;

                            case 2:
                                Console.Clear();
                                Console.WriteLine("Enter Password : ");
                                AdminPasswordToEnter = Console.ReadLine();
                                break;

                        }
                    }
                    if (AdminChoiceOfPassword == 1) { break; }
                    Console.Clear();

                    Int32 ChoiceQuiz = 0;
                    while (true)
                    {
                        Console.WriteLine($"Account : {AdminLoginToEnter}\nChoose : \n0.Exit : \n1.Create or Recreate Math quiz \n2.Create or Recreate Geography quiz \n3.Create or Recreate History quiz \n4.Create or Recreate Random quiz \n:  ");
                        ChoiceQuiz = CorrectChoice(ChoiceQuiz);
                        
                        if (ChoiceQuiz == 0 ) { break; Console.Clear(); }

                        switch (ChoiceQuiz)
                        {
                            case 1:

                                for (int i = 0; i < 20; i++)
                                {
                                    Questions questions = new Questions();
                                    questions.AddQuestion();
                                    MainDataBase.AddMath(questions);
                                }
                                

                                break;

                            case 2:
                                for (int i = 0; i < 20; i++)
                                {
                                    Questions questions = new Questions();
                                    questions.AddQuestion();
                                    MainDataBase.AddGeography(questions);
                                }
                                break;


                            case 3:
                                for (int i = 0; i < 20; i++) 
                                {
                                    Questions questions = new Questions();
                                    questions.AddQuestion();
                                    MainDataBase.AddHistory(questions);
                                }
                                break;

                            case 4:
                                MainDataBase.CreateRandom();
                                break;
                        }
                    }
                    if (ChoiceQuiz == 0) { break; }
                    break;
            }
            break;

        case 3:
            Console.Clear();
            Console.WriteLine("Choose : \n0.Exit \n1.Top Math \n2.Top History \n3.Top Geography\n4.Top Random\n:");
            Int32 ChoiceTop = 0;
            ChoiceTop = CorrectChoice(ChoiceTop);
            if (ChoiceTop == 0) {break; }

            switch (ChoiceTop)
            {
                case 1:
                    Console.Clear();
                    MainDataBase.TopMath();
                    Console.WriteLine("Choose : \n0.Exit");
                    ChoiceTop = CorrectChoice(ChoiceTop);
                    break;

                 case 2:
                    Console.Clear();
                    MainDataBase.TopHistory();
                    Console.WriteLine("Choose : \n0.Exit");
                    ChoiceTop = CorrectChoice(ChoiceTop);
                    break;

                 case 3:
                    Console.Clear();
                    MainDataBase.TopGeography();
                    Console.WriteLine("Choose : \n0.Exit");
                    ChoiceTop = CorrectChoice(ChoiceTop);
                    break;

                case 4:
                    Console.Clear();
                    MainDataBase.TopRandom();
                    Console.WriteLine("Choose : \n0.Exit");
                    ChoiceTop = CorrectChoice(ChoiceTop);
                    break;

            }

            break;
    }

    Console.Clear ();
}
