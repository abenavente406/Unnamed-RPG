using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using ProjectElements.Data;

namespace ProjectElements.IO
{
    public class SaveDataParser
    {
        private string saveDir = "";
        private XmlSerializer serializer;

        private List<SaveData> saveStates = new List<SaveData>();

        public SaveDataParser()
        {
            string myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string myGames = Path.Combine(myDocs, "My Games\\Unnamed RPG");
            saveDir = Path.Combine(myGames, "savedata.xml");

            if (!Directory.Exists(myGames))
                Directory.CreateDirectory(myGames);

            FileStream dataStream = File.Open(saveDir, FileMode.OpenOrCreate);
            dataStream.Close();

            serializer = new XmlSerializer(typeof(List<SaveData>));

            if (!(new FileInfo(saveDir).Length == 0))
                LoadData();
        }

        public void LoadData()
        {
            Stream dataStream = File.Open(saveDir, FileMode.Open);

            try
            {
                saveStates = serializer.Deserialize(dataStream) as List<SaveData>;
            }
            catch (System.Xml.XmlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                dataStream.Close();
            }
        }

        public void SaveState(SaveData saveInfo)
        {
            saveStates.Add(new SaveData() { Name = saveInfo.Name, Location = saveInfo.Location, Health = saveInfo.Health });

            Stream dataStream = File.Open(saveDir, FileMode.Open);
            try
            {
                serializer.Serialize(dataStream, saveStates);
            }
            finally
            {
                dataStream.Close();
            }
        }

        public SaveData LoadGameState(string name)
        {
            try
            {
                return saveStates.Find(s => s.Name == name);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw new Exception("Save Data: Save name not found!");
            }
        }
    }
}
