using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This Script gives Line of Sight to any Actor and looks for the Player (or target)
public class LineOfSightTest : MonoBehaviour
{
    public Transform target;

    float rotationSpeed = 2.0f;
    float visDist = 20.0f;
    float visAngle = 30.0f;

    
    // Start is called before the first frame update
    void Start()
    {
       //Get Player Component
       // if you want to dynamically add Player ref
    }

    // Update is called once per frame
    void Update()
    {
        // direction  is the target( player) location in the world 'compared' to this object's own position in the world
        Vector3 direction = target.position - transform.position;
        // angle is the angle this object is from the target
        float angle = Vector3.Angle(direction, transform.forward);

        if(direction.magnitude < visDist && angle < visAngle)
        {
            direction.y = 0;

            //turns towards player
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

            

        }


    }
}
