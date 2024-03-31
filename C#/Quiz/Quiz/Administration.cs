class Administration
{
    public String Login { get; set; }
    public String Password { get; set; }
    public String BirthDate { get; set; }

    public Administration() { }
    public Administration(String login, String password, String birthdate) 
    {
        Login = login;
        Password = password;
        BirthDate = birthdate;
    
    }

    public override string ToString()
    {
        return $"Login : {Login} \n Password {Password}";
    }
}