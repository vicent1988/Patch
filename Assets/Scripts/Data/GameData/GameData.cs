using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameType;

public partial class GameData
{

    private static GameData instance = new GameData();
    public static GameData GetInstance()
    {
        return instance;
    }

    public PlatformType platformType { get; set; }
    public bool isLive = true;
    public string strClientVersion { get; set; }
    public bool isPatch = false;

    public bool isLoaded { get; set; }
}
