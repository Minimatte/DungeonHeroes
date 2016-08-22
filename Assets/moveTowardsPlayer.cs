using UnityEngine;
using System.Collections;

public class moveTowardsPlayer : MonoBehaviour {

    public GameObject target;
    public float speed;
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 1f * speed);
	}
}
