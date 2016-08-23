using UnityEngine;
using System.Collections;

public class Beholder : Boss {

    public GameObject projectilePrefab;
    void Start() {
        flips = false;
    }

    protected override void Movement() {
        transform.position = new Vector2(transform.position.x + Mathf.Sin(Time.timeSinceLevelLoad) * Time.deltaTime, transform.position.y + Mathf.Cos(Time.timeSinceLevelLoad) * Time.deltaTime);
    }

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
        for (int x = 0; x < 5; x++) {
            var direction = (target.transform.position - transform.position).normalized;
            for (int i = 225; i < 315; i += 10) {
                currentCooldown = attackProperties.cooldown;
                Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i + (direction.x * Mathf.Rad2Deg)));
            }
            yield return new WaitForSeconds(1);
        }
    }
}
