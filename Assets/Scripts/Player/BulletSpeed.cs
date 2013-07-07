using UnityEngine;
using System.Collections;

public class BulletSpeed : MonoBehaviour
{
    private float bulletSpeed = 0.0f;
    private float killTime = 0.0f;

    private int damage = 0;

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
        Speed();
        Kill();
	}

    void Speed()
    {
        bulletSpeed = PlayerPhysics.currentBulletSpeed;
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    void Kill()
    {
        switch (playerPhysics.weaponSelected)
        {
            case PlayerPhysics.WeaponSelect.pistol:
                killTime = 5.0f;
                break;
            case PlayerPhysics.WeaponSelect.shotty:
                killTime = 2.0f;
                break;
            case PlayerPhysics.WeaponSelect.rpgChainsaw:
                killTime = 5.0f;
                break;
        }

        Destroy(gameObject, killTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Zombie")
        {
            damage = PlayerPhysics.currDamage;
            ZombieAIController.instance.HealthControl(-damage);

            Destroy(gameObject);
        }
    }
}
