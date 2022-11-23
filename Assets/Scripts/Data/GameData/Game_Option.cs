using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameType;

public partial class GameData
{
    public SystemLanguage LocalLanguage;


    public void SetSystemSettingInfo()
    {
        TextAsset textFile = Resources.Load("systemSettingInfo") as TextAsset;

        string[] strArray = textFile.text.Split(',');

        for (int i = 0; i < strArray.Length; i++)
        {
            if (strArray[i].Contains("isLive"))
            {
                string[] result = strArray[i].Split('=');
                isLive = bool.Parse(result[1]);
            }
            else if (strArray[i].Contains("platformType"))
            {
                string[] result = strArray[i].Split('=');
                platformType = (PlatformType)int.Parse(result[1]);
            }
            else if (strArray[i].Contains("version"))
            {
                string[] result = strArray[i].Split('=');
                strClientVersion = result[1];
            }
            else if (strArray[i].Contains("isPatch"))
            {
                string[] result = strArray[i].Split('=');
#if UNITY_EDITOR
                isPatch = bool.Parse(result[1]);
#else
                isPatch = true;
#endif
            }
        }
    }
}
