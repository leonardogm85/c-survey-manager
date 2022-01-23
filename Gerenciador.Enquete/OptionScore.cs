namespace Gerenciador.Enquete
{
    internal class OptionScore : IComparable<OptionScore>
    {
        public OptionScore(Option option, int count)
        {
            Option = option;
            Count = count;
        }

        public Option Option { get; private set; }
        public int Count { get; private set; }

        public int CompareTo(OptionScore other)
        {
            int comp = -Count.CompareTo(other.Count);

            if (comp == 0)
            {
                return Option.Text.CompareTo(other.Option.Text);
            }

            return comp;
        }
    }
}
