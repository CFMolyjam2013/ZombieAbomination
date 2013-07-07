using UnityEngine;
using System.Collections;

public class ZombieAIController : MonoBehaviour
{
    public static ZombieAIController instance;

    public float rangeFromPlayer = 3.0f;

    //movement forces
    public float rotateSpeed = 50.0f;
    public float moveSpeed = 10.0f;

    [HideInInspector]
    public int curHealth = 100;

    public int damage = 10;

    private int randomRot = 1;

    private float timer;

    private GameObject player;
    private PlayerPhysics playerPhysics;

    private Quaternion rot;

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

    void LateUpdate()
    {
        if (curHealth > 0)
        {
            ProcessMotion();
        }
    }

    void ProcessMotion()
    {
        Vector3 targetDir = player.transform.position - transform.position;

        float dist = Vector3.Distance(transform.position, player.transform.position);

        //chase until fully converted
        if (playerPhysics.zombieStates != PlayerPhysics.ZombieState.fullZombie)
        {
            //only chase if out of range
            if (dist > rangeFromPlayer)
            {
                CancelInvoke("Bite");
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), rotateSpeed);
            }
            //bite
            else if (!IsInvoking("Bite"))
            {
                InvokeRepeating("Bite", 0, 2);
            }
        }
        else
        {
            CancelInvoke("Bite");
            //wander
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
                if (!IsInvoking("WaitAndResetTimer"))
                {
                    Invoke("WaitAndResetTimer", 2);
                }
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotateSpeed * Time.deltaTime);
        }
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void Bite()
    {
        playerPhysics.HealthControl(-damage);
    }

    void RandomizeRotation()
    {
        randomRot = Random.Range(-40, 40);
        rot = Quaternion.AngleAxis(randomRot, Vector3.up) * transform.rotation;
    }

    void WaitAndResetTimer()
    {
        timer = 2;
    }

    public void HealthControl(int adj)
    {
        curHealth += adj;

        if (curHealth <= 0)
        {
            PlayerPhysics.killCount++;

            Destroy(gameObject, 2);
        }
    }
}
