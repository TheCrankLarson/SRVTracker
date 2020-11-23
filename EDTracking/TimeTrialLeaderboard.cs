using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDTracking
{
    public class TimeTrialLeaderboard
    {
        public List<string> Contestants { get; set; } = new List<string>();
        public List<TimeSpan> CompletedTimes { get; set; } = new List<TimeSpan>();

        public TimeTrialLeaderboard()
        {
        }

        private void SwapEntries(int x, int y)
        {
            string c = Contestants[x];
            TimeSpan t = CompletedTimes[x];
            Contestants[x] = Contestants[y];
            CompletedTimes[x] = CompletedTimes[y];
            Contestants[y] = c;
            CompletedTimes[y] = t;
        }

        public void AddEntry(string participant, TimeSpan courseTime)
        {
            // We keep the times in order as we add them, to save sorting later

            if (Contestants.Contains(participant))
            {
                // We already have an entry, check whether this is faster
                int currentIndex = Contestants.IndexOf(participant);
                if (courseTime < CompletedTimes[currentIndex])
                {
                    // This time is faster, so check whether to move up leaderboard
                    CompletedTimes[currentIndex] = courseTime;
                    while (currentIndex > 0 && CompletedTimes[currentIndex] < CompletedTimes[currentIndex - 1])
                    {
                        SwapEntries(currentIndex, currentIndex - 1);
                        currentIndex--;
                    }
                }
                return;
            }

            // New entry, so work out where to add
            int i = 0;
            while (i < Contestants.Count && CompletedTimes[i] < courseTime)
                i++;
            Contestants.Insert(i, participant);
            CompletedTimes.Insert(i, courseTime);
        }

        public Dictionary<string,string> RaceStatistics()
        {
            return new Dictionary<string, string>();
        }
    }
}
