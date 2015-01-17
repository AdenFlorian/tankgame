using UnityEngine;

public class GameMaster : Master {

	public static bool isPlayerDead = false;
	public static bool isPlayerWin = false;

	GameState gameState;

	public GameMaster() {
		gameState = new GameMenu();
		gameState.Enter();
		Debug.Log(GetType().Name + " Loaded!");
	}

	protected override void MasterUpdate() {
		GameState newState = gameState.Update();
		if (newState != null) {
			gameState.Exit();
			gameState = newState;
			gameState.Enter();
		}
	}
}
