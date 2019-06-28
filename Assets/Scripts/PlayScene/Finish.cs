using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Finish : MonoBehaviour
{
    const string PLAYER_TAG = "Player";


    private void OnTriggerEnter(Collider collider)
    {
        //if player triggered finish
        if(collider.CompareTag(PLAYER_TAG))
        {
            //Complete level
            GameManager.instance.LevelComplete();
        }
    }
}
