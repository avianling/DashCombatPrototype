using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random; 

public class HitChainCounter : MonoBehaviour {
    
    public Text textField;
    private Player player;

    private Vector3 homePosition;

    void Start()
    {
        player = Player.player;
        textField = GetComponent<Text>();
        homePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        int chainCount = Player.player.chainCount;
        textField.text = chainCount.ToString();
        
        if (chainCount >= 50)
        {
            int switchKey = Random.Range(0, 3);
            switch(switchKey)
            {
                case 0 : textField.color = Color.red; break;
                case 1: textField.color = Color.yellow; break;
                case 2: textField.color = Color.white; break;
            }
        }
        else if (chainCount >= 20)
        {
            textField.color = Color.red;            
        }
        else if(chainCount >= 10)
        {   
            textField.color = Color.yellow;
        }
        else
        {
            textField.color = Color.white;
        }

        UpdateShudder(chainCount - 9);
    }

    private void UpdateShudder(float amount)
    {
        float factor = 0.01f;
        amount = Mathf.Clamp(amount, 0f, 15f);

        transform.position = homePosition + new Vector3(
            Random.Range(-amount * factor, amount * factor),
            Random.Range(-amount * factor, amount * factor),
            0);


    }
}

