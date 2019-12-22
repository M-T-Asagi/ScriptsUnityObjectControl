using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterWaitSecond : MonoBehaviour {

    [SerializeField]
    float wait = 5.0f;
	
    // Use this for initialization
	void Start () {
        if (wait >= 0)
            StartCoroutine(DestroySelf());
            
	}
	
    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(wait);
        Destroy(gameObject);
    }
}
