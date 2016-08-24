using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement2D : MonoBehaviour {

    public bool hasStamina { get { return currentStamina > 0; } }
    public bool hasEnoughStamina { get { return (int)currentStamina >= 1; } }
    public int stamina = 3;
    public float currentStamina = 3;

    public bool canMove = true;
    public float jumpForce = 100;
    public bool grounded { get { return groundedLeft || groundedRight; } }
    public float speed = 1;

    public float GetRightValue { get { return transform.localScale.x; } }
    public Vector3 GetRightVector { get { return new Vector3(transform.localScale.x, 0, 0); } }


    bool groundedLeft, groundedRight;
    public GameObject leftGroundCheck, rightGroundCheck;

    public int airJumps = 0;

    public bool facingRight = true;

    private Animator anim;
    private GameObject jumpEffect;

    void Start() {
        jumpEffect = Resources.Load<GameObject>("Animations/SmokeJump/Smoke_JumpEffect");
        if (GetComponent<RandomizedClass>())
            speed = GetComponent<RandomizedClass>().playerStats.movementSpeed;

        if (GetComponent<Animator>())
            anim = GetComponent<Animator>();

    }

    public void OnLevelWasLoaded(int level) {
        if (level != 0)
            Camera.main.GetComponent<CameraMovement2D>().player = gameObject;
    }

    public bool canMoveX {
        get {
            Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 groundPos = new Vector2(playerPos.x + (0.32f * transform.localScale.x), playerPos.y);
            return !Physics2D.Linecast(playerPos, groundPos, 1 << LayerMask.NameToLayer("Ground")) && canMove;
        }
    }


    private IEnumerator DisableFloorCollision(Collider2D hit) {
        hit.enabled = false;
        yield return new WaitForSeconds(0.5f);
        hit.enabled = true;
    }

    void Update() {
        Interact();
        UpdateStamina();
    }


    void UpdateStamina() {
        if (currentStamina < stamina)
            currentStamina = Mathf.Clamp(currentStamina + Time.deltaTime, 0, stamina);
    }

    void Interact() {
        RaycastHit2D hit;
        if (Input.GetButtonDown("Interact")) {
            hit = Physics2D.Linecast(transform.position, transform.position + Vector3.down * 0.5f, 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("WorldObject"));
            if (hit.collider != null)
                switch (hit.collider.tag) {
                    case "Chest":
                        hit.collider.GetComponent<Chest>().DropItems();
                        break;
                    case "Portal":
                        if (hit.collider.GetComponent<Portal>().open) {
                            hit.collider.GetComponent<Portal>().UsePortal();
                            transform.position = Vector3.one;
                        }
                        break;

                }
        }


        if ((Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") == -1) || Input.GetAxisRaw("Vertical") == -1 && Input.GetJoystickNames().Length > 0) { // corridors, Press down
            hit = Physics2D.Linecast(transform.position, transform.position + Vector3.down * 0.5f, 1 << LayerMask.NameToLayer("Ground"));
            if (hit.collider != null) {
                if (hit.collider.CompareTag("Object")) {
                    StartCoroutine(DisableFloorCollision(hit.collider));
                }
            }
        }
    }


    void LateUpdate() {

        if (grounded) {
            airJumps = 0;
        }

        float xInput = Input.GetAxisRaw("Horizontal");

        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 groundPos = new Vector2(playerPos.x + (0.32f * transform.localScale.x), playerPos.y);

        if (canMoveX)
            transform.position += new Vector3((xInput * speed * Time.deltaTime), 0); // move player in x pos

        playerPos = new Vector2(leftGroundCheck.transform.position.x, leftGroundCheck.transform.position.y); // check the left foot
        groundPos = new Vector2(leftGroundCheck.transform.position.x, leftGroundCheck.transform.position.y - 0.16f);
        Debug.DrawLine(leftGroundCheck.transform.position, new Vector3(leftGroundCheck.transform.position.x, leftGroundCheck.transform.position.y - 0.16f, leftGroundCheck.transform.position.z));
        groundedLeft = Physics2D.Linecast(playerPos, groundPos, 1 << LayerMask.NameToLayer("Ground"));

        playerPos = new Vector2(rightGroundCheck.transform.position.x, rightGroundCheck.transform.position.y); // check the right foot
        groundPos = new Vector2(rightGroundCheck.transform.position.x, rightGroundCheck.transform.position.y - 0.16f);
        Debug.DrawLine(rightGroundCheck.transform.position, new Vector3(rightGroundCheck.transform.position.x, rightGroundCheck.transform.position.y - 0.16f, rightGroundCheck.transform.position.z));
        groundedRight = Physics2D.Linecast(playerPos, groundPos, 1 << LayerMask.NameToLayer("Ground"));

        if (xInput > 0 && !facingRight)
            Flip();
        else if (xInput < 0 && facingRight)
            Flip();

        if (Input.GetButtonDown("Jump")) {
            if (grounded) {
                Jump(1, true);
            } else if (hasEnoughStamina) {
                Jump(1, true);
                currentStamina = Mathf.Clamp(currentStamina - 1, 0, stamina);
            }

        }

        if (anim) {
            if (canMoveX)
                anim.SetFloat("speed", xInput);
            else
                anim.SetFloat("speed", 0);

            anim.SetBool("grounded", grounded);
            anim.SetFloat("fallSpeed", GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    public void Jump(float force, bool resetVelocity) {
        if (canMove) {
            if (resetVelocity)
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce * force), ForceMode2D.Impulse);
            groundedLeft = false;
            groundedRight = false;
            AudioSource.PlayClipAtPoint(Resources.Load("Sound/Effects/Jump_8bit") as AudioClip, transform.position);
        }
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D coll) // If the player collides with the ground
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Ground") && coll.relativeVelocity.magnitude > jumpForce) {
            Instantiate(jumpEffect, transform.position + Vector3.up * 0.08f, Quaternion.identity);
            Camera.main.GetComponent<CameraMovement2D>().ShakeCamera(0.1f, 1);
            AudioSource.PlayClipAtPoint(Resources.Load("Sound/Effects/ImpactSound01") as AudioClip, transform.position);
        }
    }



}
