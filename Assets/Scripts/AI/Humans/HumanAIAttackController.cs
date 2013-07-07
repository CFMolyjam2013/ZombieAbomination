using UnityEngine;
using System.Collections;

public class HumanAIAttackController : MonoBehaviour
{
    public static HumanAIAttackController instance;

    //movement variables
    public float fleeSpeed = 10.0f;
    public float calmMoveSpeed = 5.0f;
    public float rotateSpeed = 20.0f;

    //ranges
    public float minRange = 20.0f;
    public float attackRange = 60.0f;

    public float fireRate = 2.0f;

    private float moveSpeed = 0.0f;
    
    public int health = 100;

    public Transform bullet;

    [HideInInspector]
    public bool isDead = false;

    private int randomRot = 1;

    private float timer;

    private GameObject player;
    private PlayerPhysics playerPhysics;

    private Quaternion rot;

    public enum MoveState
    {
        fleeing,
        calm,
        stopped
    }

    public MoveState moveStates { get; set; }    

    void Awake()
    {
        instance = this;

        player = GameObject.FindWithTag("player");
        playerPhysics = player.GetComponent<PlayerPhysics>();
    }

    void Start()
    {
        timer = 2;
    }

	void LateUpdate ()
    {
        if (!isDead)
        {
            ProcessMotion();
        }
        MoveSpeed();
	}

    void ProcessMotion()
    {
        Vector3 targetDir = player.transform.position - transform.position;

        float dist = Vector3.Distance(player.transform.position, transform.position);

        //chase
        if (playerPhysics.zombieStates != PlayerPhysics.ZombieState.fullHuman)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), rotateSpeed);
            
            if (dist > minRange)
            {
                Wander();
            }
            //attack
            if (dist < attackRange && !IsInvoking("Shoot"))
            {
                Invoke("Shoot", fireRate);
            }
        }
        else
        {
            Wander();
        }
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void Wander()
    {
        if (timer == 2)
        {
            RandomizeRotation();
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            moveStates = MoveState.calm;

            if (!IsInvoking("WaitAndResetTimer"))
            {
                Invoke("WaitAndResetTimer", 2);
            }
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotateSpeed * Time.deltaTime);
    }

    void MoveSpeed()
    {
        switch (moveStates)
        {
            case MoveState.calm:
                moveSpeed = calmMoveSpeed;
                break;
            case MoveState.fleeing:
                moveSpeed = fleeSpeed;
                break;
            case MoveState.stopped:
                moveSpeed = 0;
                break;
        }
    }

    //shoot
    void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }

    void RandomizeRotation()
    {
        moveStates = MoveState.stopped;
        randomRot = Random.Range(-40, 40);
        rot = Quaternion.AngleAxis(randomRot, Vector3.up) * transform.rotation;
    }

    void WaitAndResetTimer()
    {
        timer = 2;
    }

    public void HealthControl(int dmg)
    {
        health += dmg;

        if (health <= 0)
        {
            isDead = true;
        }
    }

    public void Kill()
    {
        Destroy(gameObject, 2);
    }
}