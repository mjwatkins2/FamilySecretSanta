namespace FamilySecretSanta
{
    class FamilyMember
    {
        public FamilyMember(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
    }
}
