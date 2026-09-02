using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;



    public class ObjectPool : MonoBehaviour
    {
    public static ObjectPool instance;
    public Transform offScreen;
    public List<GameObject> towers;
    public List<GameObject> enemes;
    public List<GameObject> bullets;
    public List<GameObject> hitImpacts;
    public List<List<GameObject>> towersPool = new List<List<GameObject>>();
    public List<List<GameObject>> enemesPool = new List<List<GameObject>>();
    public List<List<GameObject>> bulletPool = new List<List<GameObject>>();
    public List<List<GameObject>> hitsPool   = new List<List<GameObject>>();
    public int towerCount;
    public int enemeCount;
    public int bulletCount;
    public int hitCount;

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
    private void Start()
    {
        GenerateEnemes();
        GenerateBullets();
        GenerateTower();
        GenerateHitImpacts();
    }
    void GenerateBase(List<GameObject> objs , List<List<GameObject>> pools , int count) 
    {
        if(objs.Count < 1) 
        {
            Debug.Log("objs NULL");
            return;
        }
        foreach(GameObject go in objs) 
        {
            if(go == null) 
            {
                continue;
            }
            List<GameObject> holder = new List<GameObject>();
            for (int i = 0; i < count; i++) 
            {
                GameObject current = Instantiate(go, offScreen.position, Quaternion.identity);
                current.SetActive(false);
                holder.Add(current);
            }
            pools.Add(holder);
            Debug.Log(holder.Count);
        }
        
    }
    GameObject PullObejctBase(int id , List<List<GameObject>> target) 
    {
        //Debug.Log("Pulling");
        foreach(GameObject tar in target[id]) 
        {
            if(tar == null) 
            {
                continue;
            }
            if (!tar.activeSelf) 
            {
                return tar;
            }
        }
        GameObject go = null;
        for(int i = 0; i < 5; i++) 
        {
            go = Instantiate(target[id][0], offScreen.position, Quaternion.identity);
            go.SetActive(false);
            target[id].Add(go);
        }
         return go;
       
    }
    public GameObject PullHitImpacts(int id) 
    {
        return PullObejctBase(id, hitsPool);
    }
    public GameObject PullTower(int id) 
    {
        return PullObejctBase(id,towersPool);
    }
    public GameObject PullEnemes(int id) 
    {
        return PullObejctBase(id,enemesPool);
    }
    public GameObject PullBullets(int id) 
    {
        return PullObejctBase(id, bulletPool);
    } 
    public void GenerateTower() 
    {
        GenerateBase(towers, towersPool, towerCount);
    }
    public void GenerateEnemes() 
    {
        GenerateBase(enemes, enemesPool, enemeCount);
    }
    public void GenerateBullets() 
    {
        GenerateBase(bullets, bulletPool, bulletCount);
    }
    public void GenerateHitImpacts() 
    {
        GenerateBase(hitImpacts, hitsPool, hitCount);
    }
}
