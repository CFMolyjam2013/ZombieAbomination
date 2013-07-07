using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public float yDistance = 20.0f;
    public float zDistance = 10.0f;

    private GameObject player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindWithTag("player");
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = new Vector3(0, yDistance, -zDistance) + player.transform.position;
	}
}
