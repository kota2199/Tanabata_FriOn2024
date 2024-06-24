using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueItemJudger : MonoBehaviour
{
    private bool isTrue = false;

    [SerializeField]
    private GameObject[] stagePrefabs;

    [SerializeField]
    private GameObject player;

    private Vector3 playerDefaultPosition;

    private int currentStageNumber = 1;

    // Start is called before the first frame update
    void Awake()
    {
        isTrue = false;
        currentStageNumber = 1;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerDefaultPosition = player.transform.position;
    }

    private void Update()
    {
        Debug.Log("CurrentStage : " + currentStageNumber);
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
