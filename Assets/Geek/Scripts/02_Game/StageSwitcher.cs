using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSwitcher : MonoBehaviour
{
    //PlayerPosition
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Vector3 playerDefaultPosition;

    //LockPlayerControll
    public bool playerLock = false;

    //ステージのオブジェクト、1~4,5~6面でステージのベースが変わる。
    [SerializeField]
    private GameObject[] phase1Stages, phase2Stages;

    //違和感がある面とない面
    [SerializeField]
    private bool[] withFalse = new bool[7];

    //同じ面が重複して出現しないように、使用した面を管理する
    [SerializeField]
    private bool[] usedPhase1Stages, usedPhase2Stages;

    //現在の面数
    [SerializeField]
    private int currentStageNumber;

    //現在の面のステージオブジェクト
    [SerializeField]
    private GameObject currentStageObj;

    //アイテム所持の有無
    [SerializeField]
    private bool withItem;

    //フェードイン・アウト
    [SerializeField]
    private FadeInOut fadeController;

    //面のゲージ
    [SerializeField]
    private Image gauge;

    [SerializeField]
    private float gaugeValue, maxGaugeValue;

    //面数のカウント用テキスト
    [SerializeField]
    private Text stageCountText;

    private void Awake()
    {
        //重複回避用のBool値配列の要素数をステージ数と合わせる
        usedPhase1Stages = new bool[phase1Stages.Length];
        usedPhase2Stages = new bool[phase2Stages.Length];

        //面数の初期化
        currentStageNumber = 1;
        currentStageObj = phase1Stages[currentStageNumber - 1];

        //アイテム所有状況の初期化k
        withItem = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //ステージが遷移したときにプレイヤーを置き直すための座標を保存
        playerDefaultPosition = player.transform.position;

        //ゲージ画像の横幅をゲージの最大値に設定
        maxGaugeValue = gauge.rectTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        //ゲージと面数カウントテキストの情報を更新
        gauge.rectTransform.sizeDelta = new Vector2(maxGaugeValue * currentStageNumber / 7, 49f);
        stageCountText.text = currentStageNumber.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Itemに触れたらSEを再生、保有状況をTrueに、Itemを消去
        if(other.gameObject.tag == "TrueItem")
        {
            AudioController.instance.PlaySE(1);
            withItem = true;
            Destroy(other.gameObject);
        }

        //各面のゴールに触れたらプレイヤー操作をロックし、SEを再生する(遷移前にもう一度ゴールに触れてしまうことを防ぐためのロック)
        if(other.gameObject.tag == "Goal")
        {
            playerLock = true;

            AudioController.instance.PlaySE(2);

            //アイテムの保有状況と違和感がある面とない面が一致していたら次の面へ、一致していなかったら最初の面に戻る。
            //(違和感が無い面ならwithItemがfalseのとき、ある面ならtrueで次の面に行ける)
            if (withItem == withFalse[currentStageNumber - 1])
            {
                StartCoroutine(ToNextStage());
            }
            else
            {
                StartCoroutine(ReturnStage());
            }
        }
    }

    private IEnumerator ToNextStage()
    {
        //フェードアウトの完了を待つ
        IEnumerator enumerator = fadeController.FadeOut();
        yield return enumerator;

        //7面をゴールしたらクリアへ
        if (currentStageNumber == 7)
        {
            Debug.Log("Clear");
            ToClear();
        }

        //3面以下でゴールしたら同じベースのステージの次の面へ
        else if(currentStageNumber <= 3)
        {
            Debug.Log("Next_Phase1");
            currentStageObj.SetActive(false);
            int nextStageNumber = RandomStageNum(1);
            phase1Stages[nextStageNumber].SetActive(true);
            currentStageObj = phase1Stages[nextStageNumber];

            currentStageNumber++;
            Initialize();
        }
        //4面をゴールしたらベースのステージが変わる5面へ
        else if(currentStageNumber == 4)
        {
            currentStageObj.SetActive(false);
            phase2Stages[0].SetActive(true);
            currentStageObj = phase2Stages[0];

            currentStageNumber++;
            Initialize();
        }
        //5面と6面でゴールしたら次の面へ
        else if(currentStageNumber == 5 || currentStageNumber == 6)
        {
            currentStageObj.SetActive(false);
            int nextStageNumber = RandomStageNum(2);
            phase2Stages[nextStageNumber].SetActive(true);
            currentStageObj = phase2Stages[nextStageNumber];

            currentStageNumber++;
            Initialize();
        }
    }

    //同じ違和感のステージが重複して出現しないようにするメソッド。アクティブにするステージの配列インデックスを返す。
    private int RandomStageNum(int phase)
    {
        int randomNum = 0;
        bool isSelecting = false;

        //1~4面のとき = phase1
        if (phase == 1)
        {
            while (!isSelecting)
            {
                randomNum = Random.Range(1, phase1Stages.Length);
                Debug.Log("RandomNum : " + randomNum);
                if (!usedPhase1Stages[randomNum])
                {
                    usedPhase1Stages[randomNum] = true;
                    isSelecting = true;
                }
            }
        }

        //5~7面のとき = phase2
        else if (phase == 2)
        {
            while (!isSelecting)
            {
                randomNum = Random.Range(1, phase2Stages.Length);
                Debug.Log("RandomNum : " + randomNum);
                if (!usedPhase2Stages[randomNum])
                {
                    usedPhase2Stages[randomNum] = true;
                    isSelecting = true;
                }
            }
        }
        else
        {
            Debug.LogError("PhaseNumber is not correct.");
        }

        isSelecting = false;
        return randomNum;
    }

    //1~4面のフェーズ1なら1面へ。5~6面のフェーズ2なら5面に戻す。
    private IEnumerator ReturnStage()
    {
        IEnumerator enumerator = fadeController.FadeOut();
        yield return enumerator;

        if (currentStageNumber <= 4)
        {
            currentStageObj.SetActive(false);
            phase1Stages[0].SetActive(true);
            currentStageObj = phase1Stages[0];
            currentStageNumber = 1;
        }
        else
        {
            currentStageObj.SetActive(false);
            phase2Stages[0].SetActive(true);
            currentStageObj = phase2Stages[0];
            currentStageNumber = 5;
        }

        for(int i = 0; i < usedPhase1Stages.Length; i++)
        {
            usedPhase1Stages[i] = false;
        }
        for (int j = 0; j < usedPhase2Stages.Length; j++)
        {
            usedPhase2Stages[j] = false;
        }

        Initialize();
    }

    //面が変わったときにプレイヤーの座標、Itemの保有状況、プレイヤーの操作ロックを初期化する関数。
    private void Initialize()
    {
        player.transform.position = playerDefaultPosition;
        withItem = false;
        playerLock = false;
        StartCoroutine(fadeController.FadeIn());
    }

    //クリア演出シーンへ遷移
    private void ToClear()
    {
        SceneController.instance.ToCustomScene("03_ClearAnime");
    }
}
