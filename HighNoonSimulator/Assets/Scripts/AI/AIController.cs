using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

public class AIController : MonoBehaviour
{
    //reference to player object
    private GameObject HealthPack = null;
    private Transform player;
    private Vector3 Target; //Where to aim
    public Vector3 destination; //Where to go
    NavMeshAgent agent;
    [Range(1, 100)]
    public float rotSpeed = 15;
    float VisualRange = 40.0f;
    float visDis = 40.0f;
    float visAngle = 90.0f;
    private float shootdistance = 30f;
    public GameObject revovler;
    private Animator Player;
    public bool alive = true;
    private Vector3 closestHealth;
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
    //Checks if player is dead or not
    [Task]
    bool dead()
    {
       if( GetComponent<NPCDamage>().enemyHealth <= 0)
        {
            Player.SetBool("Dying", true);
            return true;
        }
        else
        {
            return false;
        }
    }
    //Checks if AI can 'see' player
    // Raycasts to see if a wall is separting player from AI AND
    //Player is in sight
    [Task]
    bool seePlayer()
    {
        Vector3 distance = player.transform.position - transform.position;
        RaycastHit hit;
        bool seeWall = false;

        Debug.DrawRay(revovler.transform.position, distance, Color.red);

        if(Physics.Raycast(revovler.transform.position, distance, out hit))
        {
            if(hit.collider.gameObject.tag != "Player")
            {
                seeWall = true;
            }
         if(Task.isInspected)
            {
                Task.current.debugInfo = string.Format("wall {0}", seeWall);
            }
        }
        if(distance.magnitude < VisualRange && !seeWall)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    //Targets players location
    [Task]
    public void TargetPlayer()
    {
        Target = player.transform.position;
        Task.current.Succeed();
    }
    //Makes AI face the player
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
    //Shoots at player
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
        Player.SetBool("Running", true);
       
        Task.current.Succeed();
    }
    //Checks Health
    [Task]
    public bool IsHealthLessThan(int x)
    {
        if (GetComponent<NPCDamage>().enemyHealth <= x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //Checks if Danger is imminent
    [Task]
    public void TakeCover()
    {
        Vector3 awayFromPlayer = transform.position - player.transform.position;
        Vector3 dest = transform.position + awayFromPlayer * 2;
        agent.SetDestination(dest);
        Task.current.Succeed();
    }

    [Task]
    public void FindHealth()
    {
        
        float awayFromHealth = Mathf.Infinity;
        GameObject[] Health;
        Health = GameObject.FindGameObjectsWithTag("Health");
        foreach (GameObject health in Health)
        {
                
            Vector3 closest = health.transform.position - transform.position;
            float currDistance = closest.sqrMagnitude;
            if (currDistance < awayFromHealth)
            {
                HealthPack = health;
                awayFromHealth = currDistance;
            }
        }
        Task.current.Succeed();
        
    }
    [Task]
    public void Heal()
    {
       
        Vector3 dest = HealthPack.transform.position - transform.position;
        agent.SetDestination(HealthPack.transform.position);
        Player.SetBool("Running", true);
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            HealthPack.GetComponent<HealthPickUp>().pickedUp(this.gameObject);
            Player.SetBool("Running", false);
            Player.SetBool("Idle", true);
            GetComponent<NPCDamage>().enemyHealth = 10;
            Task.current.Succeed();
        }
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
