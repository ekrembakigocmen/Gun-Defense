using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI lives;
    public TextMeshProUGUI waveInfo;
    private GameObject waveCount;

    private void Start()
    {
        waveCount = GameObject.FindGameObjectWithTag("GameController");
    }

    void Update()
    {
        lives.text = PlayerStats.Lives.ToString() + " Lives";
        waveInfo.text = WaveSpawner.won.ToString() + " / " + waveCount.transform.GetComponent<WaveSpawner>().waves.Length.ToString();
    }

    
}
