using UnityEngine;

public class GameMaster : Master {

	public static bool isPlayerDead = false;
	public static bool isPlayerWin = false;
	public static bool isPlayerQuit = false;

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

	public void Pause() {
		Time.timeScale = 0;
	}

	public void Resume() {
		Time.timeScale = 1;
	}

	public void Reset() {
		isPlayerDead = false;
		isPlayerWin = false;
		isPlayerQuit = false;
	}
}
