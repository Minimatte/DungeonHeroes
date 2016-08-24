using UnityEngine;
using System.Collections;

public class Beholder : Boss {

    public GameObject projectilePrefab;
    private BeholderHealth health;
    void Awake() {
        health = GetComponent<BeholderHealth>();
    }

    void Start() {
        flips = false;
    }

    protected override void Movement() {
        transform.position = new Vector2(transform.position.x + Mathf.Sin(Time.timeSinceLevelLoad) * Time.deltaTime, transform.position.y + Mathf.Cos(Time.timeSinceLevelLoad) * Time.deltaTime);
    }

    protected override void Attack() {
        if (health.hasShield) {
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
        } else {
            StartCoroutine(FocusAttack());
        }
    }

    private IEnumerator FocusAttack() {
        yield return new WaitForSeconds(0.05f);
        currentCooldown = 0.1f;

        var direction = (target.position - transform.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, angle));
        Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, angle - 5));
        Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, angle + 5));
    }

    private IEnumerator SpinAttack() {
        for (int i = 0; i < 940; i += 15) {
            currentCooldown = attackProperties.cooldown;
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i + 180));
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
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            for (int i = -45; i < 45; i += 10) {
                currentCooldown = attackProperties.cooldown;
                direction = Quaternion.AngleAxis(i, Vector3.up) * direction;
                Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i + angle));
            }

            yield return new WaitForSeconds(1);
        }
    }
}
