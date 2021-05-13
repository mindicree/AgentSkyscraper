using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlock : MonoBehaviour
{
    public Transform enemyModel;
    public int enemyCount;
    public Rigidbody ground;
    public LevelRNG rng;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemyCount; i++) {
            //x will either be -6, 0, or 6
            //y is always 0.5
            //z is between the bounds of the level
            float xPos = rng.GetXPos();
            float yPos = 0.5f;
            float zPos = rng.GetZPos(ground.position);

            Debug.Log("Position " + i + ": " + xPos + ", 0.5, " + zPos);
            Instantiate(enemyModel, new Vector3(xPos, yPos, zPos), new Quaternion());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
