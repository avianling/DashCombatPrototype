using UnityEngine;
using System.Collections;

public class PlayerHealthBar : MonoBehaviour {

    public RectTransform rect;
    private Player player;

    void Start()
    {
        player = Player.player;
    }
    	
	// Update is called once per frame
	void Update () {
        float progress = player.entity.health / player.entity.maxHealth;

        Vector2 maxAnchor = rect.anchorMax;
        maxAnchor.x = progress;
        rect.anchorMax = maxAnchor;
	}
}
