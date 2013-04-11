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

        public const int MAXSAVES = 5;

        static List<SaveData> saveStates = new List<SaveData>();

        /// <summary>
        /// Returns a list of all save datas: max=5
        /// </summary>
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

        /// <summary>
        /// Loads the data from the xml save file into the List of saves
        /// </summary>
        private static void LoadData()
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

        /// <summary>
        /// Saves a save data state into the list and xml file
        /// </summary>
        /// <param name="saveInfo">Save state to save</param>
        public static void SaveState(SaveData saveInfo)
        {
            bool overridedData = false;
            savedStatus = true;

            // Loop through all saves and if a name already exists, it'll be overidden
            saveStates.ForEach(delegate(SaveData data)
            {
                if (saveInfo.Name.Equals(data.Name))    // if the name exists in the list
                {
                    overridedData = true;   // Trigger overridden data
                    saveStates[saveStates.IndexOf(data)] = saveInfo;
                }
            });

            // If no data has been overridden, append the save state
            if (!overridedData && saveStates.Count < MAXSAVES)
                saveStates.Add(saveInfo);
					 
            Stream dataStream = File.Create(saveDir);
            try
            {
                serializer.Serialize(dataStream, saveStates);   // Serialize the list into the xml file (dataStream)
            }
            finally
            {
                dataStream.Close();
            }
        }

        /// <summary>
        /// Loads a game state with a string input
        /// </summary>
        /// <param name="name">Name of person that owns the save state</param>
        /// <returns></returns>
        public static SaveData LoadGameState(string name)
        {
            try
            {
                // Quick fact: this is a lambda expression
                return saveStates.Find(s => s.Name.ToLower() == name.ToLower());    // "WTF is this," you might ask.  I don't even know...
                                                                                    // J/k it finds the save state in the list based on a name
                                                                                    // I wish I could use a dictionary but I cant de/serialize it in xml
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw new Exception("Save Data: Save name not found!");
            }
        }

        /// <summary>
        /// Gets a save at an index (1 - MAXSAVES)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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
