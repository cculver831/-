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
    float speed = 6.5f;
    float visDis = 40.0f;
    float visAngle = 90.0f;
    private float shootdistance = 30f;
    private Animator Player;
    string state = "Idle";
    public bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>().transform;
        Player = GetComponent<Animator>();
    }
    void Alive()
    {

    }
    // Update is called once per frame
    void Update()
    {



    }

    [Task]
    public void PickRandomDestination()
    {
        //Pick location to go to
        Vector3 dest = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
        agent.SetDestination(dest);
        Player.SetBool("Running", true);
        Player.SetBool("Idle", false);
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
