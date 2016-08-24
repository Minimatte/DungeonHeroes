using UnityEngine;
using System.Collections;

public class Crystals : Health {

    public GameObject summonThis;
    public Transform summonLocation;

    private GameObject summonedInstance;

    public GameObject introductoinUI;

    public override void Kill() {
        StartCoroutine(SummonMonster());
    }
    public override void takeDamage(float damageToTake) {

        if (currentHealth <= 0)
            return;

        if (damageToTake > 0.01f)
            damageToTake = 1;
        base.takeDamage(damageToTake);
    }

    private IEnumerator SummonMonster() {
        Camera.main.GetComponent<CameraMovement2D>().ShakeCamera(1f, 0.3f);
        GameEvents.player.GetComponent<PlayerMovement2D>().canMove = false;
        yield return new WaitForSeconds(3);
        Camera.main.GetComponent<CameraMovement2D>().ShakeCamera(1f, 1f);

        Camera.main.GetComponent<CameraMovement2D>().player = summonLocation.gameObject;


        yield return new WaitForSeconds(1);
        Camera.main.GetComponent<CameraMovement2D>().ShakeCamera(1f, 1);

        summonedInstance = (GameObject)Instantiate(summonThis, summonLocation.position, Quaternion.identity);
        introductoinUI.SetActive(true);
        yield return new WaitForSeconds(3);
        Camera.main.GetComponent<CameraMovement2D>().player = GameEvents.player;
        summonedInstance.GetComponent<Boss>().active = true;
        GameEvents.player.GetComponent<PlayerMovement2D>().canMove = true;
        introductoinUI.SetActive(false);


        Destroy(gameObject);



    }
}
