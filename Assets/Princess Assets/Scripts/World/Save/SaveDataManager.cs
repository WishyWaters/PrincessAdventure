using System.Collections.Generic;
using UnityEngine;
using System.Text;


namespace PrincessAdventure
{
    public static class SaveDataManager
    {
        public static void SaveGameDetails(int saveId, GameDetails gameToSave)
        {
            StringBuilder saveName = new StringBuilder();

            saveName.Append("PrincessAdvCore");
            saveName.Append(saveId.ToString());
            saveName.Append(".dat");
            

            if (FileManager.WriteToFile(saveName.ToString(), JsonUtility.ToJson(gameToSave)))
            {
                Debug.Log("Save successful");
            }
        }


        public static GameDetails LoadGameDetails(int saveId)
        {
            StringBuilder saveName = new StringBuilder();

            saveName.Append("PrincessAdvCore");
            saveName.Append(saveId.ToString());
            saveName.Append(".dat");

            GameDetails savedGame = null;

            if (FileManager.LoadFromFile(saveName.ToString(), out var savedGameJson))
            {
                savedGame = JsonUtility.FromJson<GameDetails>(savedGameJson);
            }

            return savedGame;
        }

        public static void SaveLevelDetails(int saveId, GameScenes scene, LevelSave levelToSave)
        {
            StringBuilder saveName = new StringBuilder();

            saveName.Append("PrincessAdvLevel");
            saveName.Append(saveId.ToString());
            saveName.Append("_");
            saveName.Append(scene.ToString());
            saveName.Append(".dat");


            if (FileManager.WriteToFile(saveName.ToString(), JsonUtility.ToJson(levelToSave)))
            {
                Debug.Log("Level Save successful");
            }
        }


        public static LevelSave LoadLevelDetails(int saveId, GameScenes scene)
        {
            StringBuilder saveName = new StringBuilder();

            saveName.Append("PrincessAdvLevel");
            saveName.Append(saveId.ToString());
            saveName.Append("_");
            saveName.Append(scene.ToString());
            saveName.Append(".dat");

            LevelSave savedLevel = new LevelSave();

            if (FileManager.LoadFromFile(saveName.ToString(), out var levelJson))
            {
                if(!string.IsNullOrEmpty(levelJson))
                    savedLevel = JsonUtility.FromJson<LevelSave>(levelJson);
                
                Debug.Log("Level Load complete");
            }

            return savedLevel;
        }

        public static void EraseSave(int saveId)
        {
            //Erase Core save
            StringBuilder coreSaveName = new StringBuilder();

            coreSaveName.Append("PrincessAdvCore");
            coreSaveName.Append(saveId.ToString());
            coreSaveName.Append(".dat");

            FileManager.DeleteFile(coreSaveName.ToString());

            //Erase scene saves
            foreach (string sceneName in GameScenes.GetNames(typeof(GameScenes)))
            {
                StringBuilder levelSaveName = new StringBuilder();

                levelSaveName.Append("PrincessAdvLevel");
                levelSaveName.Append(saveId.ToString());
                levelSaveName.Append("_");
                levelSaveName.Append(sceneName);
                levelSaveName.Append(".dat");

                FileManager.DeleteFile(levelSaveName.ToString());

            }
        }
    }
}