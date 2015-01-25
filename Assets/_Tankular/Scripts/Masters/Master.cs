using System.Collections.Generic;
using UnityEngine;

public abstract class Master {

	protected static ActionMaster actionMaster;
	protected static ControllerMaster controllerMaster;
	protected static GameMaster gameMaster;
	protected static InputMaster inputMaster;
	protected static MenuMaster menuMaster;
	protected static MissionMaster missionMaster;
	protected static SpawnMaster spawnMaster;

	static List<Master> masters = new List<Master>();

	private static int frameLastCalled = 0;

	public Master() {
		masters.Add(this);
	}

	public static void MasterAwake() {
		actionMaster = new ActionMaster();
		controllerMaster = new ControllerMaster();
		gameMaster = new GameMaster();
		inputMaster = new InputMaster();
		menuMaster = new MenuMaster();
		missionMaster = new MissionMaster();
		spawnMaster = new SpawnMaster();

		//MissionMaster.LoadMission<Level1Mission>();

		// Load MainMenu
	}

	/// <summary>
	/// Should only be called once per frame
	/// Tell all masters to update
	/// </summary>
	public static void StartMasterUpdate() {
		if (frameLastCalled == Time.frameCount) {
			Debug.LogError("Method was called twice in one frame");
		}
		foreach (Master master in masters) {
			master.MasterUpdate();
		}
		frameLastCalled = Time.frameCount;
	}

	protected virtual void MasterUpdate() { }
}
