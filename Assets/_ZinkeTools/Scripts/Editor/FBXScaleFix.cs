using UnityEditor;
namespace Zinke {
	/// <summary>
	/// For FBX models exported from blender
	/// </summary>
	public class FBXScaleFix : AssetPostprocessor {
		public void OnPreprocessModel() {
			ModelImporter modelImporter = (ModelImporter)assetImporter;
			modelImporter.globalScale = 1;
		}
	}
}
