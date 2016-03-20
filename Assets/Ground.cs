using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {
    
    void OnMouseUpAsButton()
    {
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        temp.z = 0;
        Player.player.MoveTo(temp);
    }

}
