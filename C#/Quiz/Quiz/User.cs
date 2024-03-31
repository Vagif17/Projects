class User
{
    public String Login { get; set; }
    public String Password { get; set; }
    public String BirthDate { get; set; }

    public Int32 MathRightAnswers { get; set; }
    public Int32 HistoryRightAnswers { get; set; }
    public Int32 GeographyRightAnswers { get; set; }
    public Int32 RandomRightAnswers { get; set; }

    public User() { }

    public User(string login, string password, string birthDate)
    {
        Login = login;
        Password = password;
        BirthDate = birthDate;
    }

    public override string ToString()
    {
        return $"Login : {Login} \n Password {Password} \n Date of birth : {BirthDate} ";
    }

}
