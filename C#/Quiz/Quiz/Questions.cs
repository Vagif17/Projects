class Questions
{
    public String Question { get; set; }

    public String[] Answers { get; set; } = new String[4];
    public Int32[] RigthAnswers { get; set; }


    public Questions() { }


    public void AddQuestion()
    {
        Console.Clear();
        Console.WriteLine("Enter Question : ");
        Question = Console.ReadLine();
        Console.Clear() ;
        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine("Enter Answers : ");
            Answers[i] = Console.ReadLine();
        }

        Console.WriteLine("Enter count of right answers : ");
        Int32 CorrectAnswers = 0;
        while (true)
        {
            bool CorrectAnswers_Check = Int32.TryParse(Console.ReadLine(), out CorrectAnswers);
            if (!CorrectAnswers_Check) { Console.WriteLine("Enter right number : "); }
            if (CorrectAnswers_Check) { break; }
        }


        this.RigthAnswers = new Int32[CorrectAnswers];

        Console.WriteLine("Enter index of right answers : ");
        for (int i = 0; i < CorrectAnswers; i++)
        {
            Int32 Answer = 0;
            while (true)
            {
                bool Answers_Check = Int32.TryParse(Console.ReadLine(), out Answer);
                if (!Answers_Check) { Console.WriteLine("Enter right number : "); }
                if (Answers_Check) { break; }
            }
            this.RigthAnswers[i] = Answer;
        }

        Console.Clear();
        
    }

   

    public override string ToString()
    {
        Console.WriteLine($"Question : {Question}");
        for (int i = 0; i < Answers.Length; i++)
        {
            Console.WriteLine($"Index {i} : {Answers[i]}");
        }
        return "";
    }
}
