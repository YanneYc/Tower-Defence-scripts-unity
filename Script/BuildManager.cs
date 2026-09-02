using System.Collections;
using UnityEngine;



    public class BuildManager : MonoBehaviour 
    { 
    public Node SelectedNote;
    public TowerBase SelectedTower;
    GameObject towerToBuild;
    public static BuildManager instance;

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
        if (Input.GetKeyDown(KeyCode.B)) 
        {
            SetBlock();
        }
        if(SelectedTower != null) 
        {
            if (Input.GetKeyDown(KeyCode.U)) 
            {
                SelectedTower.Upgrade();
                
            }
            if (Input.GetKeyDown(KeyCode.A)) 
            {
                SelectedTower.Sell();
                DeselectedTower();
            }
        }
        if (SelectedNote != null) 
        {
            if (Input.GetKeyDown(KeyCode.O)) 
            {
                if (CanBuild(SelectedNote))
                {
                    BuildTower(TestBuilder.currentSelectId, SelectedNote);
                }
            }
            
            if (Input.GetKeyDown(KeyCode.R)) 
            {
                if (CanBuild(SelectedNote)) 
                {
                    BuildTower(7, SelectedNote);
                }
                else 
                {
                    Debug.Log("Can't build there");
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (CanBuild(SelectedNote))
                {
                    BuildTower(12, SelectedNote);
                }
                else
                {
                    Debug.Log("Can't build there");
                }
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (CanBuild(SelectedNote))
                {
                    BuildTower(15, SelectedNote);
                }
                else
                {
                    Debug.Log("Can't build there");
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (CanBuild(SelectedNote))
                {
                    BuildTower(0, SelectedNote);
                }
                else
                {
                    Debug.Log("Can't build there");
                }
            }


            if (Input.GetKeyDown(KeyCode.T)) 
                {
                    if (CanBuild(SelectedNote)) 
                    {
                        BuildTower(1, SelectedNote);
                    }
                    else 
                    {
                        Debug.Log("Can't build there");
                    }
                }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                if (CanBuild(SelectedNote))
                {
                    BuildTower(2, SelectedNote);
                }
                else
                {
                    Debug.Log("Can't build there");
                }
            }

        }
        /*if (Input.GetKeyDown(KeyCode.T)) 
        {
            if(SelectedNote == null) 
            {
                Debug.Log("No Note has been select");
                return;
            }
            if (CanBuild(SelectedNote)) 
            {
                BuildTower(0, SelectedNote);
            }
            else 
            {
                Debug.Log("Can't build there");
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (SelectedNote == null)
            {
                Debug.Log("No Note has been select");
                return;
            }
            if (CanBuild(SelectedNote))
            {
                BuildTower(1, SelectedNote);
            }
            else
            {
                Debug.Log("Can't build there");
            }
        }*/
    }
    public void SetBlock() 
    {
        if (SelectedNote == null) 
        {
            Debug.Log("Something Wrong");
            return;
        }
        SelectedNote.ChangeWalkable();
    }
    public void SetSelectedTower(TowerBase tower) 
    {
        DeselectedTower();
        SelectedTower = tower;
        SelectedTower.TowerSelected();
    }
    public void DeselectedTower() 
    {
        if(SelectedTower!= null) 
        {
            SelectedTower.TowerDeSelected();
            SelectedTower = null;
        }
    }
    public void SetSelectedNote(Node node) 
    {
        DeselectedNode();
        SelectedNote = node;
        SelectedNote.NodeSelect(false);
    }
    public void DeselectedNode() 
    {
        if (SelectedNote != null) 
        {
            SelectedNote.NodeSelect(true);
            SelectedNote = null;
        }
    }
    public bool CanBuild(Node node) 
    {
        if (!node.walkalbe || node== AstarPathfinding.instance.StartPoint) 
        {
            Debug.Log("something there already");
            return false;
        }
        node.walkalbe = false;
        bool valid = AstarPathfinding.instance.CheckPath();
        node.walkalbe = true;
        return valid;
    }
    
    public void BuildTower(int id,Node targetNode) 
    {
        targetNode.walkalbe = false;
        towerToBuild = ObjectPool.instance.PullTower(id);
        Vector3 buildPos = targetNode.MiddlePoint.position;
        buildPos.y += 0.4f;
        towerToBuild.GetComponent<TowerBase>().SetLocation(targetNode);
        //towerToBuild.GetComponent<TowerScript>().currentLocation = buildPos;
        towerToBuild.transform.position = buildPos;
        towerToBuild.SetActive(true);
        SetSelectedTower(towerToBuild.GetComponent<TowerBase>());

    }
    

       
    }
