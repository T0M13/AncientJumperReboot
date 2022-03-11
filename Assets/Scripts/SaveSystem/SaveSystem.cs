using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SaveData(int _highscore, int _coins)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.ancientJumper";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(_highscore, _coins);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/data.ancientJumper";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            if (stream == null)
            {
                stream.Close();
                Debug.LogWarning("Stream empty");
            }

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            //Debug.LogWarning("Save file not found in " + path);
            return null;
        }

    }


    public static void SaveOptionsData(float _masterVolume, float _musicVolume, float _effectVolume)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/options.ancientJumper";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(_masterVolume, _musicVolume, _effectVolume);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static SaveData LoadOptionsData()
    {
        string path = Application.persistentDataPath + "/options.ancientJumper";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            if (stream == null)
            {
                stream.Close();
                Debug.LogWarning("Stream empty");
            }

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            //Debug.LogWarning("Save file not found in " + path);
            return null;
        }

    }

}
