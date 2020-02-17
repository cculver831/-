using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

public class AIController : MonoBehaviour
{
    //reference to player object
    private Transform player;
    private Vector3 Target; //Where to aim
    public Vector3 destination; //Where to go
    NavMeshAgent agent;
    [Range(1, 100)]
    public float rotSpeed = 5;
    float speed = 6.5f;
    float visDis = 40.0f;
    float visAngle = 90.0f;
    private float shootdistance = 30f;
    public GameObject revovler;
    private Animator Player;
    string state = "Idle";
    public bool alive = true;

    public int Damage = 5;

    private float x;

    public RaycastHit Shot;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player= GameObject.FindWithTag("Player").GetComponent<Transform>().transform;
        Player = GetComponent<Animator>();
    }

    [Task]
    public void TargetPlayer()
    {
        Target = player.transform.position;
        Task.current.Succeed();
    }

    [Task]
    public void LookAtTarget()
    {
        Vector3 direction = Target - transform.position;
        // Rotates facing the player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
        if (Task.isInspected)
            Task.current.debugInfo = string.Format("angle={0}", Vector3.Angle(transform.forward, direction));

        if(Vector3.Angle(transform.forward, direction) < 5.0f)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    public void Fire()
    {
        var gunSound = GetComponentInChildren<AudioSource>();
        gunSound.Play();
        GetComponentInChildren<Animation>().Play("Revolver fire");


        RaycastHit Hit;
        if (Physics.Raycast(revovler.transform.position, revovler.transform.forward, out Hit))
        {
            Debug.DrawLine(transform.position, Hit.point, Color.red);
            Debug.Log( Hit.collider.gameObject.name);
            x = Hit.distance;
            if (Hit.transform.tag == "Player")
            {

                if (x > 30)
                {
                    Damage = 1;
                }
                else if (x <= 30 && x > 20)
                {
                    Damage = 2;
                }
                else if (x <= 20)
                {
                    Damage = 3;

                }
            }
            Hit.transform.SendMessage("DeductPoints", Damage, SendMessageOptions.DontRequireReceiver);
        }
        Task.current.Succeed();
    }



    [Task]
    public void PickDestination()
    {
        //Pick location to go to
        Vector3 dest = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
     
        agent.SetDestination(dest);
        Player.SetBool("Idle", false);
        Player.SetBool("Running", true);
       
        Task.current.Succeed();
    }

    //Moves AI to random Destination
    [Task]
    public void MoveToDestination()
    {
        //Debugging
        if (Task.isInspected)
            Task.current.debugInfo = string.Format("t = ", Time.time);
        //Checks distance from Random point
        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            Player.SetBool("Running", false);
            Player.SetBool("Idle", true);
            Task.current.Succeed();
        }
    }

}
