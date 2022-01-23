using System.Text;

namespace Gerenciador.Enquete
{
    partial class Survey : IStorable
    {
        private IDictionary<string, Option> options = new Dictionary<string, Option>();

        private Votes votes;

        public string Question { get; set; }

        public int VoteCount
        {
            get
            {
                return votes.VoteCount;
            }
        }

        public Survey()
        {
            votes = new Votes(this);
        }

        public void SetOption(string id, string text)
        {
            Option option = new Option();
            option.Id = id.ToUpper();
            option.Text = text;

            if (options.ContainsKey(id))
            {
                options[id] = option;
            }
            else
            {
                options.Add(id, option);
            }
        }

        public string GetFormattedSurvey()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Question);

            foreach (Option option in options.Values)
            {
                sb.Append(option.Id).Append(" - ").AppendLine(option.Text);
            }

            return sb.ToString();
        }

        public bool Vote(out Option option, out string vote)
        {
            vote = Console.ReadLine();

            vote = vote.ToUpper();

            bool valid = options.TryGetValue(vote, out option);

            if (valid)
            {
                votes.AddVote(option);
            }

            return valid;
        }

        public List<OptionScore> CalculateScores(bool sort = true)
        {
            return votes.CalculateScores(sort);
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Question);
            writer.Write(options.Count);

            foreach (Option option in options.Values)
            {
                option.Save(writer);
            }

            votes.Save(writer);
        }

        public void Load(BinaryReader reader)
        {
            Question = reader.ReadString();

            options = new Dictionary<string, Option>();

            int count = reader.ReadInt32();

            for (int i = 0; i < count; i++)
            {
                Option option = new Option();

                option.Load(reader);

                options[option.Id] = option;
            }

            votes.Load(reader);
        }
    }
}
