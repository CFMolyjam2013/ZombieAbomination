using UnityEngine;
using System.Collections;

public class HumanAIAttackController : MonoBehaviour
{
    public static HumanAIAttackController instance;

    //movement variables
    public float forwardSpeed = 10.0f;
    public float rotateSpeed = 20.0f;

    //ranges
    public float minRange = 20.0f;
    public float attackRange = 60.0f;

    public float fireRate = 2.0f;
    
    //[HideInInspector]
    public float moveSpeed = 0.0f;
    
    public int health = 100;

    public Transform bullet;

    private GameObject player;
    private PlayerPhysics playerPhysics;

    void Awake()
    {
        instance = this;

        player = GameObject.FindWithTag("player");
        playerPhysics = player.GetComponent<PlayerPhysics>();
    }

	void LateUpdate ()
    {
        ProcessMotion();
	}

    void ProcessMotion()
    {
        Vector3 targetDir = player.transform.position - transform.position;

        float dist = Vector3.Distance(player.transform.position, transform.position);

        //chase
        if (playerPhysics.zombieStates != PlayerPhysics.ZombieState.fullHuman)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), rotateSpeed);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            
            if (dist > minRange)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }

            //attack
            if (dist < attackRange && !IsInvoking("Shoot"))
            {
                Invoke("Shoot", fireRate);
            }
        }
    }

    //shoot
    void Shoot()
    {
        //Instantiate(bullet, transform.position, transform.rotation);
    }
}
