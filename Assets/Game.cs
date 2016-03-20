using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public static Game game;
    public HitEffect hitEffectPrefab;

    public static void Slow()
    {
        
    }

    void Awake()
    {
        game = this;
    }
    
    
    public static void ShowHit( Vector3 position )
    {
        HitEffect newEffect = Instantiate<HitEffect>(game.hitEffectPrefab);
        newEffect.transform.position = position;
    }
}
