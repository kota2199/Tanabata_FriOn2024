using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://qiita.com/ga-mi-_qiita/items/ecba3343a14887bcb314
public class Rainbow : MonoBehaviour
{
    Material material = null;

    [Header("色変更スパン")]
    public float Chnge_Color_Time = 0.1f;

    [Header("変更の滑らかさ")]
    public float Smooth = 0.01f;

    [Header("色彩")]
    [Range(0, 1)] public float HSV_Hue = 1.0f;// 0 ~ 1

    [Header("彩度")]
    [Range(0, 1)] public float HSV_Saturation = 1.0f;// 0 ~ 1

    [Header("明度")]
    [Range(0, 1)] public float HSV_Brightness = 1.0f;// 0 ~ 1

    [Header("色彩 MAX")]
    [Range(0, 1)] public float HSV_Hue_max = 1.0f;// 0 ~ 1

    [Header("色彩 MIN")]
    [Range(0, 1)] public float HSV_Hue_min = 0.0f;// 0 ~ 1

    // Start is called before the first frame update
    void Start()
    {
        material =  GetComponent<Renderer>().material;
        HSV_Hue = HSV_Hue_min;
        StartCoroutine("Change_Color");
    }

    IEnumerator Change_Color()
    {
        HSV_Hue += Smooth;

        if (HSV_Hue >= HSV_Hue_max)
        {
            HSV_Hue = HSV_Hue_min;
        }

        material.color = Color.HSVToRGB(HSV_Hue, HSV_Saturation, HSV_Brightness);

        yield return new WaitForSeconds(Chnge_Color_Time);

        StartCoroutine("Change_Color");
    }
}