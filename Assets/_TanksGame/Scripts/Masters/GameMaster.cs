using UnityEngine;

public class GameMaster : Master {
	public static Tank playerTank;

	public GameMaster() {
		Debug.Log(GetType().Name + " Loaded!");
	}

	public void Update() {
		inputMaster.ProcessInput();
	}
}
