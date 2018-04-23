using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
        if (!Application.isEditor)
        {
            int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;

            if (sceneCount > 1)
            {
                for (int i = 1; i < sceneCount; i++)
                {
                    SceneManager.LoadScene(i, LoadSceneMode.Additive);
                    Debug.Log("Loaded Scene: " + SceneUtility.GetScenePathByBuildIndex(i));

                }
            }
        }
    }
	
}
