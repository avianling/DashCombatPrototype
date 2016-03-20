using UnityEngine;
using System.Collections;

public class ShooterWeapon : MonoBehaviour {

    public Entity parent;
    public GameObject shootPoint;
    public GameObject shootPrefab;

    public void Shoot()
    {
        parent.transform.position -= transform.up * 15f * Time.deltaTime;

        GameObject newBullet = Instantiate<GameObject>(shootPrefab);
        newBullet.transform.position = shootPoint.transform.position;

        Vector3 direction = transform.up;
        Quaternion rotator = Quaternion.AngleAxis(Random.Range(-10, 10), Vector3.forward);
        direction = rotator * direction;

        newBullet.transform.up = direction;
        newBullet.GetComponent<DamageVolume>().enemy = parent;
    }

}
