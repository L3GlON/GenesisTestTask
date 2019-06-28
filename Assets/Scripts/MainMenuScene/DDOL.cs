using UnityEngine;

/// <summary>
/// Utility class just to prevent destruction on load of its child gameobjects
/// </summary>
public class DDOL : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
