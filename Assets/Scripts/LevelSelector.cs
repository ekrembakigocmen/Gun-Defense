using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class LevelSelector : MonoBehaviour
{
    AccountStats accountStats = new AccountStats();
    private string jsonString = "/Saves/JsonStats.json";
    public SceneFader fader;
    public Button[] levelBtns;
    



    private void Start()
    {


        for (int i = 0; i < levelBtns.Length; i++)
        {

            if (i + 1 <= UnlockMap() && i + 1 < 5)
            {
                
                levelBtns[i].interactable = true;
            }
            else
            {
                if (i + 1 <= AdvMap() && i + 1 <= UnlockMap())
                {
                  
                    levelBtns[i].interactable = true;
                }

            }
        }
    }

    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }

    public int UnlockMap()
    {

        string jsonRead = File.ReadAllText(Application.dataPath + jsonString);
        accountStats = JsonUtility.FromJson<AccountStats>(jsonRead);
        return accountStats.mapLevel;
    }
    public int AdvMap()
    {
        string jsonRead = File.ReadAllText(Application.dataPath + jsonString);
        accountStats = JsonUtility.FromJson<AccountStats>(jsonRead);
        return accountStats.levelReached;
    }
}
