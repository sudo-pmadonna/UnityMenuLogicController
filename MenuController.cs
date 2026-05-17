using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// This is to ensure pathways between menus goes smoothly.
/// </summary>
public class MenuController : MonoBehaviour
{
    public GameObject[] menuSections; //Call these by index
    public int baseMenuPageIndex;

    private List<string> currentlyLoadedScenes = new List<string>();
    private string LOCAL_DIRECTORY = "Assets/Scenes/";

    /// <summary>
    /// Sets the baseline for starting
    /// </summary>
    void Start()
    {
        StartCoroutine(SetBasisToOff());
    }

    /// <summary>
    /// Activates a page via index argument
    /// </summary>
    /// <param name="index"></param>
    public void ActivateThisPageIndex(int index) 
    {
        if (index > menuSections.Count() || index < 0)
        {
            return;
        }
        menuSections[index].SetActive(true);
    }

    /// <summary>
    /// Deactivates a page via index argument
    /// </summary>
    /// <param name="index"></param>
    public void DeactivateThisPageIndex(int index) 
    {
        if (index > menuSections.Count() || index < 0)
        {
            return;
        }
        menuSections[index].SetActive(false);
    }

    public void GoToScene(string sceneName) 
    {
        //Tutorial2026/ConstructTutorialLevel1.unity -Sample sceneName variable
        //Use logic to check that it's in the build settings; To avoid null issue.
        string scene = Path.Combine(LOCAL_DIRECTORY, sceneName);
        Debug.Log(currentlyLoadedScenes[0]);
        if (currentlyLoadedScenes.Contains(scene))
        {
            SceneManager.LoadSceneAsync(scene);
        }
        else 
        {
            Debug.LogWarning($"{scene} not Found");
        }
    }


    /// <summary>
    /// Sets everything to a baseline state and collects all the currentScenes in build.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetBasisToOff() 
    {
        foreach (var scene in EditorBuildSettings.scenes)
        {
            currentlyLoadedScenes.Add(scene.path);
        }

        foreach (GameObject i in menuSections) 
        {
            i.SetActive(false);
        }
        menuSections[baseMenuPageIndex].SetActive(true);
        yield return null;
    }
}
