using System.Collections;
using UnityEngine;

public class GameMaster : Master
{
    public GameMaster()
    {
    }

    public void Update()
    {
        inputMaster.ProcessInput();
    }
}