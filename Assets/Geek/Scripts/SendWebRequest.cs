using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class SendWebRequest : MonoBehaviour
{
    public static SendWebRequest instance;

    [SerializeField]
    string nextStageUrl = "";

    [SerializeField]
    string clearUrl = "";

    public string team;

    public int stage;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PostNextSceneData(3));
        }
    }

    bool IsWebRequestSuccessful(UnityWebRequest req)
    {
        /*プロトコルエラーとコネクトエラーではない場合はtrueを返す*/
        return req.result != UnityWebRequest.Result.ProtocolError &&
               req.result != UnityWebRequest.Result.ConnectionError;
    }

    public IEnumerator PostNextSceneData(int stage)
    {
        WWWForm form = new WWWForm();

        DateTime timeStamp = DateTime.Now;

        form.AddField("team_id", team);
        form.AddField("stage", stage);
        form.AddField("time_stamp", timeStamp.ToString());

        using (UnityWebRequest req = UnityWebRequest.Post(nextStageUrl, form))
        {
            //情報を送信
            yield return req.SendWebRequest();

            //リクエストが成功したかどうかの判定
            if (IsWebRequestSuccessful(req))
            {
                //Debug.Log("success");
                Debug.Log(req.downloadHandler.text);
            }
            else
            {
                Debug.Log(req.downloadHandler.text);
            }
        }
    }

    public IEnumerator PostClearData()
    {
        WWWForm form = new WWWForm();

        DateTime timeStamp = DateTime.Now;

        form.AddField("team_id", team);
        form.AddField("time_stamp", timeStamp.ToString());

        using (UnityWebRequest req = UnityWebRequest.Post(clearUrl, form))
        {
            //情報を送信
            yield return req.SendWebRequest();

            //リクエストが成功したかどうかの判定
            if (IsWebRequestSuccessful(req))
            {
                //Debug.Log("success");
                Debug.Log(req.downloadHandler.text);
            }
            else
            {
                Debug.Log(req.downloadHandler.text);
            }
        }
    }
}
