using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ToTitle()
    {
        StartCoroutine(ToTitleWithFade());
    }

    private IEnumerator ToTitleWithFade()
    {
        IEnumerator enumerator = fadeController.FadeOut();
        yield return enumerator;
        SceneController.instance.ToCustomScene("01_Title");
    }
}
