using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class Node : MonoBehaviour, IHeapItem<Node>
{
    public int Id { get; set; }
    public int Gcost;
    public int Hcost;
    public int Fcost { get { return Gcost + Hcost; } }
    public Node parent;
    public bool walkalbe;
    public Vector2 selfPosition;
    public static Vector2[] directions = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    public static Dictionary<Vector2, Node> _nodesMap = new Dictionary<Vector2, Node>();
    public Transform MiddlePoint;
    public Material grass;
    public Material block;
    public Material Road;
    public Material Selected;

    public void NodeSelect(bool deselected) 
    {
        if (deselected) 
        {
            CheckDefault();
        }
        else 
        {
            ChangeMaterial(Selected);
        }
    }
    void ChangeMaterial(Material mat) 
    {
        gameObject.GetComponent<Renderer>().material = mat;
    }
    public void ChangeRoad() 
    {
        ChangeMaterial(Road);
    }
    public void CheckDefault() 
    {
        if (walkalbe) 
        {
            ChangeMaterial(grass);
        }
        else 
        {
            ChangeMaterial(block);
        }
    }
    public void ChangeWalkable() 
    {
        if (walkalbe) 
        {
            walkalbe = false;
            ChangeMaterial(block);
        }
        else 
        {
            walkalbe = true;
            ChangeMaterial(grass);
        }
    }
    public void Start()
    {
        selfPosition = new Vector2(transform.position.x, transform.position.z);
        walkalbe = true;
        _nodesMap.Add(selfPosition, this);
    }
    public int CompareTo(Node other)
    {
        int compare = this.Fcost.CompareTo(other.Fcost);
        if (compare == 0)
        {
            compare = this.Hcost.CompareTo(other.Hcost);
        }
        return -compare;
    }
    public static int GetDistance(Node a, Node b)
    {
        int _disX = (int)Mathf.Abs(a.selfPosition.x - b.selfPosition.x);
        int _disY = (int)Mathf.Abs(a.selfPosition.y - b.selfPosition.y);
        /*
         *  4+ directions 
         * if(_disX > _dixY)
         * {
         *      return 14* _dixY + 10 * (_disX - _disY);
         * }
         * else
         * {
         *      return 14* _dixX + 10* (_dixY - _dixX);
         * }
         * 
         */

        return _disX + _disY;
    }
    public static void ResetParentToNull() 
    {
        foreach(var node in _nodesMap.Values) 
        {
            node.parent = null;
            
        }
    }
    public static List<Node> GetNeighbours(Node node)
    {

        List<Node> neighbours = new List<Node>();
        foreach (Vector2 direction in directions)
        {
            Vector2 currentPos = node.selfPosition + direction;
            if (_nodesMap.ContainsKey(currentPos))
            {
                neighbours.Add(_nodesMap[currentPos]);
            }
        }
        return neighbours;
    }
    
}
