using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        /// <summary>
        /// Player is not moving
        /// </summary>
        PreGame,
        /// <summary>
        /// Player is moving
        /// </summary>
        Game,
        /// <summary>
        /// Player collided with wrong obstacle
        /// </summary>
        Lose,
        /// <summary>
        /// Player reached finish
        /// </summary>
        Victory
    }
    public GameState currentGameState { get; private set; }

    public static GameManager instance;

    [Header("Player")]
    public PlayerController playerController;
    [Header("UI Panels")]
    public GameObject victoryPanel;
    public GameObject losePanel;

    private void Awake()
    {
        //Get GameManager static singleton
        instance = GetComponent<GameManager>();
        //at the start game state is PreGame
        currentGameState = GameState.PreGame;
    }

    private void Update()
    {
        //GameState Gmae can only be activated from PreGame state
        if (currentGameState == GameState.PreGame)
        {
#if UNITY_EDITOR
            //if deteced input
            if(Input.GetMouseButtonDown(0))
            {
                //Start Game state
                ChangeGameState(GameState.Game);
            }
#elif UNITY_ANDROID || UNITY_IOS
            //if deteced input
            if(Input.touchCount > 0)
            {
                //Start Game state
                ChangeGameState(GameState.Game);
            }
#endif
        }
    }

    /// <summary>
    /// Game is lost
    /// </summary>
    public void GameOver()
    {
        //Game can only be lost if GameState is Game
        if (currentGameState == GameState.Game)
        {
            //Change state to Lose
            ChangeGameState(GameState.Lose);
        }
        //Start player deceleration
        playerController.DecelerationTillStop();
    }

    /// <summary>
    /// Game is won
    /// </summary>
    public void LevelComplete()
    {
        //Game can only be won if GameState is Game
        if (currentGameState == GameState.Game)
        {
            //Change state to victory
            ChangeGameState(GameState.Victory);
        }
        //Start player deceleration top prevent eventually falling from the edge 
        playerController.DecelerationTillStop();
    }

    /// <summary>
    /// Change current game state and Apply any required changes
    /// </summary>
    /// <param name="newState"></param>
    void ChangeGameState(GameState newState)
    {
        //set new state
        currentGameState = newState;

        //if new state is Game state
        if(currentGameState == GameState.Game)
        {
            //allow player to move
            playerController.AllowToMove();
        }
        //if new state is Lose stae
        else if(currentGameState == GameState.Lose)
        {
            //show lose Panel
            losePanel.SetActive(true);
        }
        //if ne state is victory
        else if(currentGameState == GameState.Victory)
        {
            //show victory Panel
            victoryPanel.SetActive(true);
        }
    }


}
