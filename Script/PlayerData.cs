using System.Collections;
using UnityEngine;


    public class PlayerData : MonoBehaviour
    {
    public static PlayerData instance;
    public int Coin;
    public int live;



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
    public void AddCoin(int value) 
    {
        Coin += value;
    }
    public void TakeLife() 
    {
        live -= 1;
    }

}
