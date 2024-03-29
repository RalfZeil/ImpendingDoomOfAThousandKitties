using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private Animator transition;

    [SerializeField]
    private float transitionTime = 1f;


    void Start()
    {
        transition = GetComponentInChildren<Animator>();
    }

    public static void LoadLevelStatic(string name)
    {
        SceneManager.LoadScene(name);
    }

    //Takes name of the scene to load and starts coroutine
    public void Loadlevel(string name)
    {
        StartCoroutine(LoadLevelRoutine(name));
    }

    //Starts the transition and after 1 second load the level
    IEnumerator LoadLevelRoutine(string levelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);
    }
}
