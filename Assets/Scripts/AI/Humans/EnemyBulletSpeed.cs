using UnityEngine;
using System.Collections;

public class EnemyBulletSpeed : MonoBehaviour
{
    private float bulletSpeed = 10.0f;

    private int damage = 10;

    private GameObject player;
    private PlayerPhysics playerPhysics;

    void Awake()
    {
        player = GameObject.FindWithTag("player");
        playerPhysics = player.GetComponent<PlayerPhysics>();
    }

	// Update is called once per frame
	void Update ()
    {
        Move();
        Kill();
	}

    void Move()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    void Kill()
    {
        Destroy(gameObject, 5);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "player")
        {
            playerPhysics.HealthControl(-damage);
            Destroy(gameObject);
        }
    }
}
