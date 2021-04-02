using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageSceneCrewRandomMovement : MonoBehaviour
{
    private float newX;
    private float newZ;

    private float frameX;
    private float frameZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frameX = Random.Range(-0.05f, 0.05f);
        frameZ = Random.Range(-0.05f, 0.05f);

        newX = this.transform.position.x + frameX;
        newZ = this.transform.position.z + frameZ;

        this.transform.position = new Vector3(newX, this.transform.position.y, newZ);
    }
}
