using UnityEngine;

public static class ExtTransform {
	public static float PosX(this Transform transform) {
		return transform.position.x;
	}

	/// <summary>
	/// Sets world position to origin (0, 0, 0)
	/// </summary>
	/// <param name="transform"></param>
	public static void ZeroWorldPosition(this Transform transform) {
		transform.position = Vector3.zero;
	}

	/// <summary>
	/// Sets local position to local origin (0, 0, 0)
	/// </summary>
	/// <param name="transform"></param>
	public static void ZeroLocalPosition(this Transform transform) {
		transform.localPosition = Vector3.zero;
	}

	/// <summary>
	/// Sets world rotation to (0, 0, 0)
	/// </summary>
	/// <param name="transform"></param>
	public static void ZeroWorldRotation(this Transform transform) {
		transform.rotation = Quaternion.identity;
	}

	/// <summary>
	/// Sets local rotation to (0, 0, 0)
	/// </summary>
	/// <param name="transform"></param>
	public static void ZeroLocalRotation(this Transform transform) {
		transform.localRotation = Quaternion.identity;
	}
}
