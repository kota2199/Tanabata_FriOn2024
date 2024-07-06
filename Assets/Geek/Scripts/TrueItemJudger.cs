using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrueItemJudger : MonoBehaviour
{
    private bool withItem = false;

    [SerializeField]
    private GameObject[] stagePrefabs;

    [SerializeField]
    private Material skybox_stage1, skybox_stage2;

    [SerializeField]
    private bool[] withOrWithout;

    [SerializeField]
    private GameObject player;

    private Vector3 playerDefaultPosition;

    [SerializeField]
    private int currentStageNumber = 1;

    [SerializeField]
    private Image gauge;

    [SerializeField]
    private float gaugeValue, maxGaugeValue;

    [SerializeField]
    private FadeInOut fadeController;

    [SerializeField]
    private GameObject clearUis;

    public bool playerLock = false;

    // Start is called before the first frame update
    void Awake()
    {
        withItem = false;
        currentStageNumber = 1;
        maxGaugeValue = gauge.rectTransform.sizeDelta.x;
    }

    private void Start()
    {
        RenderSettings.skybox = skybox_stage1;
        player = this.gameObject;
        playerDefaultPosition = player.transform.position;
        AudioController.instance.PlaySE(2);
    }

    private void Update()
    {
        gauge.rectTransform.sizeDelta = new Vector2(maxGaugeValue * currentStageNumber / 7, 49f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TrueItem")
        {
            AudioController.instance.PlaySE(1);
            withItem = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Goal")
        {
            playerLock = true;

            AudioController.instance.PlaySE(2);

            if (withItem == withOrWithout[currentStageNumber - 1])
            {
                StartCoroutine(ToNextStage(currentStageNumber));
            }
            else
            {
                StartCoroutine(ReturnStage(currentStageNumber));
            }
        }
    }

    private IEnumerator ToNextStage(int stageNumber)
    {
        Debug.Log("success");
        if(stageNumber == 7)
        {
            StartCoroutine(ToClear());
        }
        else
        {
            IEnumerator enumerator = fadeController.FadeInandOut();
            yield return enumerator;

            if (currentStageNumber == 5)
            {
                Debug.Log("Sed2");
                AudioController.instance.PlayBGM(1);
                RenderSettings.skybox = skybox_stage2;
                Debug.Log("Changed");
            }

            stagePrefabs[stageNumber - 1].gameObject.SetActive(false);
            stagePrefabs[stageNumber].gameObject.SetActive(true);

            //StartCoroutine(SendWebRequest.instance.PostNextSceneData(currentStageNumber));

            currentStageNumber++;

            Initialize();
        }
    }

    private IEnumerator ReturnStage(int stageNumber)
    {
        Debug.Log("fault");

        IEnumerator enumerator = fadeController.FadeInandOut();
        yield return enumerator;

        stagePrefabs[stageNumber - 1].gameObject.SetActive(false);

        switch (stageNumber)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                stagePrefabs[0].gameObject.SetActive(true);
                currentStageNumber = 1;
                Initialize();
                break;

            case 5:
            case 6:
            case 7:
                stagePrefabs[4].gameObject.SetActive(true);
                currentStageNumber = 5;
                Initialize();
                break;
        }
    }

    private void Initialize()
    {
        player.transform.position = playerDefaultPosition;
        withItem = false;
        playerLock = false;
    }

    private IEnumerator ToClear()
    {
        //StartCoroutine(SendWebRequest.instance.PostClearData());
        IEnumerator enumerator = fadeController.FadeOut();
        yield return enumerator;

        AudioController.instance.StopBGM();
        AudioController.instance.PlaySE(4);
        clearUis.SetActive(true);
        enumerator = fadeController.FadeIn();
        yield return enumerator;
    }
}
