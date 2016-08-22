using UnityEngine;
using System.Collections;

public class Beholder : Boss {

    public GameObject projectilePrefab;


    protected override void Attack() {
        var rand = Random.Range(0, 3);
        switch (rand) {
            case 0:
                StartCoroutine(SpinAttack());
                break;
            case 1:
                StartCoroutine(ShotgunAttack());
                break;
            case 2:
                StartCoroutine(Wave());
                break;
        }
    }

    private IEnumerator SpinAttack() {
        for (int i = 0; i < 940; i += 15) {
            currentCooldown = attackProperties.cooldown;
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i));
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator ShotgunAttack() {
        for (int i = 360; i > 0; i -= 15) {
            currentCooldown = attackProperties.cooldown;
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i + 90));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i + 180));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i + 270));
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator Wave() {
        for (int x = 0; x < 10; x++) {
            for (int i = 180; i < 360; i += 10) {
                currentCooldown = attackProperties.cooldown;
                Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i));
            }
            yield return new WaitForSeconds(1);
        }
    }
}
