using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Speed")]
    float currentMovementSpeed;
    public float minimumMovementSpeed;
    public float standartMovementSpeed;
    public float maximumMovementSpeed;

    Color _playerColor;
    bool _allowedToMove;

    //pointer position during previous frame
    Vector3 _startPointerPosition;
    //detect if pointer is down
    bool _pointerDown;

    float minimumClampedX = Mathf.NegativeInfinity;
    float maximumClampedX = Mathf.Infinity;

    const string OBSTACLE_TAG = "Obstacle";
    const float CHANGE_SPEED_STEP = 0.05f;

    private void Start()
    {
        //Get color of the player from attached material
        _playerColor = GetComponent<MeshRenderer>().material.color;

        //at start, set current speed as standart
        currentMovementSpeed = standartMovementSpeed;
    }

    /// <summary>
    /// Using FixedUpdate due to physics interactions of the Player
    /// </summary>
    private void FixedUpdate()
    {
        //Move only if allowed
        if (_allowedToMove)
        {
            //move forward
            Move(Vector3.forward);
#if UNITY_EDITOR
            //if pointer is pressed
            if (Input.GetMouseButton(0))
            {
                //if it was pressed earlier
                if (_pointerDown)
                {
                    //compare current pointer position and previous pointer position

                    //if previous x coordinate > current one 
                    if (_startPointerPosition.x > Input.mousePosition.x)
                    {
                        //move to left
                        Move(Vector3.left);
                    }
                    //if previous x coordinate < current one
                    else if (_startPointerPosition.x < Input.mousePosition.x)
                    {
                        //move to right
                        Move(Vector3.right);
                    }
                    //if previous y coordinate > current one
                    if (_startPointerPosition.y > Input.mousePosition.y)
                    {
                        //Descrese movement speed
                        ChangeSpeed(-CHANGE_SPEED_STEP);
                    }
                    //if previous y coordinate < current one
                    else if (_startPointerPosition.y < Input.mousePosition.y)
                    {
                        //Increase movement speed
                        ChangeSpeed(CHANGE_SPEED_STEP);
                    }
                }
                //if wasn't pressed earlier
                else
                {
                    //pointer is down
                    _pointerDown = true;
                }
                //remember new pointer position
                _startPointerPosition = Input.mousePosition;
            }
#elif UNITY_ANDROID || UNITY_IOS
            //if pointer is pressed
            if(Input.touchCount > 0)
            {
                //if it was pressed earlier
                if(_pointerDown)
                {
                    //compare current pointer position and previous pointer position

                    //if previous x coordinate > current one 
                    if (_startPointerPosition.x > Input.GetTouch(0).position.x)
                    {
                        //move to left
                        Move(Vector3.left);
                    }
                    //if previous x coordinate < current one
                    else if (_startPointerPosition.x < Input.GetTouch(0).position.x)
                    {
                        //move to right
                        Move(Vector3.right);
                    }
                       //if previous y coordinate > current one
                    if (_startPointerPosition.y > Input.GetTouch(0).position.y)
                    {
                        //Descrese movement speed
                        ChangeSpeed(-CHANGE_SPEED_STEP);
                    }
                    else if (_startPointerPosition.y < Input.GetTouch(0).position.y)
                    {
                        //Increase movement speed
                        ChangeSpeed(CHANGE_SPEED_STEP);
                    }
                }
                //if wasn't pressed earlier
                else
                {
                    //pointer is down
                    _pointerDown = true;
                }
                //remember new pointer position
                _startPointerPosition = Input.GetTouch(0).position;
            }


#endif
        }
    }

    /// <summary>
    /// Allow Player to move
    /// </summary>
    public void AllowToMove()
    {
        _allowedToMove = true;
    }

    /// <summary>
    /// Forbid Player to move
    /// </summary>
    public void ForbidToMove()
    {
        _allowedToMove = false;
    }

    /// <summary>
    /// Start smooth deceleration until player fully stops
    /// </summary>
    public void DecelerationTillStop()
    {
        StartCoroutine(Deceleration());
    }

    /// <summary>
    /// Smooth deceleration till full stop
    /// </summary>
    /// <returns></returns>
    IEnumerator Deceleration()
    {
        //Until speed will be <= 0
        while(currentMovementSpeed > 0)
        {
            //decrease spead by small amount every frame
            currentMovementSpeed -= CHANGE_SPEED_STEP / 10;
            yield return null;
        }
        //Finally, forbid to move
        ForbidToMove();
    }

    /// <summary>
    /// Move Player with specific vector
    /// </summary>
    /// <param name="movementVector">Vector to move</param>
    void Move(Vector3 movementVector)
    {
        //if it is moving left or right
        if (movementVector == Vector3.left || movementVector == Vector3.right)
        {
            //move using standart speed to prevent too fast moving sideways
            transform.Translate(movementVector * Time.deltaTime * standartMovementSpeed);
        }
        //in other cases
        else
        {
            //move using current movement speed
            transform.Translate(movementVector * Time.deltaTime * currentMovementSpeed);
        }
        //clamp Player position, so it will never get out of the borders
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minimumClampedX, maximumClampedX), transform.position.y, transform.position.z);
    }

    /// <summary>
    /// Change movement speed
    /// </summary>
    /// <param name="speedChangeStep">Variable to change speed</param>
    void ChangeSpeed(float speedChangeStep)
    {
        //Speed changing by this method allowed only during Game state
        if (GameManager.instance.currentGameState == GameManager.GameState.Game)
        {
            //add speedChangeStep
            currentMovementSpeed += speedChangeStep;
            //Clamp movement speed within min and max values
            currentMovementSpeed = Mathf.Clamp(currentMovementSpeed, minimumMovementSpeed, maximumMovementSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if there was a collision with obstacle and it's colot != players color
        if (collision.collider.CompareTag(OBSTACLE_TAG) && collision.gameObject.GetComponent<MeshRenderer>().material.color != _playerColor)
        {
            //Lose the game
            GameManager.instance.GameOver();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //This stuff is a valid thing only if Plane is positioned on zero X (or, at least, on border.x is > 0 and otherBorder.x is < 0 

        //if Border was triggered
        if (other.CompareTag("Border"))
        {
            //current Player coordinate X is greater than Zero
            if(transform.position.x > 0)
            {
                //set maxClampedX as current position
                maximumClampedX = transform.position.x;
            }
            //current Player coordinate X is lower than Zero
            else
            {
                //set min ClampedX as current position
                minimumClampedX = transform.position.x;
            }
        }
    }

}
