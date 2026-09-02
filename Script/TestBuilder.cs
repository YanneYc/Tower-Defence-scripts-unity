using System.Collections;
using UnityEngine;



    public class TestBuilder : MonoBehaviour
    {
        public bool BuildTime;
        public static int currentSelectId;
    public int[] Selections;
    private void Start()
        {
            currentSelectId = -1;
            BuildTime = false;
        Selections = new int[] { 0, 1, 2, 7, 12, 15,18 };
    }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1)) 
            {
                ChangeBuildTime();
                Debug.Log($"BuildMode = {BuildTime}");
            }
            if (BuildTime) 
            {
                Sections();
            }
        }
        public void ChangeBuildTime() 
        {
            if (BuildTime) 
            {
                BuildTime = false;
                return;
            }
            BuildTime = true;
        }
        public void Sections() 
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) 
            {
                currentSelectId = Selections[0];
            BuildTime = false;
            Debug.Log($"Select {Selections[0]}");
        }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentSelectId = Selections[1];
            BuildTime = false;
            Debug.Log($"Select {Selections[1]}");
        }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentSelectId = Selections[2];
            BuildTime = false;
            Debug.Log($"Select {Selections[2]}");
        }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                currentSelectId = Selections[3];
            BuildTime = false;
            Debug.Log($"Select {Selections[3]}");
        }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                currentSelectId = Selections[4];
            BuildTime = false;
            Debug.Log($"Select {Selections[4]}");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentSelectId = Selections[5];
            BuildTime = false;
            Debug.Log($"Select {Selections[5]}");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            currentSelectId = Selections[6];
            BuildTime = false;
            Debug.Log($"Select {Selections[6]}");
        }

    }
    }
