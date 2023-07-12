using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test_MapLoading : MonoBehaviour
{
    private string praviousMapName;

    [SerializeField]
    private string currentMapName;

    [ContextMenu("LoadMap")]
    public void LoadMap()
    {   
        StartCoroutine(LoadingScene());
    }

    private IEnumerator LoadingScene()
    {
        if (!string.IsNullOrEmpty(praviousMapName))
        {
            var op = SceneManager.UnloadSceneAsync(praviousMapName);
            while (!op.isDone)
            {
                yield return null;
            }   
        }
        praviousMapName = currentMapName;
        SceneManager.LoadScene(currentMapName, LoadSceneMode.Additive);
    }
    
}
