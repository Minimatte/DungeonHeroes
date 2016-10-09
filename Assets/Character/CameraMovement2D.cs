using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraMovement2D : MonoBehaviour {

    public GameObject player; //The offset of the camera to centrate the player in the X axis 
    public float offsetX = 0; //The offset of the camera to centrate the player in the Z axis 
    public float offsetY = 0;
    //The maximum distance permited to the camera to be far from the player, its used to make a smooth movement 
    public float maximumDistance = 2; //The velocity of your player, used to determine que speed of the camera 
    public float playerVelocity = 10;

    private float movementX;
    private float movementY;

    public float shake = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.1f;
    public float decreaseFactor = 1.0f;

    private static CameraMovement2D instance;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

    }


    void Start() {
        transform.position = new Vector3(GameEvents.player.transform.position.x, GameEvents.player.transform.position.y, transform.position.z);

        // player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame 
    void Update() {
        if (player == null) {
            if (GameEvents.player == null)
                return;
            else {
                player = GameEvents.player;
            }
        }

        if (player) {
            movementX = ((player.transform.position.x + offsetX - this.transform.position.x)) / maximumDistance;
            movementY = ((player.transform.position.y + offsetY - this.transform.position.y)) / maximumDistance;
            this.transform.position += new Vector3((movementX * playerVelocity * Time.deltaTime), (movementY * playerVelocity * Time.deltaTime), 0);
        }

        if (GetComponent<VignetteAndChromaticAberration>().chromaticAberration > 0)
            GetComponent<VignetteAndChromaticAberration>().chromaticAberration -= Time.deltaTime * 80;
        else
            GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 0;

        if (shake > 0) {
            transform.localPosition = transform.localPosition + new Vector3(Random.insideUnitSphere.x * shakeAmount, Random.insideUnitSphere.y * shakeAmount, 0);
            shake -= Time.deltaTime * decreaseFactor;
            GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 3;
        } else {
            shake = 0;
        }
    }

    public void ShakeCamera(float amount, float time) {
        decreaseFactor = time;
        shake = amount;
        
    }




}
