namespace Gerenciador.Enquete
{
    interface IStorable
    {
        void Save(BinaryWriter writer);
        void Load(BinaryReader reader);
    }
}
