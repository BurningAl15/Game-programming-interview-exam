using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {

    private static GravityManager _instance;
    public static GravityManager instance
    {
        get
        {
            if (_instance == null)
                _instance = GravityManager.FindObjectOfType<GravityManager>();
            return _instance;
        }
    }
    private Vector3 oldGravity;
    
    private void Start()
    {
        //Save current gravity
        oldGravity=Physics.gravity;
        //Freezes the gravity
        Physics.gravity=Vector3.zero;
    }
    
    public void TurnOnGravity()
    {
        //Sets the gravity to normal
        Physics.gravity=oldGravity;
    }
}
