class Database 
{
   public List<User> AllUsers { get; set; } = new List<User>();
   public List<Administration> AllAdministration { get; set; } = new List<Administration> {};
   public List<Questions> Math { get; set; } = new List<Questions>();
   public List<Questions> Geography { get; set; } = new List<Questions>();
   public List<Questions> History { get; set; } = new List<Questions>();

    public List<Questions> RandomQuiz { get; set; } = new List<Questions>();

   static Int32 MathCount  = 0;
   static Int32 MathQuestionNow = 0;


   static Int32 GeographyCount = 0;
   static Int32 GeographyQuestionNow = 0;

   static Int32 HistoryCount = 0;
   static Int32 HistoryQuestionNow = 0;

   static Int32 RandomQuizCount = 0;
   static Int32 RandomQuizQuestionNow = 0;

    public Database() { }

    public User this[int index]
    {
        get
        {
            return AllUsers[index];
        }
        set

        {
            AllUsers[index] = value;
        }
    }


    #region AddUsersMethods
    public void AddUser(User user)
    {
        AllUsers.Add(user);
    }
    public void AddAdmin(Administration admin)
    {
        AllAdministration.Add(admin);
    }

    #endregion


    #region Lengths
    public Int32 Length()
    {
        return AllUsers.Count;
    }

    public Int32 AdminLength()
    {
        return AllAdministration.Count;
    }

    #endregion


    #region MathMethods
    public void AddMath(Questions questions)
    {
        if (MathCount > 19)
        {
            MathQuestionNow = 0;
            MathCount = 0;
            Math.Clear();
        }
        Math.Add(questions);
        MathCount++;
    }

    public bool IsMathEmpty()
    {
        if (MathCount == 0) return true;
        return false;
    }

  

    public void CheckMathAnswers(Int32[] Answers, String Login)
    {
        if (MathQuestionNow > 19) 
        {
            MathQuestionNow = 0;
        }
        if (Answers.Length > Math[MathQuestionNow].RigthAnswers.Length || Answers.Length < Math[MathQuestionNow].RigthAnswers.Length) { return; }
        

        bool AddPoint = false;

        for (int i = 0; i < Answers.Length; i++)
        {
            for (int c = 0; c < Math[MathQuestionNow].RigthAnswers.Length; c++)
            {
                if (Answers[i] == Math[MathQuestionNow].RigthAnswers[c])
                {
                    AddPoint= true;
                    i++;
                }
                else
                {
                    AddPoint= false;
                    break;
                }
            }

        }
        if (AddPoint)
        {
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if (AllUsers[i].Login == Login)
                {
                    AllUsers[i].MathRightAnswers++;
                }
            }
        }

        MathQuestionNow++;
    }

    public void StartMathQuiz(String Login)
    {
        for (int i = 0; i < AllUsers.Count; i++)
        {
            if (AllUsers[i].Login == Login)
            {
                AllUsers[i].MathRightAnswers = 0;
            }
        }
        Int32 Count = 0;
        Int32[] UserAnswers = new int[0];

        for (int i = 0; i < MathCount; i++)
        {
            Console.WriteLine(Math[i]);
            Console.WriteLine("Enter count of right numbers :\n");
            Count = Int32.Parse(Console.ReadLine());

            UserAnswers = new Int32[Count];

            for (int b = 0; b < Count; b++)
            {
                Console.WriteLine("Enter right index : ");
                UserAnswers[b] = Int32.Parse(Console.ReadLine());
            }
            CheckMathAnswers(UserAnswers, Login);

        }


    }


    public void TopMath()
    {
        if (AllUsers.Count == 0)
        {
            Console.WriteLine("There are no users");
            return;
        }

        if (Math.Count == 0)
        {
            Console.WriteLine("There are no questions in quiz ");
            return;
        }





        List<Int32> Top = new List<Int32>();

        for (int i = 0; i < AllUsers.Count; i++)
        {
            Top.Add(AllUsers[i].MathRightAnswers);
        }

        Top.Sort();

        Console.Clear();
        for (int i = 0; i < Top.Count ; i++)
        {
            for (int b = 0; b < AllUsers.Count; b++)
            {
                if (Top[i] == AllUsers[b].MathRightAnswers)
                {
                    Console.WriteLine($"Place : {i + 1}.{AllUsers[b].Login}");
                }
            }
        }

    }

    #endregion


    #region HistoryMethods

    public void AddHistory(Questions questions)
    {
        if (HistoryCount > 19)
        {
            HistoryQuestionNow = 0;
            HistoryCount = 0;
            History.Clear();
        }
        History.Add(questions);
        HistoryCount++;
    }

    public bool IsHistoryEmpty()
    {
        if (HistoryCount == 0) return true;
        return false;
    }



    public void CheckHistoryAnswers(Int32[] Answers, String Login)
    {
        if (HistoryQuestionNow > 19)  
        {
            HistoryQuestionNow = 0;
        }

        if (Answers.Length > History[HistoryQuestionNow].RigthAnswers.Length || Answers.Length < History[HistoryQuestionNow].RigthAnswers.Length) { return; }
        

        bool AddPoint = false;

        for (int i = 0; i < Answers.Length; i++)
        {
            for (int c = 0; c < History[HistoryQuestionNow].RigthAnswers.Length; c++)
            {
                if (Answers[i] == History[HistoryQuestionNow].RigthAnswers[c])
                {
                    AddPoint = true;
                    i++;
                }
                else
                {
                    AddPoint = false;
                    break;
                }
            }

        }
        if (AddPoint)
        {
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if (AllUsers[i].Login == Login)
                {
                    AllUsers[i].HistoryRightAnswers++;
                }
            }
        }

        HistoryQuestionNow++;
    }

    public void StartHistoryQuiz(String Login)
    {
        for (int i = 0; i < AllUsers.Count; i++)
        {
            if (AllUsers[i].Login == Login)
            {
                AllUsers[i].HistoryRightAnswers = 0;
            }
        }
        Int32 Count = 0;
        Int32[] UserAnswers = new int[0];

        for (int i = 0; i < HistoryCount; i++)
        {
            Console.WriteLine(History[i]);
            Console.WriteLine("Enter count of right numbers :\n");
            Count = Int32.Parse(Console.ReadLine());

            UserAnswers = new Int32[Count];

            for (int b = 0; b < Count; b++)
            {
                Console.WriteLine("Enter right index : ");
                UserAnswers[b] = Int32.Parse(Console.ReadLine());
                CheckHistoryAnswers(UserAnswers, Login);
            }
        }


    }


    public void TopHistory()
    {
        if (AllUsers.Count == 0)
        {
            Console.WriteLine("There are no users");
            return;
        }

        if (History.Count == 0)
        {
            Console.WriteLine("There are no questions in quiz ");
            return;
        }





        List<Int32> Top = new List<Int32>();

        for (int i = 0; i < AllUsers.Count; i++)
        {
            Top.Add(AllUsers[i].HistoryRightAnswers);
        }

        Top.Sort();

        Console.Clear();
        for (int i = 0; i < Top.Count; i++)
        {
            for (int b = 0; b < AllUsers.Count; b++)
            {
                if (Top[i] == AllUsers[b].HistoryRightAnswers)
                {
                    Console.WriteLine($"Place : {i + 1}.{AllUsers[b].Login}");
                }
            }
        }

    }

    #endregion


    #region GeographyMethods
    public void AddGeography(Questions questions)
    {
        if (GeographyCount > 19)
        {
            GeographyQuestionNow = 0;
            GeographyCount = 0;
            Geography.Clear();
        }
        Geography.Add(questions);
        GeographyCount++;
    }

    public bool IsGeographyEmpty()
    {
        if (GeographyCount == 0) return true;
        return false;
    }



    public void CheckGeographyAnswers(Int32[] Answers, String Login)
    {
        if (GeographyQuestionNow > 19) 
        {
            GeographyQuestionNow = 0;
        }
        if (Answers.Length > Geography[GeographyQuestionNow].RigthAnswers.Length || Answers.Length < Geography[GeographyQuestionNow].RigthAnswers.Length) { return; }
        

        bool AddPoint = false;

        for (int i = 0; i < Answers.Length; i++)
        {
            for (int c = 0; c < Geography[GeographyQuestionNow].RigthAnswers.Length; c++)
            {
                if (Answers[i] == Geography[GeographyQuestionNow].RigthAnswers[c])
                {
                    AddPoint = true;
                    i++;
                }
                else
                {
                    AddPoint = false;
                    break;
                }
            }

        }
        if (AddPoint)
        {
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if (AllUsers[i].Login == Login)
                {
                    AllUsers[i].GeographyRightAnswers++;
                }
            }
        }

        GeographyQuestionNow++;
    }

    public void StartGeographyQuiz(String Login)
    {
        for (int i = 0; i < AllUsers.Count; i++)
        {
            if (AllUsers[i].Login == Login)
            {
                AllUsers[i].GeographyRightAnswers = 0;
            }
        }
        Int32 Count = 0;
        Int32[] UserAnswers = new int[0];

        for (int i = 0; i < GeographyCount; i++)
        {
            Console.WriteLine(Geography[i]);
            Console.WriteLine("Enter count of right numbers :\n");
            Count = Int32.Parse(Console.ReadLine());

            UserAnswers = new Int32[Count];

            for (int b = 0; b < Count; b++)
            {
                Console.WriteLine("Enter right index : ");
                UserAnswers[b] = Int32.Parse(Console.ReadLine());
                CheckGeographyAnswers(UserAnswers, Login);
            }
        }


    }



    public void TopGeography()
    {
        if (AllUsers.Count == 0)
        {
            Console.WriteLine("There are no users");
            return;
        }

        if (Geography.Count == 0)
        {
            Console.WriteLine("There are no questions in quiz ");
            return;
        }





        List<Int32> Top = new List<Int32>();

        for (int i = 0; i < AllUsers.Count; i++)
        {
            Top.Add(AllUsers[i].GeographyRightAnswers);
        }

        Top.Sort();

        Console.Clear();
        for (int i = 0; i < Top.Count; i++)
        {
            for (int b = 0; b < AllUsers.Count; b++)
            {
                if (Top[i] == AllUsers[b].GeographyRightAnswers)
                {
                    Console.WriteLine($"Place : {i + 1}.{AllUsers[b].Login}");
                }
            }
        }

    }



    #endregion


    #region RandomMethods

    public void CreateRandom()
    {
        if (!IsRandomEmpty())
        {
            RandomQuiz.Clear();
        }

        Int32 MathIndex = 0;
        Int32 GeographyIndex = 0;
        Int32 HistoryIndex = 0;

        if (Math.Count == 0 || Geography.Count == 0 || History.Count == 0)
        {
            Console.WriteLine("Not all quizzes have been created");
            return;
        }

        Random random = new Random();

        for (int i = 0; i < 3; i++)
        {
            Int32 Type = random.Next(1, 3);
            if (Type == 1)
            {
                RandomQuiz.Add(Math[MathIndex]);
                MathIndex++;
            }

            if (Type == 2)
            {
                RandomQuiz.Add(History[HistoryIndex]);
                HistoryIndex++;
            }

            if (Type == 3) 
            {
                RandomQuiz.Add(Geography[GeographyIndex]);
                GeographyIndex++;
            }


        }

        RandomQuizCount++;

    }

    public bool IsRandomEmpty()
    {
        if (RandomQuiz.Count == 0) return true;
        return false;
    }

    public void CheckRandomAnswers(Int32[] Answers, String Login)
    {
        if (RandomQuizQuestionNow > 19) 
        {
            RandomQuizQuestionNow = 0;
            RandomQuizCount = 1;
        }
        if (Answers.Length > RandomQuiz[RandomQuizQuestionNow].RigthAnswers.Length || Answers.Length < RandomQuiz[RandomQuizQuestionNow].RigthAnswers.Length) { return; }


        bool AddPoint = false;

        for (int i = 0; i < Answers.Length; i++)
        {
            for (int c = 0; c < RandomQuiz[RandomQuizQuestionNow].RigthAnswers.Length; c++)
            {
                if (Answers[i] == RandomQuiz[RandomQuizQuestionNow].RigthAnswers[c])
                {
                    AddPoint = true;
                    i++;
                }
                else
                {
                    AddPoint = false;
                    break;
                }
            }

        }
        if (AddPoint)
        {
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if (AllUsers[i].Login == Login)
                {
                    AllUsers[i].RandomRightAnswers++;
                }
            }
        }

        RandomQuizQuestionNow++;
    }

    public void StartRandomyQuiz(String Login)
    {
        for (int i = 0; i < AllUsers.Count; i++)
        {
            if (AllUsers[i].Login == Login)
            {
                AllUsers[i].RandomRightAnswers = 0;
            }
        }
        Int32 Count = 0;
        Int32[] UserAnswers = new int[0];

        for (int i = 0; i < RandomQuizCount; i++)
        {
            Console.WriteLine(RandomQuiz[i]);
            Console.WriteLine("Enter count of right numbers :\n");
            Count = Int32.Parse(Console.ReadLine());

            UserAnswers = new Int32[Count];

            for (int b = 0; b < Count; b++)
            {
                Console.WriteLine("Enter right index : ");
                UserAnswers[b] = Int32.Parse(Console.ReadLine());
                CheckRandomAnswers(UserAnswers, Login);
            }
        }
    }


    public void TopRandom()
    {
        if (AllUsers.Count == 0)
        {
            Console.WriteLine("There are no users");
            return;
        }

        if (RandomQuiz.Count == 0)
        {
            Console.WriteLine("There are no questions in quiz ");
            return;
        }





        List<Int32> Top = new List<Int32>();

        for (int i = 0; i < AllUsers.Count; i++)
        {
            Top.Add(AllUsers[i].RandomRightAnswers);
        }

        Top.Sort();

        Console.Clear();
        for (int i = 0; i < Top.Count; i++)
        {
            for (int b = 0; b < AllUsers.Count; b++)
            {
                if (Top[i] == AllUsers[b].RandomRightAnswers)
                {
                    Console.WriteLine($"Place : {i + 1}.{AllUsers[b].Login}");
                }
            }
        }

    }
    #endregion


    #region Checks
    public bool CheckLoginsInBase(String login)
    {
        for (int i = 0; i < AllUsers.Count; i++)
        {
            if (AllUsers[i].Login == login)
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckAdminsInBase(String admin)
    {
        for (int i = 0; i < AllAdministration.Count; i++)
        {
            if (AllAdministration[i].Login == admin)
            {
                return true;
            }
        }
        return false;
    }
    #endregion


    #region LogIns


    public bool LogIn(String login,String password)
    {
        for (int i = 0; i < AllUsers.Count; i++)
        {
            if (AllUsers[i].Login == login)
            {
                if (AllUsers[i].Password == password)
                return true;
            }
        }
        return false;
    }

    public bool AdminLogIn(String login, String password)
    {
        for (int i = 0; i < AllAdministration.Count; i++)
        {
            if (AllAdministration[i].Login == login)
            {
                if (AllAdministration[i].Password == password)
                    return true;
            }
        }
        return false;
    }

    #endregion


    #region ChangeSettings
    public void ChangePassword(String login,String password)
    {
        for (int i = 0; i < AllUsers.Count; i++)
        {
            if (AllUsers[i].Login == login)
            {
                AllUsers[i].Password = password;
            }
        }
    }

    public void ChangeDateOfBirth(String login,String date) 
    {
        for (int i = 0; i < AllUsers.Count; i++)
        {
            if (AllUsers[i].Login == login)
            {
                AllUsers[i].BirthDate = date;
            }
        }    
    }

    #endregion


    public void CheckResults(String Login)
    {
        for (int i = 0; i < AllUsers.Count; i++)
        {
            if (AllUsers[i].Login == Login)
            {
                Console.WriteLine($"Math : {AllUsers[i].MathRightAnswers} points \nGeography : {AllUsers[i].GeographyRightAnswers} ponits \nHistory : {AllUsers[i].HistoryRightAnswers} points \nRandom : {AllUsers[i].RandomRightAnswers}");
            }
        }
    }
}