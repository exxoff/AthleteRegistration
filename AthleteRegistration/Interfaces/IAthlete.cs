namespace AthleteRegistration.Interfaces
{
    public interface IAthlete
    {
        int Bib { get; set; }
        string EMail { get; set; }
        string FirstName { get; set; }
        string Group { get; set; }
        string LastName { get; set; }
        string WaveNumber { get; set; }
    }
}