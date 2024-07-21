using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    private bool onMenu = false;

    [SerializeField]
    private GameObject menuCanvas;

    [SerializeField]
    private MenuSlider seSlider, bgmSlider, mouseSenseSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (onMenu)
            {
                onMenu = false;
                AudioController.instance.PlaySE(3);
            }
            else
            {
                onMenu = true;
                SetSliderValue();
            }
            menuStatus(onMenu);
        }
    }

    private void menuStatus(bool status)
    {
        AudioController.instance.PlaySE(1);
        menuCanvas.SetActive(status);
    }

    private void SetSliderValue()
    {
        //seVolume.value = AudioController.instance.audioSourceForSe.volume * 100;
        //bgmVolume.value = AudioController.instance.audioSourceForBgm.volume * 100;
        //mouseSenseValue = ;
    }

    private void OnEnable()
    {
        seSlider.OnValueChanged += SeHandleValueChanged;
        bgmSlider.OnValueChanged += BgmHandleValueChanged;
    }

    private void OnDisable()
    {
        seSlider.OnValueChanged -= SeHandleValueChanged;
        bgmSlider.OnValueChanged -= BgmHandleValueChanged;
    }

    private void SeHandleValueChanged(float newValue)
    {
        //SEの音量を調整
        AudioController.instance.audioSourceForSe.volume = newValue / 100;
    }

    private void BgmHandleValueChanged(float newValue)
    {
        Debug.Log("Slider value changed: " + newValue);
        AudioController.instance.audioSourceForBgm.volume = newValue / 100;
    }

    public void SePointerUp()
    {
        AudioController.instance.PlaySE(3);
    }
}