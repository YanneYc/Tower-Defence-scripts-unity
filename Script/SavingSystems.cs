using System.Collections;
using UnityEngine;
using OdinSerializer;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


public class SavingSystems : MonoBehaviour
{
    string Path;
    TestData testData;
    TestData Load;
    string t = "save working";
    public static IList<ISaveble> savingList = new List<ISaveble>();

    

    private void Start()
    {
        testData = new TestData();
        Load = new TestData();
        Path =  Application.persistentDataPath + "/ " + "SaveFile1";
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            testData.t = t;
            foreach(ISaveble save in savingList) 
            {
                save.Save(ref testData);
            }
            SaveState(testData);
        }
        if (Input.GetKeyDown(KeyCode.L)) 
        {
           Load = LoadState();
            if (Load != null) 
            {
               // Debug.Log("Check");
                Debug.Log(Load.t);
                if(Load.towerLocations.Count < 1) 
                {
                    return;
                }
                foreach(var x in Load.towerLocations) 
                {
                    if(x.Key == null) 
                    {
                        continue;
                    }
                    Debug.Log( x.Key.ToString()+x.Value);
                    BuildManager.instance.BuildTower(x.Value, Node._nodesMap[x.Key]);
                }
            }
        }
    }
    public void SaveState(TestData data)
    {
        File.Delete(Path);
        byte[] bytes = SerializationUtility.SerializeValue(data, DataFormat.Binary);
        File.WriteAllBytes(Path, bytes);
    }

    public TestData  LoadState() 
    {
        //Debug.Log("load");
        if (!File.Exists(Path)) 
        {
            Debug.Log("No Save File have Found");
            return null;
        }
        byte[] bytes = File.ReadAllBytes(Path);
        return SerializationUtility.DeserializeValue<TestData>(bytes, DataFormat.Binary);
        
    }
}
