using System.Collections;
using UnityEngine;

public class GameMaster : Master
{
    public static Tank.TankController playerTankController;

    public GameMaster()
    {
    }

    public void Update()
    {
        inputMaster.ProcessInput();
    }
}
