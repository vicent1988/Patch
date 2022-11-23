using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureManager : Singleton<TextureManager>
{
    public Dictionary<string, Texture> TextureList = new Dictionary<string, Texture>();

    public override void InitData()
    {
    }

    public override void ClearData()
    {
    }

    public void LoadTexture(SystemLanguage type)
    {
        TextureList.Clear();
        string path = null;
        foreach (var texture in QM.gd.dicTextureDB)
        {
            switch (type)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    path = texture.Value.chs;
                    break;
                case SystemLanguage.ChineseTraditional: path = texture.Value.cht; break;
                case SystemLanguage.English: path = texture.Value.eng; break;
                case SystemLanguage.Korean: path = texture.Value.kor; break;
                case SystemLanguage.Japanese: path = texture.Value.jpn; break;
                default:
                    break;
            }

            path = string.Format("{0}/{1}.png", path);
            Texture tex = null;

#if UNITY_EDITOR
            try
            {
#endif
                tex = ResourceManager.Instance.LoadData<Texture>(ABPATH.TEXTURE_GLOBALTEXTURE, path, false);
#if UNITY_EDITOR
            }
            catch { }
#endif
            if (tex == null)
            {
                tex = Resources.Load<Texture>(string.Format("{0}/{1}", ABPATH.TEXTURE_GLOBALTEXTURE, System.IO.Path.ChangeExtension(path, null)));
            }

            if (tex != null)
            {
                TextureList.Add(texture.Key, tex);
            }
        }
    }

    public Texture GetTexture(string key)
    {
        if (key != null && TextureList.ContainsKey(key))
        {
            return TextureList[key];
        }
        return null;
    }
}
