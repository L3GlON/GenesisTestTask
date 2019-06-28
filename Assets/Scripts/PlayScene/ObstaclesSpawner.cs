using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    //Scriptable object with different obstacle sets prefabs
    public ObstacleSetsContainerScriptableObject obstacleSetsSO;

    //array of point where obstacle sets should be spawned
    public Transform[] spawnPoints;

    private void Awake()
    {
        //for every spawn point
        for(int i = 0; i< spawnPoints.Length; i++)
        {
            //instantiate random obstacle set
            Instantiate(obstacleSetsSO.GetRandomSet(), spawnPoints[i]);
        }
    }
}
