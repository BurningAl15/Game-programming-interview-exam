using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour {
    
    private GameObject parent = null;
    private bool isColliding=false;
    
    void Update () {
		if(isColliding)
        {
            StartCoroutine(GlassController.instance.Behaviour(parent));
            isColliding=false;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        //Get the snake grand parent (whole snake and model) and set it to the parent gameobject
        if (collision.transform.parent.parent.name == "SnakeTest")
        {
            parent = collision.transform.parent.parent.gameObject;
        }

        //To avoid collide twice with same part of the snake
        if(collision.gameObject.tag.Equals("StartSnake"))
        {
            collision.gameObject.tag="Spindle";
            isColliding=true;
        }

        //To avoid snake of going through the pipe
        collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
