using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Coin : MonoBehaviour {

    public GameObject pickupVFX;

    public List<AudioClip> soundEffect;

    public int value = 0;

    void Awake() {
        value = Random.Range(1, 11);
        GetComponent<ConstantForce2D>().torque = Random.Range(-1, 2);
        StartCoroutine(EnableCollision());
    }

    private IEnumerator EnableCollision() {
        yield return new WaitForSeconds(0.5f);
        GetComponent<Collider2D>().enabled = true;
    }

    void Start() {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<PlayerMovement2D>()) {
            PlayerItems.gold += value;
            Instantiate(pickupVFX, transform.position, Quaternion.identity);

            if (soundEffect.Count > 0) {
                AudioSource.PlayClipAtPoint(soundEffect[Random.Range(0, soundEffect.Count)], transform.position);
            }

            Destroy(gameObject);
        }
    }
}
