using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrapplingHook : DefensiveSpell {
    public LayerMask hitMask = 1 << 0;
    public float range = 5;

    public List<Vector2> corners;

    private PlayerMovement2D movement;
    private Rigidbody2D playerRigid;

    private bool hookedOn = false;

    private Mana playerMana;

    protected override void Init() {
        playerMana = GetComponent<Mana>();
        manaCost = 2f;
        hitMask += 1 << 8;
        playerRigid = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement2D>();
        corners = new List<Vector2>();
        spellName = "GrapplingHook";
    }


    void BreakHook() {
        hookedOn = false;
        movement.canMove = true;
        Destroy(GetComponent<LineRenderer>());
        corners.Clear();
    }

    protected override void ActivateSpell() {

        if (hookedOn) {
            BreakHook();
        } else {

            RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position + (Vector3.up * range) + (Vector3.right * range * movement.GetRightValue), hitMask);

            if (hit) {
                corners.Add(hit.point);

                movement.canMove = false;

                hookedOn = true;
                /*
                                LineRenderer lr = gameObject.AddComponent<LineRenderer>();

                                lr.SetPosition(0, (Vector2)corners.Peek());
                                lr.SetPosition(1, transform.position);

                                lr.SetWidth(0.05f, 0.05f);
                                Material newMat = new Material(Shader.Find("Sprites/Default"));
                                lr.material = newMat;
                                */
            }
        }
    }

    void Update() {
        if (hookedOn) {
            RaycastHit2D hit;


            playerMana.useMana(10 * Time.deltaTime);
            if (!playerMana.HasMana(10 * Time.deltaTime))
                BreakHook();

            if (Input.GetButtonDown("Jump")) {
                BreakHook();
                GetComponent<PlayerMovement2D>().Jump(1, false);
            }

            Debug.DrawLine(transform.position, corners[corners.Count - 1]);
            hit = Physics2D.Linecast(transform.position, corners[corners.Count - 1], hitMask);

            Vector2 currentHookPoint = corners[corners.Count - 1];
            if (corners.Count > 1) {

                RaycastHit2D checkForLast = Physics2D.Linecast(transform.position, corners[corners.Count - 2], hitMask);
                RaycastHit2D checkForCurrent = Physics2D.Linecast(transform.position, corners[corners.Count - 1], hitMask);

                Debug.DrawLine(transform.position, corners[corners.Count - 2], Color.red);
                Debug.DrawLine(transform.position, corners[corners.Count - 1], Color.blue);


                if (checkForLast.point == corners[corners.Count - 2] && (checkForCurrent.point == corners[corners.Count - 1]))
                    corners.Remove(currentHookPoint);


            }



            if (currentHookPoint != hit.point && hit.point != Vector2.zero) {
                if (!corners.Contains(hit.point))
                    corners.Add(hit.point);
            }

            /* LineRenderer lr = GetComponent<LineRenderer>();

             lr.SetPosition(corners.Count, transform.position);
             */
            Vector2 directionToGrapple = (currentHookPoint - new Vector2(transform.position.x, transform.position.y)).normalized; // get direction to the grappling point

            float speedTowardsAnchor = Vector3.Dot(playerRigid.velocity, directionToGrapple); // dot product to get the speed

            playerRigid.velocity = playerRigid.velocity - (speedTowardsAnchor * directionToGrapple); // then we set the speed
            playerRigid.AddForce(directionToGrapple * (Physics2D.gravity.magnitude * 1.65f)); // we need to add gravity so that we dont fall down, its towards the grappling point

        }
    }


    void OnDrawGizmos() {
        foreach (Vector2 v in corners) {

            Gizmos.DrawCube(v, new Vector2(0.25f, 0.25f));
        }
    }

}
