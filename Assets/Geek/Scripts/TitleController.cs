using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    private FadeInOut fadeController;

    [SerializeField]
    private GameObject explainPanel;

    private bool onSelect = false;

    public string teamAlphabet;

    [SerializeField]
    private Text teamAlphabetText;

    [SerializeField]
    private string[] teamAlphabets;

    private int teamNumber = 1;

    private void Start()
    {
        teamAlphabet = "A";
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        teamAlphabetText.text = teamAlphabet;
    }

    public void SelectOnOff()
    {
        if (onSelect)
        {
            onSelect = false;
        }
        else
        {
            onSelect = true;
        }
    }

    public void TeamSelect(string team)
    {
        onSelect = false;
        teamAlphabet = team;
    }

    public void PlusTeamAlphabet()
    {
        teamNumber++;
        if(teamNumber > teamAlphabets.Length)
        {
            teamNumber = 1;
        }
        teamAlphabet = teamAlphabets[teamNumber -1];
    }

    public void MinusTeamAlphabet()
    {
        teamNumber--;
        if (teamNumber < 1)
        {
            teamNumber = teamAlphabets.Length;
        }
        teamAlphabet = teamAlphabets[teamNumber -1];
    }

    // Update is called once per frame
    public void ToNextScene(string sceneName)
    {
        StartCoroutine(ToGame());
    }

    private IEnumerator ToGame()
    {
        IEnumerator enumerator = fadeController.FadeOut();
        yield return enumerator;
        SceneController.instance.ToGame();
    }

    public void GameStart()
    {
        explainPanel.SetActive(true);
        SendWebRequest.instance.team = teamAlphabet;
    }
}
