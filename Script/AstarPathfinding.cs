using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;



    public class AstarPathfinding : MonoBehaviour
    {
    public Node StartPoint;
    public Node EndPoint;
    public List<Node> path;
    public static AstarPathfinding instance;

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
    public bool CheckPath() 
    {
        return ValidPath(StartPoint, EndPoint, Node._nodesMap.Count,ref path, true);
    }
    public bool ValidPath(Node StartNode , Node EndNode, int GridSize,ref List<Node> Path,bool check) 
    {
        Node.ResetParentToNull();
        if(path.Count > 0) 
        {
            foreach(var node in path) 
            {
                node.CheckDefault();
            }
            path.Clear();
        }
        MinHeap<Node> OpenList = new MinHeap<Node>(GridSize);
        HashSet<Node> CloseList = new HashSet<Node>();
        OpenList.Insert(StartNode);
        while(OpenList.Count() > 0)
        {

            Node current = OpenList.RemoveFirst();
            CloseList.Add(current);
            if(current == EndNode) 
            {
                if (!check) 
                {
                    Path = RetracePath(EndNode);
                }
                 return true;
            }
            foreach(Node neighbour in Node.GetNeighbours(current)) 
            {
                if(!neighbour.walkalbe || CloseList.Contains(neighbour)) 
                {
                    continue;
                }
                int CostToNext = current.Hcost + Node.GetDistance(current, neighbour);
                if(CostToNext < current.Hcost || !CloseList.Contains(neighbour)) 
                {
                    neighbour.parent = current;
                    neighbour.Hcost = CostToNext;
                    neighbour.Gcost = Node.GetDistance(neighbour, EndNode);
                }
                if (!OpenList.Contains(neighbour)) 
                {
                    OpenList.Insert(neighbour);
                }
                else 
                {
                    OpenList.ShiftUp(neighbour);
                }
            }
        }
          return false;
    }
    List<Node> RetracePath(Node Target)
       {
        List<Node> Path = new List<Node>();
        Node currentNode = Target;
        while(currentNode!= null) 
        {
            Path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Path.Reverse();
        return Path;
       }
    
    }
