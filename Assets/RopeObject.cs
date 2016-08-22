using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopeObject : MonoBehaviour {

    List<GameObject> rope;

    public void Initialize(int length) {
        rope = new List<GameObject>();
        for (int i = 0; i < length; i++) {
            GameObject go = Instantiate(gameObject, transform.position + Vector3.down * i * 0.3f, Quaternion.identity) as GameObject;
            rope.Add(go);
        }
        rope[0].GetComponent<Rigidbody2D>().isKinematic = true;
        for (int i = 0; i < length - 1; i++) {
            rope[i].GetComponent<DistanceJoint2D>().enabled = true;
            Physics2D.IgnoreCollision(rope[i].GetComponent<Collider2D>(), rope[i + 1].GetComponent<Collider2D>());
            rope[i].GetComponent<DistanceJoint2D>().connectedBody = rope[i + 1].GetComponent<Rigidbody2D>();
            rope[i + 1].transform.SetParent(rope[0].transform, true);
        }
    }
}
