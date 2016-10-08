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
        if (!active)
            return;

        if (health.hasShield) {
            var rand = Random.Range(0, 3);
            switch (rand) {
                case 0:
                    StartCoroutine(FocusAttack());
                    break;
                case 1:
                    StartCoroutine(ShotgunAttack());
                    break;
                case 2:
                    StartCoroutine(Wave());
                    break;
            }
        } else {
            StartCoroutine(SpinAttack());

        }
    }

    private IEnumerator FocusAttack() {
        for (int i = 0; i < 3; i++) {
            currentCooldown = 0.6f;

            var direction = (target.position - transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, angle));
            yield return new WaitForSeconds(0.15f);
        }

    }

    private IEnumerator SpinAttack() {
        for (int i = 0; i < 365; i += 15) {
            currentCooldown = 0.4f;
            //currentCooldown = 1.2166666f;
            //currentCooldown = attackProperties.cooldown;
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i + 180));
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator ShotgunAttack() {
        for (int i = 90; i > 0; i -= 15) {
            //currentCooldown = 0.3f;
            currentCooldown = 0.5f;
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i + 90));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i + 180));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i + 270));
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator Wave() {
        for (int x = 0; x < 1; x++) {
            var direction = (target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            for (int i = -45; i < 45; i += 10) {
                //currentCooldown = attackProperties.cooldown;
                direction = Quaternion.AngleAxis(i, Vector3.up) * direction;
                Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, i + angle));
            }

            yield return new WaitForSeconds(1);
        }
        currentCooldown = 0.2f;
    }
}
