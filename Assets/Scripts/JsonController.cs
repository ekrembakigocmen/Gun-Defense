using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonController : MonoBehaviour
{
    AccountStats accountStats = new AccountStats();
    private string jsonString = "/Saves/JsonStats.json";
    private int levelUp = 1;
    
    private void Awake() // ilk kayit dosyasini olusturma
    {
        if (File.Exists(Application.dataPath+jsonString))
        {
            return;
        }
        else
        {
            Debug.Log("dosya olusturuldu");
            JsonSave(levelUp);
        }

    }

    private void Start()
    {
        JsonLoad();
    }
    public void JsonSave(int levelUp)
    {
        accountStats.mapLevel = levelUp; // += olmamasinin sebebi sadece ilk mapi oynayarak mapLevel seviyesini yuksaltmesin
        string jsonWrite = JsonUtility.ToJson(accountStats);
        File.WriteAllText(Application.dataPath + jsonString, jsonWrite);
        Debug.Log(jsonWrite);
    }

    public void JsonSaveAdv(int AdvKey)
    {
        accountStats.advKeys += AdvKey;
        string jsonWrite = JsonUtility.ToJson(accountStats);
        File.WriteAllText(Application.dataPath + jsonString, jsonWrite);
        Debug.Log(jsonWrite);
    }

    public void JsonSaveReached(int Reached)
    {
        accountStats.levelReached += Reached;
        string jsonWrite = JsonUtility.ToJson(accountStats);
        File.WriteAllText(Application.dataPath + jsonString, jsonWrite);
        Debug.Log(jsonWrite);

    }
    public void JsonLoad()
    {
        if (File.Exists(Application.dataPath + jsonString))
        {
            string jsonRead = File.ReadAllText(Application.dataPath + jsonString);
            accountStats = JsonUtility.FromJson<AccountStats>(jsonRead);
           
        }
        else
            Debug.Log("Dosya bulunamadi");


    }
}
