using System.IO;
using System.Text;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine.Networking;

public class PatchManager : MonoBehaviour
{
    internal struct ManiFastData
    {
        public long size;
        public Hash128 hash;

        public ManiFastData(long size, Hash128 hash)
        {
            this.size = size;
            this.hash = hash;
        }
    }

    #region singleton
    private static PatchManager _instance;

    public static PatchManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (PatchManager)GameObject.FindObjectOfType(typeof(PatchManager));
                if (_instance == null)
                {
                    GameObject container = new GameObject();
                    container.name = "PatchManager";
                    _instance = container.AddComponent(typeof(PatchManager)) as PatchManager;
                    DontDestroyOnLoad(container);
                }
            }
            return _instance;
        }
    }

    public void InitData()
    {
        PLogger.LogM(this.name + "initialized");
    }
    #endregion

    private bool m_bIsError = false;
    public string m_strAssetBundleBaseURL = "http://questgames.iptime.org:30000/";

    AssetBundleManifest serverManifast;
    Dictionary<string, ManiFastData> downloadList = new Dictionary<string, ManiFastData>();     
    public Dictionary<string, TextAsset> dicPatchDataFile = new Dictionary<string, TextAsset>(); 
}
