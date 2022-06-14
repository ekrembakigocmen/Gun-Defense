using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundsSurvived : MonoBehaviour
{
    public TextMeshProUGUI roundsTxt;
    
    void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText ()
    {

        roundsTxt.text = "0";
        int round = 0;

        yield return new WaitForSeconds(.7f);
        while (round < PlayerStats.rounds)
        {
            round++;
            roundsTxt.text = round.ToString();
            yield return new WaitForSeconds(.05f);

        }


    }
}
