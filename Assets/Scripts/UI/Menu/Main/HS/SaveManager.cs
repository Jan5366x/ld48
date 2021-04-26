using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    private static readonly SaveHighScoreData CurrentLocalData = new SaveHighScoreData();
    
    public static void AddResult(PlayerHighScoreItem item)
    {
        var data = Load();

        var list = data.Results.OrderBy(o => o.Record).ToList();
        var removeOldest = list.First();
        list.Remove(removeOldest);
        list.Insert(0, item);

        data.Results = list.ToArray();
        Save(data);
    }

    private static string GetFilename()
    {
        return $"{Application.persistentDataPath}/highscore.bin";
    }

    public static SaveHighScoreData Load()
    {
        try
        {
            if (!File.Exists(GetFilename()))
            {
                Debug.Log("No Data exist. Create new data container");
                return new SaveHighScoreData();
            }

            var binaryFormatter = new BinaryFormatter();
            var stream = new FileStream(GetFilename(), FileMode.Open);

            var data = (SaveHighScoreData) binaryFormatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        return CurrentLocalData;
    }

    private static void Save(SaveHighScoreData data)
    {
        try
        {
            var binaryFormatter = new BinaryFormatter();
            var stream = new FileStream(GetFilename(), FileMode.Create);

            binaryFormatter.Serialize(stream, data);
            stream.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}