using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;



public class EnemesScript : MonoBehaviour
{
    public int startHealth;
    public int id;
    public float speedDefault;
    public float speed;
    public int health;
    public int coinValue;
    public static List<Node> Path;
    Transform target;
    int Count;
    public static int EnemysCount;
    public Animator anie;
    public bool impacts;
    public bool Slow;
    public Transform HitPoint;
    //public TowerScript tower;
    

    public bool CurrentlyImpact() 
    {
        return impacts;
    }

    public bool IsDead() 
    {
        return health < 1;
    }
    private void Start()
    {
        speedDefault = speed;
    }
    private void OnEnable()
    {
        Slow = false;
        health = startHealth;
        Path = AstarPathfinding.instance.path;
        if (Path.Count < 1)
        {
            Debug.Log("Something Wrong");
            return;
        }
        anie.SetBool("RUN", true);
        
        EnemysCount++;
        speedDefault = speed;
       // Count = 0;
        target = Path[Count].MiddlePoint.transform;
    }
    void NextTarget() 
    {
        if(Count < Path.Count - 1) 
        {
            Count++;
            target = Path[Count].MiddlePoint.transform;
            
        }
        else 
        {
            //Debug.Log("Reached Goal");
           // PlayerData.instance.TakeLife();
            DisableGameobject();
        }
    }
    private void Update()
    {
        FollowPath();
    }
    private void OnDisable()
    {
        EnemysCount--;
        
    }
    public void FollowPath() 
    {
        if (target == null) 
        {
            Debug.Log("?");
        }
        if(health < 1) 
        {
            return;
        }
        Vector3 pos = target.position;
        
          
                pos.y = 0.36f;
        this.gameObject.transform.LookAt(pos) ;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //Vector3 directions = target.position - transform.position;
       // transform.LookAt(target.position);
        //transform.Translate(directions.normalized * speed * Time.deltaTime);
        if(Vector3.Distance(target.position,transform.position) < 0.5f) 
        {
            NextTarget();
        }
    }
    public void TakeDamage(int damage,TowerScript who) 
    {
        health -= damage;
        if(health <= 0) 
        {

            who.targets.Remove(this);
            StartCoroutine(Dying());
            // PlayerData.instance.AddCoin(this.coinValue);

            //DisableGameobject();
        }
    }
    public void TakeDamage(int damage, TowerBase who)
    {
        health -= damage;
        if (health <= 0)
        {

           // who.RemovingSelf(this);
            StartCoroutine(Dying());
            // PlayerData.instance.AddCoin(this.coinValue);

            //DisableGameobject();
        }
    }
    IEnumerator Dying() 
    {
        
        anie.SetBool("RUN", false);
        anie.SetBool("Death", true);
        yield return new WaitForSeconds(1.4f);
        DisableGameobject();

    }

    public void DisableGameobject()
    {
        Count = 0;
        this.gameObject.SetActive(false);

        transform.position = ObjectPool.instance.offScreen.position;


    }
    
   public IEnumerator  SlowEffect(float value,float durations) 
    {
        if (!Slow) 
        {
            speedDefault = speed;
            Slow = true;
            speed = speed * value;
            Debug.Log("what");
            yield return new WaitForSeconds(0.5f);
            
        }
        speed = speedDefault;
        Debug.Log("whyu");
        Slow = false;

    }
}
