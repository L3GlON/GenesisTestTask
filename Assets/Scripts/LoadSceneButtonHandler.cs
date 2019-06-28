using UnityEngine;

public class LoadSceneButtonHandler : MonoBehaviour
{
    public string sceneToLoadName;

    /// <summary>
    /// Load scene with set name. 
    /// </summary>
    public void LoadScene()
    {
        //Use SceneLoader singleton
        SceneLoader.instance.LoadScene(sceneToLoadName);
    }
}
