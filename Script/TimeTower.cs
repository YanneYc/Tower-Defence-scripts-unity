using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TimeTower : TowerBase
{
    public float duration;
    public float percentage;
    public override void AttackEnemes()
    {
        
        
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (base.CheckIfEnemies(other.gameObject))
        {

            enemesScripts.Add(base.GetEnemies(other.gameObject));
            base.GetEnemies(other.gameObject).Slow = true;
            base.GetEnemies(other.gameObject).speed *= percentage;

        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if (base.CheckIfEnemies(other.gameObject))
        {

            enemesScripts.Add(base.GetEnemies(other.gameObject));
            base.GetEnemies(other.gameObject).Slow = false ;
            base.GetEnemies(other.gameObject).speed = base.GetEnemies(other.gameObject).speedDefault;

        }
    }
}
