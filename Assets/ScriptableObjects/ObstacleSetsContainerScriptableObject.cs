using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleSetsContainer", menuName = "ScriptableObjects/ObstacleSetsContainer")]
public class ObstacleSetsContainerScriptableObject : ScriptableObject
{
    public GameObject[] obstacleSets;


    /// <summary>
    /// Returns random obstacleSet from obstacleSets array 
    /// </summary>
    /// <returns></returns>
    public GameObject GetRandomSet()
    {
        return obstacleSets[Random.Range(0, obstacleSets.Length)];
    }
}
