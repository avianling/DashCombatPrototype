using UnityEngine;
using System.Collections;

public class HitEffect : MonoBehaviour {

    public void Die()
    {
        Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
	}
	
}
