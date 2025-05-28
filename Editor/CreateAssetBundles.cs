#if UNITY_EDITOR
using System.IO;
using UnityEditor;

namespace AssetBundleUtilities
{
    public static class CreateAssetBundles
    {
        [MenuItem("Assets/Build AssetBundles")]
        static void BuildAllAssetBundles()
        {
            if (!Directory.Exists(AssetLoader.AssetBundlePath))
            {
                Directory.CreateDirectory(AssetLoader.AssetBundlePath);
            }

            BuildPipeline.BuildAssetBundles(AssetLoader.AssetBundlePath, BuildAssetBundleOptions.None,
                BuildTarget.StandaloneWindows);
        }
    }
}
#endif