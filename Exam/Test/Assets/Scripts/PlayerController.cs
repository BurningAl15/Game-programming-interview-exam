using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Tooltip("To set the speed of the pipe movement")]
    [SerializeField]
    private float speed;
    //Sets the limit of the player's movement
    private Vector3 movementLimit;

	void Start () {
	    movementLimit=new Vector3(Screen.width,Screen.height/3,0f);
        movementLimit=Camera.main.ScreenToWorldPoint(movementLimit);
    }
	
	void FixedUpdate () {
        Vector3 mouse=Input.mousePosition;
        mouse.z=10;
        mouse=Camera.main.ScreenToWorldPoint(mouse);
        if(mouse.y<movementLimit.y)
        {
            this.transform.position = Vector3.Lerp(transform.position,mouse,speed*Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("StartSnake") || other.tag.Equals("EndSnake"))
        {
            //To avoid snake of going through the pipe
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.tag="Spindle";
            StartCoroutine(GameController.instance.FadeOut());
        }
    }
}
