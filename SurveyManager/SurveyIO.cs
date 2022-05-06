namespace SurveyManager
{
    /// <summary>
    /// Agrupa operações de I/O da aplicação.
    /// </summary>
    static class SurveyIO
    {
        /// <summary>
        /// Salva um objeto para um arquivo.
        /// </summary>
        /// <param name="obj">Objeto a ser salvo.</param>
        /// <param name="filePath">Arquivo de destino.</param>
        public static void SaveToFile(IStorable obj, string filePath)
        {
            FileInfo file = new FileInfo(filePath);

            using (BinaryWriter writer = new BinaryWriter(file.OpenWrite()))
            {
                obj.Save(writer);
            }
        }

        /// <summary>
        /// Carrega um objeto com dados de um arquivo.
        /// </summary>
        /// <param name="obj">Objeto a ser carregado.</param>
        /// <param name="filePath">Arquivo de origem.</param>
        public static void LoadFromFile(IStorable obj, string filePath)
        {
            FileInfo file = new FileInfo(filePath);

            using (BinaryReader reader = new BinaryReader(file.OpenRead()))
            {
                obj.Load(reader);
            }
        }
    }
}
