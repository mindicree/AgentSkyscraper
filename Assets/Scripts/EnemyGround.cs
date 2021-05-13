using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround : MonoBehaviour
{
    // Start is called before the first frame update
    public float hp = 2;
    public GameObject ammoDrop, hpDrop;
    public Transform transform;
    public AudioSource deathSound;
    public LevelRNG rng;

    public void TakeDamage(int damage) {
        hp -= damage;
        if (hp <= 0) {
            double dropChance = rng.GetDropChance();
            Debug.Log("Drop: Destroy Object. Drop Chance: " + dropChance);
            if (dropChance < 0.4) {
                Debug.Log("Drop: Dropping Ammo");
                Instantiate(ammoDrop, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), new Quaternion());
            } else if (dropChance < 0.5) {
                Debug.Log("Drop: Dropping Health");
                Instantiate(hpDrop, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion());
            } else {
                Debug.Log("Drop: No Drop");
            }
            deathSound.Play();
            Destroy(gameObject);
        }
    }
}
