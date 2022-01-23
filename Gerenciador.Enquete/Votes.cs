namespace Gerenciador.Enquete
{
    partial class Survey
    {
        private class Votes : IStorable
        {
            private Survey survey;

            private IDictionary<Option, int> votes = new Dictionary<Option, int>();

            public int VoteCount { get; private set; }

            public Votes(Survey survey)
            {
                this.survey = survey;
            }

            public void AddVote(Option option)
            {
                int count;

                if (votes.TryGetValue(option, out count))
                {
                    count++;
                    votes[option] = count;
                }
                else
                {
                    votes[option] = 1;
                }

                VoteCount++;
            }

            public List<OptionScore> CalculateScores(bool sort = true)
            {
                List<OptionScore> scores = new List<OptionScore>();

                foreach (KeyValuePair<Option, int> entry in votes)
                {
                    scores.Add(new OptionScore(entry.Key, entry.Value));
                }

                if (sort)
                {
                    scores.Sort();
                }

                return scores;
            }

            public void Save(BinaryWriter writer)
            {
                writer.Write(votes.Count);

                foreach (KeyValuePair<Option, int> entry in votes)
                {
                    Option option = entry.Key;
                    int numVotes = entry.Value;

                    option.Save(writer);

                    writer.Write(numVotes);
                }
            }

            public void Load(BinaryReader reader)
            {
                int count = reader.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    Option option = new Option();

                    option.Load(reader);

                    int numVotes = reader.ReadInt32();

                    VoteCount += numVotes;

                    votes.Add(option, numVotes);
                }
            }
        }
    }
}
