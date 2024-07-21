using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAnimeController : MonoBehaviour
{
    [SerializeField]
    private GameObject player_pink;

    [SerializeField]
    private GameObject cam1, cam2, cam3;

    [SerializeField]
    private Animator playerAnim;

    [SerializeField]
    private float cam1Time, intervalCam2, intervalCam3, walkStop;

    [SerializeField]
    private FadeInOut fadeController;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Anim());
    }

    private IEnumerator Anim()
    {
        playerAnim.SetBool("walking", true);
        yield return new WaitForSeconds(cam1Time);
        cam1.SetActive(false);
        cam2.SetActive(true);
        yield return new WaitForSeconds(walkStop);
        playerAnim.SetBool("walking", false);
        yield return new WaitForSeconds(intervalCam2 - walkStop);
        cam2.SetActive(false);
        player_pink.SetActive(true);
        cam3.SetActive(true);
        yield return new WaitForSeconds(2);
        IEnumerator enumerator = fadeController.FadeOut();
        yield return enumerator;
        SceneController.instance.ToCustomScene("04_Clear");
    }
}
