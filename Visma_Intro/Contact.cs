namespace Visma_Intro
{
    /// <summary>
    /// The class to store information about a single contact
    /// </summary>
    class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public Contact(string fName, string lName, string address)
        {
            FirstName = fName;
            LastName = lName;
            Address = address;
        }
        public override string ToString() => $"{FirstName}, {LastName}, {Address}";
    }
}
