using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class logoScene : MonoBehaviour
{
    private void Awake()
    {
        QM.gd.LocalLanguage = Application.systemLanguage;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        QM.gd.SetSystemSettingInfo();

        if (QM.gd.dicLanguageDB.Count == 0)
            QM.gd.LoadFromByteFile_NoPatch("LanguageDB", ref QM.gd.dicLanguageDB);

        TextureManager.Instance.Init();
        QM.gd.LoadFromByteFile_NoPatch("TextureDB", ref QM.gd.dicTextureDB);

        LanguageManager.Instance.Init();
        LanguageManager.Instance.SetGetLanguage();
        PLogger.Instance.Init();

        LoadTitleScene();
    }

    private void LoadTitleScene()
    {
        StartCoroutine(LoadTitleScene(2f));
    }

    private IEnumerator LoadTitleScene(float wait_time_seconds)
    {
        yield return new WaitForSeconds(wait_time_seconds);

        Resources.UnloadUnusedAssets();
        System.GC.Collect();

        SceneManager.LoadScene("MainScene");
    }
}
