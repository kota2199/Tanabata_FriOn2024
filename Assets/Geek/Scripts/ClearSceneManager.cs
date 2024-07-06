using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearSceneManager : MonoBehaviour
{
    [SerializeField]
    private FadeInOut fadeController;

    [SerializeField]
    private AdditionalItem itemManager;

    [SerializeField]
    private Text keywordText;

    private string keyword;

    private void OnEnable()
    {
        Debug.Log("enable");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        switch (itemManager.additionalItemCounter)
        {
            case 0:
            case 1:
                keyword = "彦星はわし座のベガ。";
                break;
            case 2:
                keyword = "彦星はわし座のベガ。織姫はこと座のアルタイル。";
                break;
            case 3:
                keyword = "彦星はわし座のベガ。織姫はこと座のアルタイル。はくちょう座のデネブを結ぶと夏の大三角。";
                break;
        }

        keywordText.text = keyword;
    }

    public void ToTitle()
    {
        StartCoroutine(ToTitleWithFade());
    }

    private IEnumerator ToTitleWithFade()
    {
        AudioController.instance.PlaySE(2);
        IEnumerator enumerator = fadeController.FadeOut();
        yield return enumerator;
        SceneController.instance.ToCustomScene("01_Title");
    }
}
