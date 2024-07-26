using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    public bool onMenu = false;

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
            }
            menuStatus(onMenu);
        }
    }

    private void menuStatus(bool status)
    {
        AudioController.instance.PlaySE(1);
        menuCanvas.SetActive(status);
    }

    public float SetDefaultValue(MenuSlider getter)
    {
        if(getter == seSlider)
        {
            return AudioController.instance.audioSourceForSe.volume;
        }
        else if(getter == bgmSlider)
        {
            return AudioController.instance.audioSourceForBgm.volume;
        }
        else if(getter == mouseSenseSlider)
        {
            return MouseSensitivityController.instance.mouseSense;
        }
        else
        {
            Debug.LogError("failed get default values");
            return 0;
        }
    }

    private void OnEnable()
    {
        seSlider.OnValueChanged += SeHandleValueChanged;
        bgmSlider.OnValueChanged += BgmHandleValueChanged;
        mouseSenseSlider.OnValueChanged += MouseHandleValueChanged;
    }

    private void OnDisable()
    {
        seSlider.OnValueChanged -= SeHandleValueChanged;
        bgmSlider.OnValueChanged -= BgmHandleValueChanged;
        mouseSenseSlider.OnValueChanged -= MouseHandleValueChanged;
    }

    private void SeHandleValueChanged(float newValue)
    {
        AudioController.instance.audioSourceForSe.volume = Mathf.Floor(newValue * 100f) / 100f; ;
    }

    private void BgmHandleValueChanged(float newValue)
    {
        AudioController.instance.audioSourceForBgm.volume = Mathf.Floor(newValue * 100f) / 100f;
    }

    private void MouseHandleValueChanged(float newValue)
    {
        MouseSensitivityController.instance.mouseSense = Mathf.Floor(newValue * 100f) / 100f;
    }

    public void SePointerUp()
    {
        AudioController.instance.PlaySE(3);
    }
}