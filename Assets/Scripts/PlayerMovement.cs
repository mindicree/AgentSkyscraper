using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Transform t;
    public TextMesh displayHP, displayAmmo;
    public Camera mainCamera;
    public ParticleSystem gunFlash;
    public AudioSource gunShot, outOfAmmo, healthPickup, enemyCollisionSFX, jumpSFX, dashSFX;
    public GameObject hitFlash, gunFlashScreen;

    public int lane, hp, ammo;
    public bool canHop, canJump;
    public float jumpForce = 5.0f;
    public int startingVelocity;
    public bool hasWon, hasLost;
    
    // Start is called before the first frame update
    void Start()
    {
        lane = 0;
        canHop = true;
        canJump = true;
        hasLost = false;
        hasWon = false;
        hp = 3;
        ammo = 12;
        hitFlash.SetActive(false);
        gunFlashScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //lane hopping mechanic
        if ((Input.GetKeyDown("a") || Input.GetKeyDown("left")) && lane != -1 && canHop) {
            lane--;
            canHop = false;
            dashSFX.Play();
            t.Translate(-6, 0, 0);
            Debug.Log("Lane: " + lane);
        }
        if ((Input.GetKeyDown("d") || Input.GetKeyDown("right")) && lane != 1 && canHop) {
            lane++;
            canHop = false;
            dashSFX.Play();
            t.Translate(6, 0, 0);  
            Debug.Log("Lane: " + lane); 
        }

        //jumping mechanic
        if ((Input.GetKeyDown("space") || Input.GetKeyDown("up")) && canJump) {
            canHop = false;
            canJump = false;
            jumpSFX.time = 0.3f;
            jumpSFX.Play();
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            Debug.Log("Jump!"); 
        }

        //lose
        if (hasLost || (t.position.y < 0 && rb.velocity.z < 10) || t.position.y < -5) {
            Debug.Log("LOSER!!!");
            SceneManager.LoadScene("LoseScreen");
        }
        if (hasWon) {
            Debug.Log("LOSER!!!");
            SceneManager.LoadScene("WinScreen");
        }

        //shooting
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("p")) {
            Debug.Log("Shoot: Firing...");
            if (ammo <= 0) {
                Debug.Log("Shoot: No ammo");
                outOfAmmo.time = 0.9f;
                outOfAmmo.Play();
            } else {
                Shoot();
            }
        }
        
    }

    void Shoot() {
        ammo--;
        gunShot.time = 0.1f;
        gunShot.Play();
        RaycastHit hit;
        StartCoroutine(ShowGunFlash());
        gunFlash.Play();
        //Debug.DrawLine(t.transform.position, mainCamera.transform.position + mainCamera.transform.forward + new Vector3(0, 0, 1000), Color.red, 3);
        if (Physics.Raycast(t.transform.position, mainCamera.transform.forward, out hit, 200f)) 
        {
            Debug.Log("Shoot: " + hit.transform.name);
            EnemyGround enemyGround = hit.transform.GetComponent<EnemyGround>();
            if (enemyGround != null) {
                enemyGround.TakeDamage(1);
            }
        }
        else
        {
            Debug.Log("Shoot: Missed");
        }
        UpdateAmmo();
    }

    void FixedUpdate() {
        //for resetting hopping after jumping with slight delay
        if (canJump) {
            canHop = true;
        }   
        
        //tells current velocity
        Debug.Log(rb.velocity);

        //starts velocity after touching ground initially
        //also sets velocity appropriately if studders from edge hit
        if (rb.velocity.z < 75 && rb.position.y < 0.5) {
            rb.velocity = rb.velocity = startingVelocity * new Vector3(0f, 0f, 1f);
        }

        if (lane == 0 && rb.position.z >= 10700 && rb) {
            hasWon = true;
        }
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log("Collision Triggered");

        //if collides with ground, reset hop and jump
        //unless below kill zone, in which case, lose life
        if (other.gameObject.tag == "Ground") {
            if (rb.position.y < -1.5) {
                //TODO: lose life here
                hasLost = true;
            }
            Debug.Log("Hit the ground. Current Velocity: " + startingVelocity);
            canJump = true;
            canHop = true;
        }

        //if collides with GroundSpeed
        if (other.gameObject.tag == "GroundSpeedUp") {
            
            canJump = true;
            canHop = true;

            startingVelocity = startingVelocity + (int)(startingVelocity*0.25);
            rb.velocity = startingVelocity * new Vector3(0f, 0f, 1f); 
            Debug.Log("SpeedUp!!! New Velocity: " + startingVelocity);  
        }

        //if collides with victory
        //finish level
        if (other.gameObject.tag == "Finish") {
            hasWon = true;
        }
    }

    void OnTriggerEnter(Collider other) {
        //if collides with enemy
        //take damage
        //if hp is 0, end game
        if (other.gameObject.tag == "EnemyGround00") {
            hp--;
            Debug.Log("EnemyHit. HP=" + hp);
            if (hp <= 0) {
                hasLost = true;
            } else {
                enemyCollisionSFX.time = 0.15f;
                enemyCollisionSFX.Play();
                StartCoroutine(ShowHitFlash());
            }
            UpdateHP();
        }

        if (other.gameObject.tag == "HPReload") {
            hp = 3;
            UpdateHP();
            healthPickup.Play();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "GunReload") {
            ammo+=6;
            outOfAmmo.time = 0.6f;
            outOfAmmo.Play();
            UpdateAmmo();
            Destroy(other.gameObject);
        }
    }

    void UpdateHP() {
        displayHP.text = "HP: " + hp;
    }

    void UpdateAmmo() {
        displayAmmo.text = "Ammo: " + ammo;
    }

    IEnumerator ShowHitFlash () {
        hitFlash.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        hitFlash.SetActive(false);
    }

    IEnumerator ShowGunFlash () {
        gunFlashScreen.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        gunFlashScreen.SetActive(false);
    }

}
