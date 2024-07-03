using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrueItemJudger : MonoBehaviour
{
    private bool isTrue = false;

    [SerializeField]
    private GameObject[] stagePrefabs;

    [SerializeField]
    private GameObject player;

    private Vector3 playerDefaultPosition;

    private int currentStageNumber = 1;

    [SerializeField]
    private Text currentStageText;

    // Start is called before the first frame update
    void Awake()
    {
        isTrue = false;
        currentStageNumber = 1;
        currentStageText.text = currentStageNumber.ToString();
    }

    private void Start()
    {
        player = this.gameObject;
        playerDefaultPosition = player.transform.position;
    }

    private void Update()
    {
        currentStageText.text = currentStageNumber.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "TrueItem")
        {
            isTrue = true;
        }
        else if(other.gameObject.tag == "FalseItem")
        {
            isTrue = false;
        }

        if(other.gameObject.tag == "Goal")
        {
            if (isTrue)
            {
                ToNextStage(currentStageNumber);
                currentStageNumber++;
            }
            else
            {
                ToFirstStage(currentStageNumber);
                currentStageNumber = 1;
            }
        }
    }

    private void ToNextStage(int stageNumber)
    {
        Debug.Log("StageNumber" + stageNumber);
        stagePrefabs[stageNumber - 1].gameObject.SetActive(false);
        stagePrefabs[stageNumber].gameObject.SetActive(true);

        player.transform.position = playerDefaultPosition;
        isTrue = false;
    }

    private void ToFirstStage(int stageNumber)
    {
        stagePrefabs[stageNumber - 1].gameObject.SetActive(false);
        stagePrefabs[0].gameObject.SetActive(true);
        player.transform.position = playerDefaultPosition;
        isTrue = false;
    }
}
