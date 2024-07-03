using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    private FadeInOut fadeController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ToNextScene(string sceneName)
    {
        StartCoroutine(ToGame(sceneName));
    }

    private IEnumerator ToGame(string sceneName)
    {
        fadeController.FadeOut();
        yield return new WaitForSeconds(1.0f);
        SceneController.instance.ToGame();
    }
}
