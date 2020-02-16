using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private Transform player;

    float rotationSpeed = 7.0f;

    float speed = 5.0f;
    float visDis = 20.0f;
    float visAngle = 90.0f;
    private float shootdistance = 6f;
        private Animator Player;
    string state = "Idle";
    public bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>().transform;
        Player = GetComponent<Animator>();
    }
    void Alive()
    {

    }
    // Update is called once per frame
    void Update()
    {

            Vector3 Direction = player.position - this.transform.position;
            float angle = Vector3.Angle(Direction, transform.position);

            if (Direction.magnitude < visDis && angle < visAngle)
            {
                Direction.y = 0;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Direction), Time.deltaTime * rotationSpeed);


                if (Direction.magnitude > shootdistance)
                {
                    state = "Running";
                    Player.SetBool("Running", true);
                    Player.SetBool("Idle", false);
                }
                else
                {
                    state = "shooting";
                }

            }
            else
            {
                state = "Idle";
                Player.SetBool("Running", false);
                Player.SetBool("Idle", true);
            }
            if (state == "Running")
            {
                transform.Translate(0, 0, Time.deltaTime * speed);
            }
        
    }

}
