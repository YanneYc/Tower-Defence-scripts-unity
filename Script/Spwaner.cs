using System.Collections;
using UnityEngine;



public class Spwaner : MonoBehaviour
{
    public WaveInfo[] waveinfo;
    public static Spwaner instance;
    int currentWave;


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
    IEnumerator SpwanAll()
    {
        foreach (var info in waveinfo)
        {
            GameObject go = ObjectPool.instance.PullEnemes(info.prefab.id);
            go.GetComponent<EnemesScript>().speed = info.Speed;
            go.GetComponent<EnemesScript>().startHealth = info.Health;
            go.SetActive(true);
            yield return new WaitForSeconds(info.Rate);
        }
    }
    public void SpwanWave()
    {
         StartCoroutine(Spwanning(currentWave));
        currentWave++;
        if (currentWave >= waveinfo.Length)
        {
            currentWave = 0;
        }
        //StartCoroutine(SpwanAll());
    }
    IEnumerator Spwanning(int current)
    {
        for (int i = 0; i < waveinfo[current].Count; i++)
        {
            GameObject go = ObjectPool.instance.PullEnemes(waveinfo[current].prefab.id);
            go.GetComponent<EnemesScript>().speed = waveinfo[current].Speed;
            go.GetComponent<EnemesScript>().startHealth = waveinfo[current].Health;
            go.SetActive(true);

            yield return new WaitForSeconds(waveinfo[currentWave].Rate);
        }


    }
}
