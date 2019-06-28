using UnityEngine;
using System.Collections.Generic;

public class ScreensSwitcher : MonoBehaviour
{
    public GameObject menuScreen;

    Stack<GameObject> openedScreens;

    //timer and cooldown for escape(back) button to prevent multiple clicks at one time
    float timer = 0f;
    const float ESCAPE_BUTTON_COOLDOWN = 1f;

    private void Start()
    {
        //create new stack of opened screens
        openedScreens = new Stack<GameObject>();
        //push main menu as first screen in stack
        openedScreens.Push(menuScreen);
    }

    private void Update()
    {
        //if "Back" button is pressed and cooldown was passed
        if (Input.GetKey(KeyCode.Escape) && timer >= ESCAPE_BUTTON_COOLDOWN)
        {
            //if stack contains only menu screen
            if(openedScreens.Count == 1)
            {             
                //Quit Application
                Application.Quit();
            }
            //if stack contains more screens
            else
            {
                //open previous screen
                OpenPreviousScreen();
            }
            //reset timer
            timer = 0f;
        }
        //add deltaTime to timer
        timer += Time.deltaTime;
    }

    /// <summary>
    /// Open new screen and add it to the stack
    /// </summary>
    /// <param name="screen">Screen to Open</param>
    public void OpenScreen(GameObject screen)
    {
        //Activate target screen
        screen.SetActive(true);
        //disable previous active screen
        openedScreens.Peek().SetActive(false);
        //add new screen to stack
        openedScreens.Push(screen);
    }

    /// <summary>
    /// Open previous screen and close current one
    /// </summary>
    void OpenPreviousScreen()
    {
        //Remove current screen from stack and disable it
        openedScreens.Pop().SetActive(false);
        //Activate next screen from the stack (it will be previous open)
        openedScreens.Peek().SetActive(true);
    }
}
