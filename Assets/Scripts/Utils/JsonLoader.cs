using UnityEngine;
using System.IO;

public class JsonLoader
{
    private static string savePath = Application.persistentDataPath + "/saves/";

    public static T Load<T>(string savefileName) where T : new()
    {
        string savePathFile = savePath + savefileName + ".json";

        if (!File.Exists(savePathFile))
        {
            T n = new T();
            Save<T>(n, savefileName);
            return n;
        }

        string file = File.ReadAllText(savePathFile);
        return JsonUtility.FromJson<T>(file);
    }

    public static void Save<T>(T data, string savefileName) where T : new()
    {
        string savePathFile = savePath + savefileName + ".json";

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        string file = JsonUtility.ToJson(data);
        File.WriteAllText(savePathFile, file);
    }
}
