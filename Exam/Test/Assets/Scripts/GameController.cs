using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private static GameController _instance;
    public static GameController instance
    {
        get
        {
            if (_instance == null)
                _instance = GameController.FindObjectOfType<GameController>();
            return _instance;
        }
    }
    
    [Tooltip("Fade animator")]
    [SerializeField]
    private Animator fade;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    void Update () {
		if(Input.GetKeyDown(KeyCode.R))
            Reset();
	}

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator FadeIn()
    {
        fade.SetTrigger("FadeIn");
        yield return new WaitForSeconds(.1f);
        StopAllCoroutines();
    }

    public IEnumerator FadeOut()
    {
        //Make the fade out and send the player to next level
        fade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(.08f);
        
        int buildIndex=SceneManager.GetActiveScene().buildIndex+1;
        if(buildIndex>=3)
        {
            buildIndex=0;
        }
        SceneManager.LoadScene(buildIndex);

        StopAllCoroutines();
    }
}
