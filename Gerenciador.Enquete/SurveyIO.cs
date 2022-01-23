namespace Gerenciador.Enquete
{
    static class SurveyIO
    {
        public static void SaveToFile(IStorable obj, string filePath)
        {
            FileInfo file = new FileInfo(filePath);

            using (BinaryWriter writer = new BinaryWriter(file.OpenWrite()))
            {
                obj.Save(writer);
            }
        }

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
