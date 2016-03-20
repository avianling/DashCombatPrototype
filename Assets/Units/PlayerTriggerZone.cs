using UnityEngine;
using System.Collections;

public class PlayerTriggerZone : MonoBehaviour {

    public bool PlayerInZone { get; private set; }

    void Update()
    {
        Debug.Log(PlayerInZone);
    }

    void OnTriggerEnter2D(Collider2D other )
    {
        Player player = other.GetComponent<Player>();
        if ( player != null )
        {
            PlayerInZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null )
        {
            PlayerInZone = false;
        }
    }

}
