using System.Collections;
using UnityEngine;

public class InputMaster : Master
{
    public bool mouse0 { get; private set; }

    public InputMaster()
    {
    }

    public void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (Screen.lockCursor == false) {
                Screen.lockCursor = true;
                mouse0 = false;
            } else {
                mouse0 = true;
            }
        }
    }
}