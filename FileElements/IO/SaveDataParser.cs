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
        static string saveDir = "";
        public static string myGamesDir;
        public static bool savedStatus = false;
        static XmlSerializer serializer;

        static List<SaveData> saveStates = new List<SaveData>();

        public static List<SaveData> SaveStates
        {
            get { return saveStates; }
        }

        public SaveDataParser()
        {
            string myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string myGames = Path.Combine(myDocs, "My Games\\Unnamed RPG");
            myGamesDir = myGames;
            saveDir = Path.Combine(myGames, "savedata.xml");

            if (!Directory.Exists(myGames))
                Directory.CreateDirectory(myGames);

            FileStream dataStream = File.Open(saveDir, FileMode.OpenOrCreate);
            dataStream.Close();

            serializer = new XmlSerializer(typeof(List<SaveData>));

            if (!(new FileInfo(saveDir).Length == 0))
                LoadData();
        }

        public static void LoadData()
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

        public static void SaveState(SaveData saveInfo)
        {
            bool overridedData = false;
            savedStatus = true;

            saveStates.ForEach(delegate(SaveData data)
            {
                if (saveInfo.Name.Equals(data.Name))
                {
                    overridedData = true;
                    saveStates[saveStates.IndexOf(data)] = saveInfo;
                }
            });

            if (!overridedData && saveStates.Count < 5)
                saveStates.Add(saveInfo);
					 
            Stream dataStream = File.Create(saveDir);
            try
            {
                serializer.Serialize(dataStream, saveStates);
            }
            finally
            {
                dataStream.Close();
            }
        }

        public static SaveData LoadGameState(string name)
        {
            try
            {
                return saveStates.Find(s => s.Name.ToLower() == name.ToLower());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw new Exception("Save Data: Save name not found!");
            }
        }

        public static SaveData GetSaveAtIndex(int index)
        {
            SaveData saveState = null;

            try
            {
                saveState = saveStates[index];
            }
            catch (ArgumentOutOfRangeException ex)
            {
                System.Diagnostics.Debug.WriteLine("The save cannot be found!\n" + ex.ToString());
            }

            return saveState;
        }
    }
}
