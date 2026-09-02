using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

    public class InputManager : MonoBehaviour
    {
    public static InputManager instance;
    public Camera point;


    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(this);
        }
        else 
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            RayCasting();
        }
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            
            Debug.Log(AstarPathfinding.instance.ValidPath(AstarPathfinding.instance.StartPoint, AstarPathfinding.instance.EndPoint, Node._nodesMap.Count,ref AstarPathfinding.instance.path, false));
            foreach(var no in AstarPathfinding.instance.path) 
            {
                no.ChangeRoad();

            }
        }

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if(AstarPathfinding.instance.path.Count < 1) 
            {
                Debug.Log("path not generate");
                return;
            }
            Spwaner.instance.SpwanWave();
           
        }
    }

    void RayCasting() 
    {
        Ray ray = point.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit , 500f)) 
        {
           // Debug.Log(hit.collider.name);
            if (CheckNode(hit.collider.gameObject)) 
            {
                BuildManager.instance.DeselectedTower();
                Node target = GetNodeInstance(hit.collider.gameObject);
                BuildManager.instance.SetSelectedNote(target);
                
            }
            else if (CheckTower(hit.collider.gameObject)) 
            {
                BuildManager.instance.DeselectedNode();
                TowerBase target = GetTowerInstance(hit.collider.gameObject);
                BuildManager.instance.SetSelectedTower(target);
            }
            else 
            {
                BuildManager.instance.DeselectedTower();
                BuildManager.instance.DeselectedNode();
            }
        }
    }
    bool CheckNode(GameObject go) 
    {
        return go.GetComponent<Node>();
    }
    Node GetNodeInstance(GameObject go) 
    {
        return go.GetComponent<Node>();
    }
    bool CheckTower(GameObject go) 
    {
        return go.GetComponent<TowerBase>();
    }
    TowerBase GetTowerInstance(GameObject go) 
    {
        return go.GetComponent<TowerBase>();
    }

}
