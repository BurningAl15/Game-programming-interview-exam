using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassController : MonoBehaviour {

    private static GlassController _instance;
    public static GlassController instance
    {
        get
        {
            if (_instance == null)
                _instance = GlassController.FindObjectOfType<GlassController>();
            return _instance;
        }
    }
    [Tooltip("Invisible collider that works as glass lid")]
    [SerializeField]
    private BoxCollider lid;

    [Tooltip("Targets to move the glass:\n1)Right Top of the screen.\n2)Center of the screen.")]
    [SerializeField]
    private Transform[] positions=new Transform[2];
	
    [Tooltip("Glass prefab")]
    [SerializeField]
    private GameObject glass;

    float speed;

    void Start () {
        Vector3 centerScreen=new Vector3(Screen.width / 2, Screen.height / 2, 0f);
        positions[1].position = Camera.main.ScreenToWorldPoint(centerScreen);

        //Random Speed
        speed=Random.Range(150,300);
    }
	
    public IEnumerator Behaviour(GameObject temp)
    {
        //Waiting for the snake to lie down in the glass
        yield return new WaitForSeconds(2.5f);
        lid.isTrigger=false;

        //Set the snake as child
        if (temp != null)
            temp.transform.SetParent(glass.transform);

        //Move towards the right top of the screen
        for (float i = 0; i <1;i+=0.01f)
        {
            glass.transform.position= Vector3.Lerp(glass.transform.position,positions[0].position,Mathf.InverseLerp(0,1,i));
            yield return null;
        }
        yield return new WaitForSeconds(.1f);
        
        //Get Random Direction
        float randomVal=Random.value;
        Vector3 direction;
        if(randomVal<.5f)
            direction=Vector3.forward;
        else
            direction=Vector3.back;
        
        //Rotates around the target 2 for 6 seconds
        for (float i = 0; i < 6; i += Time.deltaTime)
        {
            glass.transform.RotateAround(positions[1].position, direction, Time.deltaTime*speed);
            yield return null;
        }
        yield return new WaitForSeconds(.1f);

        temp.transform.GetChild(0).GetComponentInChildren<Collider>().gameObject.tag="StartSnake";

        //Make trigger the lid to let the snake fall down
        lid.isTrigger=true;
        glass.transform.rotation=Quaternion.Euler(0f,0f,180f);

        StopAllCoroutines();
    }
}
