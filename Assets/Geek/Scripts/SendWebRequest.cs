using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendWebRequest : MonoBehaviour
{
    [SerializeField]
    string url = "";

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StartCoroutine(GetData());
            StartCoroutine(PostData("example_data"));
        }
    }

    bool IsWebRequestSuccessful(UnityWebRequest req)
    {
        /*プロトコルエラーとコネクトエラーではない場合はtrueを返す*/
        return req.result != UnityWebRequest.Result.ProtocolError &&
               req.result != UnityWebRequest.Result.ConnectionError;
    }

    IEnumerator GetData()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(url)) //UnityWebRequest型オブジェクト
        {
            yield return req.SendWebRequest(); //URLにリクエストを送る

            if (IsWebRequestSuccessful(req)) //成功した場合
            {
                Debug.Log(req.downloadHandler.text);
            }
            else                            //失敗した場合
            {
                Debug.Log("error");
            }
        }
    }

    IEnumerator PostData(string message)
    {
        WWWForm form = new WWWForm();

        string text = message;

        form.AddField("data", text);

        using (UnityWebRequest req = UnityWebRequest.Post(url, form))
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
                Debug.Log("error");
            }
        }
    }
}
