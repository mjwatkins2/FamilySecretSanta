using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FamilySecretSanta
{
    class MatchMaker
    {
        private Random rand = new Random();

        public List<Match> MatchMakerMatchMakerMakeMeAMatch(Family family)
        {
            while (true)
            {
                var matches = new List<Match>();
                var gifteesRemaining1 = family.Members.ToList();
                var gifteesRemaining2 = family.Members.ToList();

                // random matching
                foreach (var giftor in family.Members)
                {
                    var giftee1 = gifteesRemaining1[rand.Next(0, gifteesRemaining1.Count)];
                    gifteesRemaining1.Remove(giftee1);
                    var giftee2 = gifteesRemaining2[rand.Next(0, gifteesRemaining2.Count)];
                    gifteesRemaining2.Remove(giftee2);

                    matches.Add(new Match
                    {
                        Giftor = giftor,
                        Giftee1 = giftee1,
                        Giftee2 = giftee2
                    });
                }

                // check whether random matching is acceptable
                var spouses = family.Spouses;
                bool success = true;
                foreach (var match in matches)
                {
                    if (match.Giftor == match.Giftee1) // don't give present to self
                        success = false;
                    if (match.Giftor == match.Giftee2) // don't give present to self
                        success = false;
                    if (match.Giftee1 == match.Giftee2) // don't give two presents to same person
                        success = false;
                    if (spouses.ContainsKey(match.Giftor))
                    {
                        var giftorSpouse = spouses[match.Giftor];
                        if (giftorSpouse == match.Giftee1 || giftorSpouse == match.Giftee2) // don't match spouses
                            success = false;
                    }
                    if (spouses.ContainsKey(match.Giftee1))
                    {
                        if (spouses[match.Giftee1] == match.Giftee2) // don't give a present to both spouses in a pair
                            success = false;
                    }

                    if (!success)
                        break;
                }

                if (!success)
                    continue;

                // double-check 
                foreach (var giftor in family.Members)
                {
                    if (matches.Count(m => m.Giftor == giftor) != 1) // each name should appear *once* in matches giftor
                    {
                        Debug.Assert(false);
                        success = false;
                    }

                    if (matches.Count(m => m.Giftee1 == giftor || m.Giftee2 == giftor) != 2) // each name should appear *twice* in matches giftees
                    {
                        Debug.Assert(false);
                        success = false;
                    }
                }

                if (success)
                    return matches;
            }

            throw new Exception("Failed. :-(");
        }

        /// <summary>
        /// Match each family member with two gift recipients
        /// </summary>
        public class Match
        {
            public FamilyMember Giftor, Giftee1, Giftee2;
        }
    }
}
