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


    private void Start()
    {
        AudioController.instance.PlayBGM(0);
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

    public void GameStart()
    {
        AudioController.instance.PlaySE(1);
        explainPanel.SetActive(true);
    }

    // Update is called once per frame
    public void ToNextScene(string sceneName)
    {
        StartCoroutine(ToGame());
    }

    private IEnumerator ToGame()
    {
        AudioController.instance.PlaySE(0);
        IEnumerator enumerator = fadeController.FadeOut();
        yield return enumerator;
        yield return new WaitForSeconds(AudioController.instance.audioData.ses[0].seClip.length);
        SceneController.instance.ToGame();
    }
}
