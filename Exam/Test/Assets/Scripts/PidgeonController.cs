using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PidgeonController : MonoBehaviour {
    
    private float timer;

    [Header("Pidgeon atributes")]
    [Tooltip("Movement speed of the pidgeon")]
    [SerializeField]
    private float speed;

    [Tooltip("Movement direction of the pidgeon")]
    [SerializeField]
    private int direction;

    [Tooltip("Pidgeon Peck Child that holds the snake")]
    [SerializeField]
    private GameObject peck;

    void Start () {
        timer=Random.Range(5,10);
        direction=1;
	}

    private void Update()
    {
        timer-=Time.deltaTime;
        if(timer<=0)
        {
            GravityManager.instance.TurnOnGravity();
            peck.transform.DetachChildren();
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.right*direction*Time.deltaTime*speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        direction *=-1;

        float rotation=0f;
        if(other.gameObject.name=="RightLimit")
            rotation=180f;
        else if(other.gameObject.name=="LeftLimit")
            rotation=0f;

        transform.rotation = Quaternion.Euler(0f,rotation, 0f);
    }
}
