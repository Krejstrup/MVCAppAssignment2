namespace MVCAppAssignment2.Models.Data
{
    public class PersonLanguage
    {

        // Assossiation table to join the Many to Many (Languages and People)
        //

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

    }


}
