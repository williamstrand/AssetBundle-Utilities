using System.Collections.Generic;
using UnityEngine;

namespace AssetBundleUtilities
{
    public static class AssetLoader
    {
        public const string AssetBundlePath = "Assets/WSP/Asset Bundles/";

        static readonly bool Verbose = false;
        static readonly Dictionary<string, AssetBundle> AssetBundles = new();

        public static T LoadAsset<T>(string assetName, string assetBundleName) where T : Object
        {
            if (!AssetBundles.TryGetValue(assetBundleName, out var bundle))
            {
                bundle = AssetBundle.LoadFromFile($"{AssetBundlePath}{assetBundleName}");
                AssetBundles.Add(assetBundleName, bundle);
            }

            var asset = bundle.LoadAsset<T>(assetName);

            if (asset != null) return asset;

            var gameObject = bundle.LoadAsset<GameObject>(assetName);
            if (gameObject)
            {
                if (gameObject.TryGetComponent(out asset)) return asset;

                if (Verbose) Debug.LogError($"{typeof(T).Name} not found in AssetBundle.");
                return null;
            }

            if (Verbose) Debug.LogError($"{typeof(T).Name} not found in AssetBundle.");
            return null;
        }

        public static T[] LoadAllAssets<T>(string assetBundleName) where T : Object
        {
            if (!AssetBundles.TryGetValue(assetBundleName, out var bundle))
            {
                bundle = AssetBundle.LoadFromFile($"{AssetBundlePath}{assetBundleName}");
                AssetBundles.Add(assetBundleName, bundle);
            }

            var assets = bundle.LoadAllAssets<T>();

            if (assets.Length > 0) return assets;

            var gameObjects = bundle.LoadAllAssets<GameObject>();
            if (gameObjects.Length > 0)
            {
                var list = new List<T>();
                foreach (var gameObject in gameObjects)
                {
                    if (gameObject.TryGetComponent(out T asset)) list.Add(asset);
                }

                if (list.Count > 0) return list.ToArray();
            }

            if (Verbose) Debug.LogError($"{typeof(T).Name} not found in AssetBundle.");
            return null;
        }
    }
}