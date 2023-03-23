using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SurveyIDGenerator : MonoBehaviour
{
    private bool showingIDs = false;

    public void GenerateID()
    {
        if (PlayerPrefs.GetInt("IDsGenerated") != 1) //generate IDs if they haven't been already
        {
            print("Generating Player IDs");

            int player1ID, player2ID;
            player1ID = Random.Range(100000, 1000000);
            player2ID = Random.Range(100000, 1000000);
            PlayerPrefs.SetInt("Player1ID", player1ID);
            PlayerPrefs.SetInt("Player2ID", player2ID);

            PlayerPrefs.SetInt("IDsGenerated", 1);
        }

        ShowID();
    }

    public void ShowID()
    {
        if (!showingIDs && PlayerPrefs.GetInt("Player1ID") != 0)
        {
            GameObject.Find("Player1IDText").GetComponent<TextMeshProUGUI>().text += PlayerPrefs.GetInt("Player1ID");
            GameObject.Find("Player2IDText").GetComponent<TextMeshProUGUI>().text += PlayerPrefs.GetInt("Player2ID");

            showingIDs = true;
        }
    }
}