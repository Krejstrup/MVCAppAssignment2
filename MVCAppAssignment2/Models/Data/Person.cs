namespace MVCAppAssignment2.Models.Data
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }


        public Person(int inId, string inFirstName, string inLastName, string inPhone, string inCity)
        {
            Id = inId;
            FirstName = inFirstName;
            LastName = inLastName;
            Phone = inPhone;
            City = inCity;
        }
    }
}
