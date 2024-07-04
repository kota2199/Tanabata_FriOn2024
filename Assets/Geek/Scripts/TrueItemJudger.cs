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
    private bool[] withOrWithout;

    [SerializeField]
    private GameObject player;

    private Vector3 playerDefaultPosition;

    private int currentStageNumber = 1;

    [SerializeField]
    private Image gauge;

    [SerializeField]
    private float gaugeValue, maxGaugeValue;

    [SerializeField]
    private FadeInOut fadeController;

    [SerializeField]
    private GameObject clearUis;

    // Start is called before the first frame update
    void Awake()
    {
        withItem = false;
        currentStageNumber = 1;
        maxGaugeValue = gauge.rectTransform.sizeDelta.x;
    }

    private void Start()
    {
        player = this.gameObject;
        playerDefaultPosition = player.transform.position;
    }

    private void Update()
    {
        gauge.rectTransform.sizeDelta = new Vector2(maxGaugeValue * currentStageNumber / 7, 49f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "TrueItem")
        {
            withItem = true;
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Goal")
        {
            if (withItem == withOrWithout[currentStageNumber - 1])
            {
                StartCoroutine(ToNextStage(currentStageNumber));
                currentStageNumber++;
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

        IEnumerator enumerator = fadeController.FadeInandOut();
        yield return enumerator;

        stagePrefabs[stageNumber - 1].gameObject.SetActive(false);
        stagePrefabs[stageNumber].gameObject.SetActive(true);

        Initialize();
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
                stagePrefabs[4].gameObject.SetActive(true);
                currentStageNumber = 5;
                Initialize();
                break;

            case 7:
                //end
                clearUis.SetActive(true);
                break;
        }
    }

    private void Initialize()
    {
        player.transform.position = playerDefaultPosition;
        withItem = false;
    }
}
