using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameType;
using System;

public class LanguageManager : Singleton<LanguageManager>
{
    public E_LangType eLangType = E_LangType.NONE;

    public override void InitData()
    {
    }

    public override void ClearData()
    {
    }

    public void SetGetLanguage()
    {
        eLangType = (E_LangType)PlayerPrefs.GetInt(PlayerPrefsKey.PLANG, (int)E_LangType.NONE);

        if (eLangType == E_LangType.NONE)
        {
            if (QM.gd.LocalLanguage == SystemLanguage.ChineseSimplified || QM.gd.LocalLanguage == SystemLanguage.Chinese)
            {
                eLangType = E_LangType.CHIS;
            }
            else if (QM.gd.LocalLanguage == SystemLanguage.ChineseTraditional)
            {
                eLangType = E_LangType.CHIT;
            }
            else if (QM.gd.LocalLanguage == SystemLanguage.Japanese)
            {
                eLangType = E_LangType.JPN;
            }
            else if (QM.gd.LocalLanguage == SystemLanguage.Korean)
            {
                eLangType = E_LangType.KOR;
            }
            else
            {
                eLangType = E_LangType.ENG;
            }
        }
        else
        {
            switch (eLangType)
            {
                case E_LangType.CHIS:
                    QM.gd.LocalLanguage = SystemLanguage.Chinese;
                    break;
                case E_LangType.CHIT:
                    QM.gd.LocalLanguage = SystemLanguage.ChineseTraditional;
                    break;
                case E_LangType.ENG:
                    QM.gd.LocalLanguage = SystemLanguage.English;
                    break;
                case E_LangType.KOR:
                    QM.gd.LocalLanguage = SystemLanguage.Korean;
                    break;
                case E_LangType.JPN:
                    QM.gd.LocalLanguage = SystemLanguage.Japanese;
                    break;
                default:
                    break;
            }
        }

        SaveLangType(E_LangType.ENG);
        TextureManager.Instance.LoadTexture(QM.gd.LocalLanguage);
    }

    public string GetLanguage(string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;

        string[] separator = { "/#" };
        string[] strSplit = str.Split(separator, StringSplitOptions.None);

        if (strSplit.Length > 0)
        {
            int index = 0;
            string str_result = "";

            for (int i = 0; i < strSplit.Length; i++)
            {
                if (QM.gd.dicLanguageDB.ContainsKey(strSplit[index]))
                {
                    switch (eLangType)
                    {
                        case E_LangType.CHIS:
                            str_result += QM.gd.dicLanguageDB[strSplit[index]].chs;
                            break;
                        case E_LangType.CHIT:
                            str_result += QM.gd.dicLanguageDB[strSplit[index]].cht;
                            break;
                        case E_LangType.ENG:
                            str_result += QM.gd.dicLanguageDB[strSplit[index]].eng;
                            break;
                        case E_LangType.KOR:
                            str_result += QM.gd.dicLanguageDB[strSplit[index]].kor;
                            break;
                        case E_LangType.JPN:
                            str_result += QM.gd.dicLanguageDB[strSplit[index]].jpn;
                            break;
                    }
                }
                else
                {
                    str_result += strSplit[index];
                }
                index++;
            }

            return str_result;
        }
        return str;
    }

    public void SaveLangType(E_LangType eLan)
    {
        eLangType = eLan;

        PlayerPrefs.SetInt(PlayerPrefsKey.PLANG, (int)eLangType);
        PlayerPrefs.Save();
    }
}
