using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


public class Game : MonoBehaviour {

    public static Game game;
    public HitEffect hitEffectPrefab;
    public Text YouDied;
     
    public static void Slow()
    {
        
    }

    void Awake()
    {
        game = this;
        YouDied.gameObject.SetActive(false);
    }
    
    
    public static void ShowHit( Vector3 position )
    {
        HitEffect newEffect = Instantiate<HitEffect>(game.hitEffectPrefab);
        newEffect.transform.position = position;
    }

    internal void Restart()
    {
        YouDied.gameObject.SetActive(true);
        StartCoroutine(ReloadGame());
    }

    public IEnumerator ReloadGame()
    {
        yield return new WaitForSeconds(4f);
        YouDied.gameObject.SetActive(false);
        Application.LoadLevel(0);
    }

 }
