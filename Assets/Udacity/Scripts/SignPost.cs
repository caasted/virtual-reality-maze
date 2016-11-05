using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SignPost : MonoBehaviour
{
    public string sceneName = "";

    public void ResetScene() 
	{
        // Reset the scene when the user clicks the sign post
        SceneManager.LoadScene(sceneName);
    }
}