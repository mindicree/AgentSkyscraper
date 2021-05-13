using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRNG : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    System.Random rng = new System.Random((int)(System.DateTime.Now.Ticks%100000));

    public float GetXPos() {
        float xPos = rng.Next(-1, 2) * 6;
        return xPos;
    }

    public float GetZPos(Vector3 position) {
        float zPos = position.z + rng.Next(-50, 100);
        return zPos;
    }

    public double GetDropChance() {
        return rng.NextDouble();
    }
}
