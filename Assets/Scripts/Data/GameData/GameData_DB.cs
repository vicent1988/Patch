using UnityEngine;
using LitJson;
using System;
using System.IO;
using System.Collections.Generic;

public class LanguageDB
{
    public string key;
    public string cht;
    public string chs;
    public string eng;
    public string kor;
    public string jpn;

}

public class TextureDB
{
    public string id;
    public string cht;
    public string chs;
    public string eng;
    public string kor;
    public string jpn;
}

public partial class GameData
{

    public Dictionary<string, LanguageDB> dicLanguageDB = new Dictionary<string, LanguageDB>();
    public Dictionary<string, TextureDB> dicTextureDB = new Dictionary<string, TextureDB>();

    public void SetLanguageDB(JsonData[] datas)
    {
        dicLanguageDB.Clear();
        foreach (JsonData data in datas)
        {
            LanguageDB container = new LanguageDB();
            container.key = data["key"].ToString();
            container.eng = data["eng"].ToString();
            container.chs = data["chs"].ToString();
            container.cht = data["cht"].ToString();
            container.kor = data["kor"].ToString();
            container.jpn = data["jpn"].ToString();
            dicLanguageDB.Add(container.key, container);

        }
    }

    public void SetTextureDB(JsonData[] datas)
    {
        dicTextureDB.Clear();
        foreach (JsonData data in datas)
        {
            TextureDB container = new TextureDB();
            container.id = data["id"].ToString();
            container.eng = data["eng"].ToString();
            container.chs = data["chs"].ToString();
            container.cht = data["cht"].ToString();
            container.kor = data["kor"].ToString();
            container.jpn = data["jpn"].ToString();
            dicTextureDB.Add(container.id, container);
        }
    }

    /// <summary>
    /// 加载策划配置表数据
    /// </summary>
    public void LoadDB()
    {
        if (isLoaded == false)
        {
            LoadDB_from_file();

            TextureManager.Instance.LoadTexture(QM.gd.LocalLanguage);
            //    wordFilterTree.AddFilterTexts(listForbiddenWordDB.ToArray());

            //    InitJewelKeys();

            //    cModelAtlas.LoadAtlases();

            //    isLoaded = true;

            //    PLogger.Log("LoadDataBase completed");

            //    //QM.pd.sfxVolume = float.Parse(QM.gd.GetGlobalDB(22));
            //    NGUITools.soundVolume = 1f;
            }

    }

    string encryptOption = "?encrypt=true&encryptiontype=AES128&password=f2WYP35djvxP2pdR";

    public delegate void DBdelegate(JsonData[] datas);

    public void SaveToByteFile<TKey, TValue>(string json_name, DBdelegate func, Dictionary<TKey, TValue> dic)
    {
        string path = Application.dataPath + "/Resources/Data/" + json_name + ".bytes";

        try
        {
            func(GetJsonData(json_name + ".json"));
        }
        catch (Exception e)
        {
            throw new Exception("Error From : " + json_name + "\n" + e);
        }
        if (dic != null)
        {
            ES2.Delete(path);
            ES2.Save(dic, path + encryptOption);
        }
    }

    public void SaveToByteFile<T>(string json_name, DBdelegate func, List<T> list)
    {
        string path = Application.dataPath + "/Resources/Data/" + json_name + ".bytes";
        try
        {
            func(GetJsonData(json_name + ".json"));
        }
        catch (Exception e)
        {
            throw new Exception("Error From : " + json_name + "\n" + e);
        }
        if (list != null)
        {
            ES2.Delete(path);
            ES2.Save(list, path + encryptOption);
        }
    }

    public void LoadFromByteFile<TKey, TValue>(string json_name, ref Dictionary<TKey, TValue> dic)
    {
        if (QM.gd.isPatch)
        {
            dic.Clear();

            ES2Settings newSettings = new ES2Settings(json_name);

            using (ES2Reader reader = ES2Reader.Create(PatchManager.Instance.dicPatchDataFile[json_name].bytes, newSettings))
            {
                dic = reader.ReadDictionary<TKey, TValue>(newSettings.filenameData.tag);
            }
            return;
        }
        else
        {
            string path = "Data/" + json_name + ".bytes?savelocation=resources";

            if (ES2.Exists(path))
            {
                dic.Clear();
                dic = ES2.LoadDictionary<TKey, TValue>(path);
            }
        }
    }

    public void LoadFromByteFile<T>(string json_name, ref List<T> list)
    {
        if (QM.gd.isPatch)
        {
            list.Clear();

            ES2Settings newSettings = new ES2Settings(json_name);

            using (ES2Reader reader = ES2Reader.Create(PatchManager.Instance.dicPatchDataFile[json_name].bytes, newSettings))
            {
                list = reader.ReadList<T>(newSettings.filenameData.tag);
            }
            return;
        }
        else
        {
            string path = "Data/" + json_name + ".bytes?savelocation=resources";

            if (ES2.Exists(path))
            {
                list.Clear();
                list = ES2.LoadList<T>(path);
            }
        }
    }

    public void LoadFromByteFile_NoPatch<TKey, TValue>(string json_name, ref Dictionary<TKey, TValue> dic)
    {
        string path = "Data/" + json_name + ".bytes?savelocation=resources";

        if (ES2.Exists(path))
        {
            dic.Clear();
            dic = ES2.LoadDictionary<TKey, TValue>(path);
        }
    }
    public void LoadFromByteFile_NoPatch<T>(string json_name, ref List<T> list)
    {
        string path = "Data/" + json_name + ".bytes?savelocation=resources";

        if (ES2.Exists(path))
        {
            list.Clear();
            list = ES2.LoadList<T>(path);
        }
    }

    private JsonData[] GetJsonData(string jsonFileName)
    {
        string response = ReadFile(jsonFileName);

        return JsonMapper.ToObject<JsonData[]>(response);
    }

    private string ReadFile(string fileName)
    {
        string t = "";
        string line = "";

        string replacedPath = Application.dataPath.Replace("/Assets", "") + "/Data/Json/" + fileName;

        StreamReader sr = new StreamReader(replacedPath);
        if (sr == null)
        {
            Debug.Log("Error : " + replacedPath);
        }
        else
        {
            line = sr.ReadLine();
            while (line != null)
            {
                t += line;
                line = sr.ReadLine();
                if (line != null)
                {
                    t += "\n";
                }
            }
            sr.Close();
        }
        return t;
    }

    public void SaveDB_to_file()
    {
        SaveToByteFile("LanguageDB", SetLanguageDB, dicLanguageDB);
        SaveToByteFile("TextureDB", SetTextureDB, dicTextureDB);

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    public void LoadDB_from_file()
    {
        LoadFromByteFile("LanguageDB", ref dicLanguageDB);
        LoadFromByteFile("TextureDB", ref dicTextureDB);
    }

    /*
   *数据库添加顺序
   *
   *1.创建类
   *
   *2.Edit=>Easy Save 2=>单击Manage Types
   *
   *3.相应的类add type
   *
   *4.创建SetClass
   *
   *6.在SaveDB_to_file函数中添加save
   *
   *7.LoadDB_from_file函数中添加load
   *
   *
   *只要excel存在，就会生成json文件。
   * */
}
