using UnityEngine;

public class GameMaster : Master {

	public GameMaster() {
		Debug.Log(GetType().Name + " Loaded!");
	}

	protected override void MasterUpdate() {
	}
}
