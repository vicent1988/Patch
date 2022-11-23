using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLogger : Singleton<PLogger>
{
    public override void InitData()
    {
    }

    public override void ClearData()
    {
    }


    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void EditorLog(string message, bool isUse)
    {
        if (!isUse) return;
        Debug.Log(string.Format("<color=yellow>{0}</color>", message));
        return;
    }

    /// <summary>
    /// ��ɫ������־
    /// </summary>
    /// <param name="message"></param>
    public static void Log(string message)
    {
#if UNITY_EDITOR
        Debug.Log(string.Format("<color=yellow>{0}</color>", message));
#endif
    }
    
    /// <summary>
    /// ��ɫ������־
    /// </summary>
    /// <param name="message"></param>
    public static void LogR(string message)
    {
#if UNITY_EDITOR
        Debug.Log(string.Format("<color=red>{0}</color>", message));
#endif
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void EditorLogG(string message, bool isUse)
    {
        if (!isUse) return;
        Debug.Log(string.Format("<color=green>{0}</color>", message));
        return;
    }

    /// <summary>
    /// ��ɫ������־
    /// </summary>
    /// <param name="message"></param>
    public static void LogG(string message)
    {
#if UNITY_EDITOR
        Debug.Log(string.Format("<color=green>{0}</color>", message));
#endif
    }

    /// <summary>
    /// ��ɫ������־
    /// </summary>
    /// <param name="message"></param>
    public static void LogB(string message)
    {
#if UNITY_EDITOR
        Debug.Log(string.Format("<color=blue>{0}</color>", message));
#endif
    }

    /// <summary>
    /// ��ɫ������־
    /// </summary>
    /// <param name="message"></param>
    public static void LogO(string message)
    {
#if UNITY_EDITOR
        Debug.Log(string.Format("<color=orange>{0}</color>", message));
#endif
    }

    /// <summary>
    /// ��ϢЭ����־
    /// </summary>
    /// <param name="message"></param>
    public static void LogM(string message)
    {
#if UNITY_EDITOR
        if (PLogger.Instance.bNetwork)
            Debug.Log(string.Format("<color=magenta>{0}</color>", message));
#endif
    }

    /// <summary>
    /// ��ϢЭ����־
    /// </summary>
    /// <param name="message"></param>
    public static void LogError(string message)
    {
        Debug.LogError(message);
    }

    [Header("Settings")]
    public bool bNetwork;   //��ӡ��Ϣ��־
}
