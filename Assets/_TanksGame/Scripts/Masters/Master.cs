using System.Collections.Generic;
using UnityEngine;

public abstract partial class Master {

	public static GameMaster gameMaster { get; protected set; }
	public static InputMaster inputMaster { get; protected set; }
	public static MenuMaster menuMaster { get; protected set; }
	public static SpawnMaster spawnMaster { get; protected set; }
	public static MissionMaster missionMaster { get; protected set; }

	static List<Master> masters = new List<Master>();

	private static int frameLastCalled = 0;

	public static void Begin() {
		gameMaster = new GameMaster();
		masters.Add(gameMaster);
		inputMaster = new InputMaster();
		masters.Add(inputMaster);
		menuMaster = new MenuMaster();
		masters.Add(menuMaster);
		spawnMaster = new SpawnMaster();
		masters.Add(spawnMaster);
		missionMaster = new MissionMaster();
		masters.Add(missionMaster);
	}

	/// <summary>
	/// Should only be able to be called once per frame
	/// Tell all masters to update
	/// </summary>
	public static void StartMasterUpdate() {
		if (frameLastCalled == Time.frameCount) {
			Debug.LogError("Method was called twice in one frame");
		}
		foreach (Master master in masters) {
			master.MasterUpdate();
		}
	}

	protected virtual void MasterUpdate() {
	}
}
