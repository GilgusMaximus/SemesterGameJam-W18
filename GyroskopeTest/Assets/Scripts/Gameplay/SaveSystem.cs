using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem { 

    public static void SaveData(CurrencyManager cm)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath+"/saveData.xD";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(cm);

        formatter.Serialize(stream, data);//format into binary

        Debug.Log("Saved at: " + path);
        stream.Close();
    }

    public static LevelData LoadData()
    {
        string path = Application.persistentDataPath + "/saveData.xD";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = (LevelData)formatter.Deserialize(stream);//laden der Daten

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Ohney, no saveData found at " + path);
            return null;
        }

    }

}
