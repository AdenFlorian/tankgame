using UnityEngine;

public class GameMaster : Master {
	//public static Tank playerTank;

	public GameMaster() {
		Debug.Log(GetType().Name + " Loaded!");
	}

	protected override void MasterUpdate() {
	}
}
