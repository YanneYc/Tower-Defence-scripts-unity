using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class TowerBase : MonoBehaviour , ISaveble
{
    Node Location;
   protected float NextFire;
   protected List<EnemesScript> enemesScripts = new List<EnemesScript>();
    public int NumberOfTargets;
    public bool CanRoatate;
    public int id;
    public float Range;
    public int Attack;
    public int Cost;
    public float FireRate;
    public TowerBase Next;
    public GameObject VisualRange;
    public Projectile projectile;
    public EnemesScript[] targets;
    public GameObject Pivot;
    public Transform FirePoint;
    public SphereCollider TriggerZone;
    GameObject currentarget;
    public enum SetTarget { First, Last, Strongest, Weakest };
    public SetTarget CurrentLock;
    
    public void RemovingSelf(EnemesScript e)
    {
        enemesScripts.Remove(e);
    }
    public void SetLocation(Node n) 
    {
        this.Location = n;
    }
    public void Sell() 
    {
        gameObject.SetActive(false);
        Location.walkalbe = true;
        Location.NodeSelect(true);
    }
    public void TowerSelected() 
    {
        VisualRange.SetActive(true);
    }
    public void TowerDeSelected() 
    {
        VisualRange.SetActive(false);
    }
    public void Upgrade() 
    {
        if(Next == null) 
        {
            return;
        }
        BuildManager.instance.BuildTower(Next.id, Location);
        this.gameObject.SetActive(false);

    }
    void Awake() 
    {
        SavingSystems.savingList.Add(this);
    }
    private void Start()
    {
        targets = new EnemesScript[NumberOfTargets];
    }
    private void OnEnable()
    {
        NextFire = 0;
        TriggerZone.radius = Range;
        Vector3 size = new Vector3(Range * 2, Range * 2, Range * 2);
        VisualRange.transform.localScale = size;
    }
    private void Update()
    {
        if (enemesScripts.Count > 0)
        {
            //LockingTarget();
            AttackEnemes();
        }
    }
    public virtual void AttackEnemes()
    {
        
        if (CanRoatate)
        {
            currentarget = enemesScripts[0].gameObject;
            if(currentarget != null) 
            {
                Vector3 ToLook = currentarget.transform.position;
               ToLook.y = Pivot.transform.position.y;
                Pivot.transform.LookAt(ToLook);
            }
           
        }
        if(Time.time > NextFire) 
        {
            int current = 1; 
            foreach(var e in enemesScripts.ToList()) 
            {
                if(current > NumberOfTargets) 
                {
                    break;
                }
                if (e.IsDead()) 
                {
                        RemovingSelf(e);
                    continue;
                }
                Projectile bullet = ObjectPool.instance.PullBullets(projectile.id).GetComponent<Projectile>();
                bullet.transform.position = FirePoint.position;
                bullet.SetTarget(e, Attack);
                Debug.Log($"Attacking {e.name} current{ current} Count{enemesScripts.Count} ");
                current++;
            }
            NextFire = Time.time + FireRate;
        }
        
    }
    public void LockingTarget()
    {
       // Debug.Log("lock");

        switch (CurrentLock)
        {
            case SetTarget.First:
                for (int i = 0; i < targets.Length; i++)
                {
                    if (i >= enemesScripts.Count)
                    {
                        break;
                    }
                    if (targets[i] != null)
                    {
                        continue;
                    }
                    targets[i] = enemesScripts[i];
                }

                break;

            case SetTarget.Last:
                for (int i = targets.Length - 1; i > 0; i--)
                {
                    if (i >= enemesScripts.Count)
                    {
                        break;
                    }
                    if (targets[i] != null)
                    {
                        continue;
                    }
                    targets[i] = enemesScripts[i];
                }

                break;

            case SetTarget.Strongest:

                break;

            case SetTarget.Weakest:

                break;

            default:
                break;
        }
    }

   protected bool CheckIfEnemies(GameObject go)
    {
        return go.GetComponent<EnemesScript>();
    }
   protected EnemesScript GetEnemies(GameObject go)
    {
        return go.GetComponent<EnemesScript>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (CheckIfEnemies(other.gameObject))
        {
            
            enemesScripts.Add(GetEnemies(other.gameObject));
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (CheckIfEnemies(other.gameObject))
        {
            enemesScripts.Remove(GetEnemies(other.gameObject));
        }
    }

    public void Save(ref TestData data)
    {
        if (!gameObject.activeSelf) 
        {
            return;
        }
        if(Location == null) 
        {
            Debug.Log("Location null");
        }
        if (data.towerLocations.ContainsKey(this.Location.selfPosition)) 
        {
            return;
        }
        data.towerLocations.Add(Location.selfPosition, id);
    }
}
