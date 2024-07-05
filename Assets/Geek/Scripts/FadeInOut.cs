using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField]
    float speed = 0.08f;  //透明化の速さ

    float inalfa;    //A値を操作するための変数
    float outalfa;
    float inred, ingreen, inblue;
    float red, green, blue;    //RGBを操作するための変数
    private bool FIFlag;
    private bool FOFlag;
    public GameObject FadeInPanel;
    public GameObject FadeOutPanel;

    //public GameObject fadePanel;
    // Start is called before the first frame update
    void Start()
    {
        //out
        red = FadeOutPanel.GetComponent<Image>().color.r;
        green = FadeOutPanel.GetComponent<Image>().color.g;
        blue = FadeOutPanel.GetComponent<Image>().color.b;
        outalfa = 0;
        FadeOutPanel.GetComponent<Image>().color = new Color(red, green, blue, outalfa);
        //in
        inred = FadeInPanel.GetComponent<Image>().color.r;
        ingreen = FadeInPanel.GetComponent<Image>().color.g;
        inblue = FadeInPanel.GetComponent<Image>().color.b;
        inalfa = 1;
        FadeInPanel.GetComponent<Image>().color = new Color(red, green, blue, inalfa);

        StartCoroutine("FadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        if (FIFlag == true)
        {
            FadeInPanel.GetComponent<Image>().color = new Color(inred, ingreen, inblue, inalfa);
            inalfa -= speed;
            if(inalfa <= 0.0f)
            {
                FIFlag = false;
                FadeInPanel.SetActive(false);
                inalfa = 1;
            }

        }
        if (FOFlag == true)
        {
            FadeOutPanel.GetComponent<Image>().color = new Color(red, green, blue, outalfa);
            outalfa += speed;
            Debug.Log("outalfa" + outalfa);
            if (outalfa >= 1)
            {
                FOFlag = false;
            }
        }
    }

    public void FadeStart()
    {
        FOFlag = true;
    }
    public IEnumerator FadeInandOut()
    {
        FadeOutPanel.SetActive(true);
        FOFlag = true;

        yield return new WaitUntil(() => !FOFlag);
        yield return new WaitForSeconds(1);


        FadeOutPanel.SetActive(false);
        outalfa = 0;

        FadeInPanel.SetActive(true);
        FIFlag = true;
    }

    public IEnumerator FadeOut()
    {
        FadeOutPanel.SetActive(true);
        FOFlag = true;
        yield return new WaitUntil(() => !FOFlag);

        //FadeOutPanel.SetActive(false);
        outalfa = 0;
    }

    public IEnumerator FadeIn()
    {
        FadeInPanel.SetActive(true);
        FIFlag = true;
        yield return new WaitUntil(() => !FIFlag);
    }
}
