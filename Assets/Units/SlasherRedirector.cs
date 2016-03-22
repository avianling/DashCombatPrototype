using UnityEngine;
using System.Collections;

public class SlasherRedirector : MonoBehaviour {
    public Slasher slasher;
    public float distance;

    public void DashForward()
    {
        slasher.transform.position += transform.up * distance * Time.deltaTime;
    }

    public void AttackComplete()
    {
        slasher.OnAttackComplete();
    }

}
