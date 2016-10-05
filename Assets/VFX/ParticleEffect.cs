using UnityEngine;
using System.Collections;

public class ParticleEffect : MonoBehaviour {

    public float lifeTime = 2;
	
	void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
