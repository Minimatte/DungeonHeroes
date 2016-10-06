using UnityEngine;
using System.Collections;

public class BeholderHealth : Health {

    public GameObject[] loot;
    public GameObject explosionEffect;
    public AudioClip explosionSound;

    public bool hasShield { get { return shields > 0; } }
    public int shields = 4;

    public override void TakeDamage(float damageToTake) {
        if (!hasShield) {
            if (transform.childCount > 0)
                foreach (Transform child in transform)
                    Destroy(child.gameObject);
           
                base.TakeDamage(damageToTake);
        }
    }

    void Update() {
        if (!hasShield)
            hasDamageFrames = true;
        else
            hasDamageFrames = false;
    }


    private IEnumerator DropLoot(int amount) {
        int dropped = 0;
        Vector2 location = transform.position;
        float locationOffset = 0.5f;

        while (dropped < amount) {
        Camera.main.GetComponent<CameraMovement2D>().ShakeCamera(0.1f, 0.1f);
            dropped++;
            Instantiate(loot[Random.Range(0, loot.Length)], transform.position, Quaternion.identity);

            Vector2 effectLocation = location + new Vector2(Random.Range(-locationOffset, locationOffset), Random.Range(-locationOffset, locationOffset));
            Destroy(Instantiate(explosionEffect, effectLocation, Quaternion.identity), 2f);
            AudioSource.PlayClipAtPoint(explosionSound, effectLocation);
            yield return new WaitForSeconds(0.25f);
            
        }

        Destroy(gameObject);
    }

    public override void Kill() {
        Boss b = GetComponent<Boss>();
        BossRoom.OnBossDeath.Invoke();
        b.active = false;
        
        StartCoroutine(DropLoot(25));
    }

}

