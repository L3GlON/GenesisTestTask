using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    private void Start()
    {
        //Get static singleton SceneLoader
        instance = GetComponent<SceneLoader>();
    }


    /// <summary>
    /// Load scene asyncronously
    /// </summary>
    /// <param name="sceneName">Scene name</param>
    public void LoadScene(string sceneName)
    {
        //Check if scene with such name exists
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            //load scene with Single SceneMode
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
       }
       else
       {
            //if scene doesn't exists - log error
            Debug.LogError(sceneName + " scene does not exists!");
       }
    }
}
