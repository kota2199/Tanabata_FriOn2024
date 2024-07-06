using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearSceneManager : MonoBehaviour
{
    [SerializeField]
    private FadeInOut fadeController;

    private void OnEnable()
    {
        Debug.Log("enable");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Retry()
    {
        StartCoroutine(ToTitleWithFade("02_Game"));
    }

    public void ToTitle()
    {
        StartCoroutine(ToTitleWithFade("01_Title"));
    }

    private IEnumerator ToTitleWithFade(string sceneName)
    {
        AudioController.instance.PlaySE(2);
        IEnumerator enumerator = fadeController.FadeOut();
        yield return enumerator;
        AudioController.instance.PlayBGM(0);
        SceneController.instance.ToCustomScene(sceneName);
    }
}
