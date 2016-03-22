using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BanditSpawner : MonoBehaviour {

    public GameObject prefab;
    public GameObject hightwayman;

    int waveIndex = 0;

    private CombatGroup combatGroup = new CombatGroup();

    [System.Serializable]
    public class Wave
    {
        public int bandits;
        public int highwaymen;
    }

    public List<Wave> waves = new List<Wave>();

    private List<GameObject> creatures = new List<GameObject>();

    public IEnumerator CreateWave()
    {
        Wave wave = waves[waveIndex];
        waveIndex = (waveIndex + 1) % (waves.Count - 1);
        //waves.RemoveAt(0);

        for ( int i=0; i < wave.bandits; i++ )
        {
            creatures.Add(Create(prefab));
            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 0; i < wave.highwaymen; i++)
        {
            creatures.Add(Create(hightwayman));
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    public GameObject Create(GameObject prefab)
    {
        GameObject obj = Instantiate<GameObject>(prefab);

        List<Rect> rects = new List<Rect>();

        Vector2 playerViewportPos = Camera.main.WorldToViewportPoint(Player.player.transform.position);

        if ( playerViewportPos.x < 0.5f )
        {
            // add the two points above 0.5
            rects.Add(new Rect(0.5f, 0f, 0.5f, 0.5f));
            rects.Add(new Rect(0.5f, 0.5f, 0.5f, 0.5f));
            if ( playerViewportPos.y < 0.5f )
            {
                rects.Add(new Rect(0f, 0.5f, 0.5f, 0.5f));
            } else
            {
                rects.Add(new Rect(0f, 0f, 0.5f, 0.5f));
            }
        } else
        {
            rects.Add(new Rect(0f, 0f, 0.5f, 0.5f));
            rects.Add(new Rect(0f, 0.5f, 0.5f, 0.5f));

            if (playerViewportPos.y < 0.5f)
            {
                rects.Add(new Rect(0.5f, 0.5f, 0.5f, 0.5f));
            }
            else
            {
                rects.Add(new Rect(0.5f, 0f, 0.5f, 0.5f));
            }
        }

        Rect r = rects[Random.Range(0, rects.Count)];
        float xPos = Mathf.Clamp(Random.Range(r.xMin, r.xMax), 0.1f, 0.9f);
        float yPos = Mathf.Clamp(Random.Range(r.yMin, r.yMax), 0.1f, 0.9f);

        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(xPos, yPos));
        pos.z = 0;
        obj.transform.position = pos;

        var enemy = obj.GetComponent<Enemy>();
        if (enemy != null) combatGroup.Add(enemy);

        return obj;
    }
	
	// Update is called once per frame
	void Update () {

        combatGroup.Update();

        if (waves.Count > 0)
        {
            creatures.RemoveAll(creature => creature == null);
            if (creatures.Count == 0)
            {
                StartCoroutine(CreateWave());
            }
        }
	}
}
