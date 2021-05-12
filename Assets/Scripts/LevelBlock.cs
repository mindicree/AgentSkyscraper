using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlock : MonoBehaviour
{
    public Transform enemyModel;
    public int enemyCount;
    public Rigidbody ground;

    // Start is called before the first frame update
    void Start()
    {
        System.Random rng = new System.Random((int)(System.DateTime.Now.Ticks%1000));
        int zOffSet = 8200;
        for (int i = 0; i < enemyCount; i++) {
            //x will either be -6, 0, or 6
            //y is always 0.5
            //z is between the bounds of the level
            float xPos = rng.Next(-1, 2) * 6;
            float yPos = 0.5f;
            float zPos = ground.position.z + rng.Next(-50, 100);

            Debug.Log("Position " + i + ": " + xPos + ", 0.5, " + zPos);
            Instantiate(enemyModel, new Vector3(xPos, yPos, zPos), new Quaternion());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
