using UnityEngine;
using UnityEngine.AI;


/*
 * Basic Enemy class that will be added on as project expands
 **/
public class Enemy : GAgent
{
    public AudioClip[] AudioFilesSearching;
    public AudioClip[] AudioFilesDamage;
    private AudioSource audioData;
    //Text for visuals
    public GameObject Enemyobj;
    public  GameObject Text; 
    //setting melee range and health
    private float meleeRange = 4.0f;
    private int fullHealth = 50;
    public int CurrentHealth = 50;
    //self reference
    public NavMeshAgent agent;
    private bool doOnce = true;
   //gameobject we use to "track" player movement
    public GameObject lastLocation;
    public GameObject drop; //Ammo drop
    public WalkSpeeds settings;

    
    new void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //GetComponent<NavMeshAgent>().speed = settings.speed;
        // Call the base start
        base.Start();
        // Set up the subgoal "isWaiting"
        SubGoal s1 = new SubGoal("Safe", 4, true);
        // Add it to the goals
        goals.Add(s1, 6);
        SubGoal s2 = new SubGoal("Dodge", 1, false);
        goals.Add(s2, 1);
        SubGoal s3 = new SubGoal("KillPlayer", 3, false);
        goals.Add(s3, 5);

        SubGoal s5 = new SubGoal("Patrol", 4, false);
        goals.Add(s5, 5);
        InvokeRepeating("ReturnBeliefs", 0.1f, 0.1f);

        agent = this.gameObject.GetComponent<NavMeshAgent>();
        //GetComponent<NavMeshAgent>().Warp;
        beliefs.ModifyState("NotActivated", 0); //keeps enemy from active aggression (keeps passive until damage is taken
    }


    //Variables for Enemy Vision
    public Transform target;
    float visDist = 35.0f;
    float visAngle = 90.0f;
    float speed =15.0f;

    void Update()
    {

        EnemySight();
        //CheckHealth();
    }
    //Enemy Senses that are updated every tenth of a second
    void ReturnBeliefs()
    {
        
        CheckMelee();

        if (ISeeYou)
        {
            
            beliefs.AddStateOnce("SeesPlayer", 0); //adds sees player to belief state
            beliefs.RemoveState("Doesn'tSeePlayer");
            beliefs.AddStateOnce("JustSawPlayer", 3);
        }
        else
        {
            beliefs.RemoveState("SeesPlayer");
            beliefs.AddStateOnce("Doesn'tSeePlayer", 0);
            Text.SetActive(false);
        }

    }

    private bool ISeeYou = false; //for player
    void EnemySight()
    {

        // direction  is the target( player) location in the world 'compared' to this object's own position in the world
        Vector3 direction = target.position - transform.position;
        // angle is the angle this object is from the target
        float angle = Vector3.Angle(direction, transform.forward);

        //will need to be modified for player cover
        //add wall detection
        if (direction.magnitude < visDist && angle < visAngle)
        {
            NavMeshHit hit;
            if (!agent.Raycast(target.position, out hit))
            {
                direction.y = 0;

                //turns towards player
                // Determine which direction to rotate towards
                Vector3 targetDirection = target.position - transform.position;

                // The step size is equal to speed times frame time.
                float singleStep = speed * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

                // Draw a ray pointing at our target in
                Debug.DrawRay(transform.position, newDirection, Color.red);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                if(beliefs.HasState("Aggressive")|| beliefs.HasState("activated")) // if activated, show exclaimation text
                {
                    transform.rotation = Quaternion.LookRotation(newDirection);
                    ShowText();
                }
                lastLocation.transform.position = target.transform.position;
            
                //end of enemy Rotate

                ISeeYou = true;
            }
           
        }
        
        else
        {
            ISeeYou = false;
            //turns off text when player is no longer seen

        }
    }
    void ShowText()
    {
            Text.SetActive(true);
            //Debug.Log("Showing Text");

    }
   
    void CheckMelee()
    {
        if (Mathf.Abs(target.position.magnitude - transform.position.magnitude) <= meleeRange)
        {
            //lastLocation.transform.position = target.transform.position + spaceBetween;
            //Debug.Log("I can punch you, but I don't know how :/");
            beliefs.AddStateOnce("isinMeleeRange", 0);
            beliefs.RemoveState("NotinMeleeRange");
        }
        else
        {
            beliefs.AddStateOnce("NotinMeleeRange", 0);
            beliefs.RemoveState("isinMeleeRange");
        }

    }
    public bool doOnceDead = true;
    public void TakeDamage(int damage)
    {
        if(doOnce)
        {
            beliefs.ModifyState("activated", 0);
            beliefs.RemoveState("NotActivated");
            doOnce = false;
        }

        Debug.Log("I got hit");
        beliefs.AddStateOnce("JustSawPlayer", 0); //update beliefs
        audioData = GetComponent<AudioSource>(); //Use audio source
        audioData.clip = GetComponent<Enemy>().AudioFilesDamage[Random.Range(0, AudioFilesDamage.Length)]; // play random hit sound
        audioData.Play(); //play audio
        lastLocation.transform.position = target.transform.position; //update last location player was in when enemy was hit
        CurrentHealth -= damage;
        if (CurrentHealth < (fullHealth/2)) //checks if enemy needs to heal
        {
            beliefs.AddStateOnce("isHurt", 0);
            Debug.Log("I need to find cover!");
        }
        if(CurrentHealth < 0 && doOnceDead) //Enemy should be dead right now
        {
            Debug.Log("I should be dead right now");
            this.GetComponent<NavMeshAgent>().enabled = false;
            this.enabled = false;
            
            Invoke("Death", 3.0f);
            doOnceDead = false;
        }
    }
   
    //"deletes" enemy from game world
    //will be saved later to calculate score
    void Death()
    {
        for ( int i = 0; i < Random.Range(1,3); i ++)
        {
            Instantiate(drop, transform.position + new Vector3(0.0f, 0.1f, 0.0f), Quaternion.identity);
        }
           

            this.GetComponent<Enemy>().enabled = false;
            this.gameObject.SetActive(false);
  

    }
}
