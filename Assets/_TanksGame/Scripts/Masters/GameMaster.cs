using System.Collections;
using UnityEngine;

public class GameMaster : Master
{
    public static Tank.Tank playerTank;

    public GameMaster()
    {
        Debug.Log(GetType().Name + " Loaded!");
    }

    public void Update()
    {
        inputMaster.ProcessInput();
    }
}
