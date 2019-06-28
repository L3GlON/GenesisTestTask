using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    public Transform objectToFollow;

    float _zOffset;

    private void Start()
    {
        //detect offset on z coordinate from object to follow
        _zOffset = objectToFollow.position.z - transform.position.z;
    }

    //Use LateUpdate for camera following, because object could move in Update or FixeвUpdate
    private void LateUpdate()
    {
        //camera remains with detected earlier offset to followed object. 
        transform.position = new Vector3(transform.position.x, transform.position.y, objectToFollow.position.z - _zOffset);
    }
}
