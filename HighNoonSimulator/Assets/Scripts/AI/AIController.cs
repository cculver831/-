using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    //reference to player object
    private GameObject HealthPack = null;
    private Transform player;
    private Vector3 Target; //Where to aim
    public Vector3 destination; //Where to go
    //navmesh used by enemy agent
    NavMeshAgent agent;
    //Rotation speed of enemy
    [Range(1, 100)]
    public float rotSpeed = 25;
    //Enemy Sight
    float VisualRange = 40.0f;
    float visAngle = 90.0f;
    private float shootdistance = 30f;

    //References to enemy Components
    public GameObject revovler;
    private Animator Player;
    private Vector3 closestHealth;
    public int Damage = 5;

    private float x;

    public RaycastHit Shot;

    //Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
      //player= GameObject.FindWithTag("Player").GetComponent<Transform>().transform;
        
    }
    private void Update()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>().transform;
        Player = GetComponent<Animator>();
    }
    //Checks if player is dead or not

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
    //Raycasts to see if a wall is separting player from AI and checks if player is in VisualRange

    bool seePlayer()
    {
        Vector3 distance = player.transform.position - transform.position;
        float angle = Vector3.Angle(distance, transform.forward);
        RaycastHit hit;
        bool seeWall = false;

        Debug.DrawRay(revovler.transform.position, distance, Color.red);
        //shoots raycast in forward direction to see if a player is in sight of enemy
        if(Physics.Raycast(revovler.transform.position, distance, out hit))
        {
            if(hit.collider.gameObject.tag != "Player")
            {
                seeWall = true;
            }

            {

            }
        }
        //Checks is enemy can see player, checks for distance, angle and walls
        if(distance.magnitude < VisualRange && angle < visAngle && !seeWall)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    //Targets players location

    public void TargetPlayer()
    {
        Target = player.transform.position;

    }
    //Makes AI face the player

    public void LookAtTarget()
    {
        Vector3 direction = Target - transform.position;

        //Rotates facing the player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);


        if(Vector3.Angle(transform.forward, direction) < 5.0f)
        {
 
        }
    }

    //Lines up Shot for AI

    public bool ShotLinedUp()
    {
        Vector3 distance = Target - transform.position;
        if(distance.magnitude < shootdistance && Vector3.Angle(transform.forward, distance) < 10.0f)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void SetTargetDestination()
    {
        agent.SetDestination(Target);
        Player.SetBool("Running", true);

    }
    //Stop AI from moving to aim

    public bool Stop(float angle)
    {
        //Make enemy look around for player
        var p = transform.position + Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;

        return true;
    }
    //Shoots at player

    public void Fire()
    {
        var gunSound = GetComponentInChildren<AudioSource>();
        gunSound.Play();

        RaycastHit Hit;
        if (Physics.Raycast(revovler.transform.position, revovler.transform.forward, out Hit))
        {
            Debug.DrawLine(transform.position, Hit.point, Color.red);

            x = Hit.distance;
            //Debug.Log("Gun shot distance: " + Hit.distance);
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

    }
    //Picks a random location to patrol

    public void PickDestination()
    {
        //Pick location to go to
        Vector3 dest = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
     
        agent.SetDestination(dest);
        Player.SetBool("Running", true);
       

    }

    public void lookFoward()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward), Time.deltaTime * rotSpeed);
    }
    //Checks Health

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

    public void TakeCover()
    {
        Vector3 awayFromPlayer = transform.position - player.transform.position;
        Vector3 dest = transform.position + awayFromPlayer * 2;
        agent.SetDestination(dest);

    }

    //Allows AI to run to nearest health pack

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

        
    }
    //Heals AI after health pack is found

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

        }
    }
    //Moves AI to random Destination

    public void MoveToDestination()
    {
        //Debugging

        //Checks distance from Random point
        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            Player.SetBool("Running", false);

        }
    }

}
