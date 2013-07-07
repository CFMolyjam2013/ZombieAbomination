using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour
{
    private float angle;
    public float counterActAngle = 90.0f;

    public Transform currentProjectile;

    public static Aim instance;

	// Use this for initialization
	void Start ()
    {
        instance = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Aiming();
	}

    void Aiming()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);

        //get relative position from mouse position and player
        mousePos.x = mousePos.x - objPos.x;
        mousePos.y = mousePos.y - objPos.y;

        //get angle
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        //get rotation
        Quaternion rot = Quaternion.Euler(new Vector3(0, -angle + counterActAngle, 0));
        //apply rotation
        transform.rotation = rot;
    }

    public void Shoot()
    {
        Instantiate(currentProjectile, transform.position, transform.rotation);
    }
}
