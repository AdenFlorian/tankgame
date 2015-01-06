using System.Collections;
using UnityEngine;

public class Master
{
    public static GameMaster gameMaster { get; protected set; }

    public static InputMaster inputMaster { get; protected set; }

    public static MenuMaster menuMaster { get; protected set; }

    public static void Begin()
    {
        gameMaster = new GameMaster();
        inputMaster = new InputMaster();
        menuMaster = new MenuMaster();
    }
}
