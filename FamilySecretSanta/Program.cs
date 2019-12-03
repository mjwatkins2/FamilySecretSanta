using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FamilySecretSanta
{
    class Program
    {
        static void Main(string[] args)
        {
            Family family = new Family();
            MatchMaker mm = new MatchMaker();

            // uncomment to check for any bias in match-making and settle family complaints!
            //CheckBias();

            var matches = mm.MatchMakerMatchMakerMakeMeAMatch(family);
            using (Emailer emailer = new Emailer())
            {
                if (!emailer.Connect())
                    return;

                int sentCount = 0;
                foreach (var match in matches)
                {
                    if (!emailer.SendEmail(match.Giftor, match.Giftee1, match.Giftee2))
                        break;
                    sentCount++;
                }
                MessageBox.Show(string.Format("{0} of {1} e-mails sent!", sentCount, matches.Count));
            }
        }

        static void CheckBias(Family family, MatchMaker mm)
        {
            var matchCounts = new Dictionary<string, Dictionary<string, int>>();

            for (int i = 0; i < 10000; i++)
            {
                var matches = mm.MatchMakerMatchMakerMakeMeAMatch(family);
                foreach (var match in matches)
                {
                    if (!matchCounts.ContainsKey(match.Giftor.Name))
                    {
                        matchCounts.Add(match.Giftor.Name, new Dictionary<string, int>());
                    }
                    if (!matchCounts[match.Giftor.Name].ContainsKey(match.Giftee1.Name))
                    {
                        matchCounts[match.Giftor.Name].Add(match.Giftee1.Name, 0);
                    }
                    if (!matchCounts[match.Giftor.Name].ContainsKey(match.Giftee2.Name))
                    {
                        matchCounts[match.Giftor.Name].Add(match.Giftee2.Name, 0);
                    }
                    matchCounts[match.Giftor.Name][match.Giftee1.Name]++;
                    matchCounts[match.Giftor.Name][match.Giftee2.Name]++;
                }
            }
        }
    }
}
