using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;



    public class TowerScript : MonoBehaviour 
    {
    public float range;
    public int attack;
    public int cost;
    public TowerScript Next;
    public GameObject visualisRange;
    public List<EnemesScript> targets;
    public float fireRate;
    float NextFire;
    public Transform FirePoint;
    public int id;
    public Projectile projectile;
    public bool canRotate;
    public GameObject Pivot;
    EnemesScript currenttarget;
    public Vector3 currentLocation;
    Node location;

    public void SetLoacation(Node n) 
    {
        location = n;
    }
    private void Start()
    {
        NextFire = 0;
    }
    private void Update()
    {
        CanAttack();
    }
    public void TowerSelected() 
    {
        visualisRange.SetActive(true);
    }
    public void TowerDeSelected() 
    {
        visualisRange.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (CheckIfEnenmy(other.gameObject))
        {
            targets.Add(GetEnemes(other.gameObject));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (CheckIfEnenmy(other.gameObject)) 
        {
            targets.Remove(GetEnemes(other.gameObject));
        }
    }
    bool CheckIfEnenmy(GameObject go) 
    {
        return go.GetComponent<EnemesScript>();
    }
    EnemesScript GetEnemes(GameObject go)
    {
        return go.GetComponent<EnemesScript>();
    }
    public virtual void Attack() 
    {
        Projectile bullet = ObjectPool.instance.PullBullets(id).GetComponent<Projectile>();
        bullet.transform.position = FirePoint.position;
        bullet.SetTarget(currenttarget, attack,this);
    }
    public virtual void SetTarget() 
    {
        TargetFirst();
       
        if (currenttarget.IsDead() && targets.Count > 0)
        {
            targets.Remove(currenttarget);
            TargetFirst();
        }
    }
    public void UpGrade() 
    {
        if(Next == null) 
        {
            Debug.Log("No upgrade available");
            return;
        }
        GameObject next =  ObjectPool.instance.PullTower(Next.id);
        
        next.transform.position = currentLocation;
        next.GetComponent<TowerScript>().TowerDeSelected();
        next.SetActive(true);
        //BuildManager.instance.SetSelectedTower(Next.GetComponent<TowerScript>());
        gameObject.SetActive(false);

    }
    public void Sell() 
    {
        gameObject.SetActive(false);
        location.walkalbe = true;
        location.NodeSelect(true);
    }
    private void TargetFirst()
    {
        if (targets.Count > 0)
        {
            currenttarget = targets[0];
        }
    }

    public virtual void CanAttack() 
    {
        if(targets.Count < 1) 
        {
            //Debug.Log("??");
            return;
        }
        SetTarget();
        if (canRotate && currenttarget!=null)
        {
            Vector3 ToLook = currenttarget.transform.position;
            ToLook.y = Pivot.transform.position.y;
            Pivot.transform.LookAt(ToLook);
        }
        if (Time.time > NextFire) 
        {
            //Debug.Log("attack");
                Attack();
            NextFire = Time.time + fireRate;
        }
        
        
    }


}
