using System.Collections;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


    public static class SaveManager 
    {
    
    public static void Saving<T>(T ojb , string filename) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + filename;
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, ojb);
        stream.Close();
    }
    public static T Loading<T>(string filename) 
    {
        string path = Application.persistentDataPath + "/" + filename;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            T objectToLoad = (T)formatter.Deserialize(stream);
            stream.Close();

            return objectToLoad;
        }
        else
        {
            Debug.LogError("File not found at " + path);
            return default(T);
        }
    }



    }
       
    
