using UnityEngine;
using System.Collections;

public class Bleeder : MonoBehaviour {

    private ParticleSystem system;

    void Awake()
    {
        system = GetComponent<ParticleSystem>();
        bleed = false;
    }

    public bool bleed { set
        {
            ParticleSystem.EmissionModule module = system.emission;
            module.enabled = value;
        }
    }
}
