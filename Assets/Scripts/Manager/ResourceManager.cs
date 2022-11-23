using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class ABPATH
{
    public const string TEXTURE_GLOBALTEXTURE = "textures/globaltexture";   //∂‡”Ô—‘Œ∆¿Ì

    public static readonly string[] PATHS = new string[]
    {
        TEXTURE_GLOBALTEXTURE,
    };
}

public class ResourceManager : Singleton<ResourceManager>
{
    Dictionary<string, AssetBundle> mAssetBundles = new Dictionary<string, AssetBundle>();

    public override void InitData()
    {
    }
     
    public override void ClearData()
    {
    }

    public void AddAssetBundle(AssetBundle _ab)
    {
        if (!mAssetBundles.ContainsKey(_ab.name))
            mAssetBundles.Add(_ab.name, _ab);
    }

    public T LoadData<T>(string _abName, string _path, bool _log = true) where T : UnityEngine.Object
    {
        if (_path.StartsWith("~")) 
            return null;

#if UNITY_EDITOR
        if (!QM.gd.isPatch)
        {
            var fullPath = Path.Combine("Asset", "AssetBundles", _abName, _path);
            var noPatchObj = AssetDatabase.LoadAssetAtPath<T>(fullPath);

            if (noPatchObj == null)
                throw new Exception(string.Format("Not Found Local AssetBundle Data. {0}.{1}", _abName, _path));
            return noPatchObj;
        }
#endif

        if (!mAssetBundles.ContainsKey(_abName))
        {
#if UNITY_EDITOR
            throw new Exception(string.Format("Not Find Assetbundle File. {0}", _abName));
#else
            if (_log)
                PLogger.LogError(string.Format("Not Find Assetbundle File. {0}", _abName));
            return null;
#endif

        }

        var p = System.IO.Path.GetFileName(_path);
        var obj = mAssetBundles[_abName].LoadAsset<T>(p);

        if (obj == null)
        {
#if UNITY_EDITOR
            throw new Exception(string.Format("Not Found AssetBundle Data. {0}.{1}", _abName, _path));
#else
            if (_log)
                Debug.LogError(string.Format("Not Found AssetBundle Data. {0}.{1}", _abName, _path));
            return null;
#endif
        }
        return obj;
    }
}
