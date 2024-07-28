using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DepthOfFieldFocus : MonoBehaviour
{
    public PostProcessVolume postProcessVolume; // Post Processing Volume
    public Transform focusTarget; // フォーカスを合わせたいオブジェクト
    public float lerpSpeed = 2f; // フォーカス距離のスムージング速度

    private DepthOfField depthOfField;

    void Start()
    {
        // Post Process VolumeからDepth of Fieldの設定を取得
        postProcessVolume.profile.TryGetSettings(out depthOfField);
    }

    void Update()
    {
        if (depthOfField != null && focusTarget != null)
        {
            // カメラからフォーカス対象までの距離を計算
            float focusDistance = Vector3.Distance(Camera.main.transform.position, focusTarget.position);

            // 現在のフォーカス距離とターゲット距離を補間してスムーズに変更
            depthOfField.focusDistance.value = Mathf.Lerp(depthOfField.focusDistance.value, focusDistance, Time.deltaTime * lerpSpeed);
        }
    }
}
