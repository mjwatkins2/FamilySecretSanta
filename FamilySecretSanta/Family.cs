using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilySecretSanta
{
    class Family
    {
        public Family()
        {
            Members = GenerateFamily();
            Spouses = GenerateSpouseDictionary();
        }

        public IReadOnlyList<FamilyMember> Members { get; private set; }
        public IReadOnlyDictionary<FamilyMember, FamilyMember> Spouses { get; private set; }

        private List<FamilyMember> GenerateFamily()
        {
            var family = new List<FamilyMember>
            {
                new FamilyMember("Me", "email@gmail.com"), // replace names and emails with actual names and emails for actual usage
                new FamilyMember("Spouse", "email@gmail.com"),
                new FamilyMember("Brother", "email@gmail.com"),
                new FamilyMember("SisterInLaw", "email@gmail.com"),
                new FamilyMember("Sister", "email@gmail.com"),
                new FamilyMember("Dad", "email@gmail.com"),
                new FamilyMember("Mom", "email@gmail.com")
            };
            return family;
        }

        private Dictionary<FamilyMember, FamilyMember> GenerateSpouseDictionary()
        {
            var spouseNamePairs = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Me", "Spouse"),
                new Tuple<string, string>("Brother", "SisterInLaw"),
                new Tuple<string, string>("Dad", "Mom")
            };

            Dictionary<FamilyMember, FamilyMember> spouses = new Dictionary<FamilyMember, FamilyMember>();
            foreach (var pair in spouseNamePairs)
            {
                var first = Find(pair.Item1);
                var second = Find(pair.Item2);
                spouses.Add(first, second);
                spouses.Add(second, first);
            }
            return spouses;
        }

        FamilyMember Find(string name)
        {
            return Members.First(member => member.Name == name);
        }
    }
}
