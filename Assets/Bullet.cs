using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed;

    public void OnTriggerEnter2D( Collider2D other )
    {
        if ( other.gameObject.layer == 0 || other.gameObject.layer == 9 )
        {
            Destroy(gameObject);
        }
    }
    
	// Update is called once per frame
	void Update () {
        transform.position += transform.up * speed * Time.deltaTime;
	}
}
