using System.Collections;
using UnityEngine;



    public class Projectile : MonoBehaviour
    {
    EnemesScript Currenttarget;
    int atk;
    public float speed;
    TowerScript tower1;
    TowerBase tower;
    public HitImpacts hitImpact;
    public Vector3 impactNormal;
    public float delayTime;
    public int id;
    GameObject impacts;
    

    private void OnEnable()
    {
        
    }
    private void Update()
    {
        follow();
    }
    void follow() 
    {
        if (!gameObject.activeSelf) 
        {
            return;
        }
        if(Currenttarget == null || Currenttarget.IsDead()) 
        {
            
            Deactivate();
        }
        
        Vector3 direction = Currenttarget.HitPoint.position - transform.position;
        float distance = speed * Time.deltaTime;
        if(direction.magnitude <= distance) 
        {
            TargetHit();
            Currenttarget.impacts = false;
            Deactivate();
        }
        transform.Translate(direction.normalized * distance, Space.World);
        transform.LookAt(Currenttarget.transform.position);
        //transform.position = Vector3.MoveTowards(transform.position, Currenttarget.transform.position, speed * Time.deltaTime);
       

    }
    public void SetTarget(EnemesScript target,int attack,TowerScript to) 
    {
        tower1 = to;
        //Debug.Log("A");
        Currenttarget = target;
        atk = attack;
        this.gameObject.SetActive(true);
    }
    public void SetTarget(EnemesScript target, int attack)
    {
        
        //Debug.Log("A");
        Currenttarget = target;
        atk = attack;
        this.gameObject.SetActive(true);
    }
    public void SlowEffBullet(EnemesScript target,float duration , float Percentage) 
    {
        Currenttarget = target;
        this.gameObject.SetActive(true);
        StartCoroutine(Currenttarget.SlowEffect(Percentage, duration));
    }
    void Deactivate() 
    {
       // Debug.Log("B");
        this.gameObject.transform.position = ObjectPool.instance.offScreen.position;
        this.gameObject.SetActive(false);
    }
    void TargetHit() 
    {
        if (!Currenttarget.gameObject.activeSelf) 
        {
            Deactivate();
            
        }
        if (!Currenttarget.CurrentlyImpact()) 
        {
            Currenttarget.impacts = true;
            hitimpacts();
        }
        if (Currenttarget.IsDead()) 
        {
            this.gameObject.SetActive(false);
            return;
        }
        Currenttarget.TakeDamage(atk,tower);
    }
    void hitimpacts()
    {
       
        impacts = ObjectPool.instance.PullHitImpacts(hitImpact.id);

        Vector3 pos = Currenttarget.HitPoint.transform.position;
       // pos.y += (Currenttarget.transform.localScale.y / 2);
        impacts.transform.position = pos;
        impacts.SetActive(true);
        impacts.transform.parent = Currenttarget.HitPoint.transform;

    }
    private void OnDisable()
    {
    
    }
}
