namespace Gerenciador.Enquete
{
    /// <summary>
    /// Votos de uma opção.
    /// </summary>
    class OptionScore : IComparable<OptionScore>
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="option">Opção.</param>
        /// <param name="count">Número de votos.</param>
        public OptionScore(Option option, int count)
        {
            Option = option;
            Count = count;
        }

        /// <summary>
        /// Opção.
        /// </summary>
        public Option Option { get; private set; }

        /// <summary>
        /// Número de votos.
        /// </summary>
        public int Count { get; private set; }

        public int CompareTo(OptionScore other)
        {
            // Define a comparação como ordem decrescente de votos. Se duas opções tiverem o mesmo número de votos,
            // usa o críterio de ordem alfabética do texto da opção.

            int comp = -Count.CompareTo(other.Count);

            if (comp == 0)
            {
                return Option.Text.CompareTo(other.Option.Text);
            }

            return comp;
        }
    }
}
