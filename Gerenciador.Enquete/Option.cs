namespace Gerenciador.Enquete
{
    class Option : IStorable, IEquatable<Option>
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write(Text);
        }

        public void Load(BinaryReader reader)
        {
            Id = reader.ReadString();
            Text = reader.ReadString();
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Option);
        }

        public bool Equals(Option? other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
