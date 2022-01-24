namespace Gerenciador.Enquete
{
    /// <summary>
    /// Interface que define os métodos de salvamento e carregamento de dados usando arquivos.
    /// </summary>
    interface IStorable
    {
        /// <summary>
        /// Grava os dados.
        /// </summary>
        /// <param name="writer">Stream de escrita.</param>
        void Save(BinaryWriter writer);

        /// <summary>
        /// Lê os dados.
        /// </summary>
        /// <param name="reader">Stream de leitura.</param>
        void Load(BinaryReader reader);
    }
}
