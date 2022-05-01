using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMarkerController : MonoBehaviour
{
    #region Singleton

    public static QuestMarkerController instance;

    void Awake () {
        if (instance != null) {
            Debug.LogError("THERE ARE TWO OR MORE QuestMarkerController INSTANCES! PLEASE KEEP ONLY ONE instance OF THIS SCRIPT");
        }else {
            instance = this;
        }
    }

    #endregion

    public Image image;

    public Transform targetTransform;

    public Vector2 clampPositionRatio = new Vector2(3, 27);

    // Update is called once per frame
    void Update()
    {
        if (targetTransform != null) {
            image.enabled = true;
            Vector3 newImagePosition = Camera.main.WorldToScreenPoint(targetTransform.position);
            if (Mathf.Abs(newImagePosition.z) / newImagePosition.z > 0) {
                image.rectTransform.position = new Vector3(Mathf.Clamp(newImagePosition.x, Camera.main.pixelWidth / clampPositionRatio.x, Camera.main.pixelWidth - (Camera.main.pixelWidth / clampPositionRatio.x)), Camera.main.pixelHeight - (Camera.main.pixelHeight / clampPositionRatio.y), 0);
            }
        }
        else {
            image.enabled = false;
        }
    }
}
