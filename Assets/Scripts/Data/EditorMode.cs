using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class EditorMode
{
#if UNITY_EDITOR
    private const string m_ResPathName = "Assets/Resources/";
    public static string GetResourcePath(Object go)
    {
        if (go == null)
        {
            return string.Empty;
        }

        string path = UnityEditor.AssetDatabase.GetAssetPath(go);
        string file = GetFileTitle(path);
        path = System.IO.Path.GetDirectoryName(path);
        path += "/";
        path = path.Replace(m_ResPathName, string.Empty);
        path += file;

        return path;
    }
    private static string GetFileTitle(string path)
    {
        return System.IO.Path.GetFileNameWithoutExtension(path);
    }
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void CopyComponent(Component component)
    {
        if (component == null)
        {
            return;
        }

        UnityEditorInternal.ComponentUtility.CopyComponent(component);
        return;
    }
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void PasteComponent(GameObject obj, System.Type type, bool isShowDialog = true)
    {
        if (obj == null)
        {
            return;
        }

        var component = obj.GetComponent(type) as Component;
        if (component != null)
        {
            UnityEditorInternal.ComponentUtility.PasteComponentValues(component);
        }
        else
        {
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(obj);
        }

        if (isShowDialog) UnityEditor.EditorUtility.DisplayDialog("保存完成", string.Format("{0} 脚本保存完成。", type.Name), "确认");
        return;
    }
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void SelectionObject(MonoBehaviour monoBehaviour)
    {
        if (monoBehaviour == null)
        {
            return;
        }

        if (UnityEditor.Selection.activeGameObject == monoBehaviour.gameObject)
        {
            return;
        }

        UnityEditor.Selection.SetActiveObjectWithContext(monoBehaviour.gameObject, null);
        return;
    }
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void CopyBuffer(string strMessage)
    {
        GUIUtility.systemCopyBuffer = strMessage;
        PLogger.EditorLog("CopyBuffer", true);
        return;
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void SetAssetBundleName(string resPath, string bundleName)
    {
        var objs = Resources.LoadAll(resPath);
        for (int i = 0; i < objs.Length; i++)
        {
            var singlePath = AssetDatabase.GetAssetPath(objs[i]);
            AssetImporter.GetAtPath(singlePath).SetAssetBundleNameAndVariant(bundleName, "");
            Debug.Log(singlePath + " added " + bundleName);
        }
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void SetAssetBundleNameCustom(string resPath)
    {
        var objs = Resources.LoadAll(resPath);
        for (int i = 0; i < objs.Length; i++)
        {
            var singlePath = AssetDatabase.GetAssetPath(objs[i]);

            if (objs[i].name == "LanguageDB")
            {
                AssetImporter.GetAtPath(singlePath).SetAssetBundleNameAndVariant("languagedata", "");
            }
            else
            {
                AssetImporter.GetAtPath(singlePath).SetAssetBundleNameAndVariant("tabledata", "");
            }
        }
    }
#endif
}