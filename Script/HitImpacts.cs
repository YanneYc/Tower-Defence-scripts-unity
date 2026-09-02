using System.Collections;
using UnityEngine;



    public class HitImpacts : MonoBehaviour
    {
        public float delay;
        public int id;

        private void OnEnable()
        {
            StartCoroutine(selfDeactive());
        }
        IEnumerator selfDeactive() 
        {
            yield return new WaitForSeconds(delay);
            this.transform.position = ObjectPool.instance.offScreen.transform.position;
            this.gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            
        }

    }
