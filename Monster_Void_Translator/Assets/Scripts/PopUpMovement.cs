using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMovement : MonoBehaviour
{
    float bigScale = 1.3f;
    float timeScale = 0.3f;
    float recordContentSize = 300f;
    public static PopUpMovement Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void ShowPopUp(GameObject popUp)
    {
        var bg = popUp.GetComponent<RectTransform>().GetChild(0);
        var content = popUp.GetComponent<RectTransform>().GetChild(1);
        bg.gameObject.SetActive(true);
        content.DOScale(Vector3.one * bigScale, timeScale).OnComplete(() =>
        {
            content.DOScale(Vector3.one, timeScale);
        });
    }

    public void HidePopUp(GameObject popUp)
    {
        var bg = popUp.GetComponent<RectTransform>().GetChild(0);
        var content = popUp.GetComponent<RectTransform>().GetChild(1);
        content.DOScale(Vector3.one * bigScale, timeScale).OnComplete(() =>
        {
            content.DOScale(Vector3.zero, timeScale).OnComplete(()=>
            {
                bg.gameObject.SetActive(false);
            });
        });
    }

    public void ChangePopUp(GameObject popUpShow, GameObject popUpHide, bool leftToRight)
    {
        var widthScreen = Camera.main.pixelWidth;
        var showRect = popUpShow.GetComponent<RectTransform>();
        var hideRect = popUpHide.GetComponent<RectTransform>();
        if(leftToRight)
        {
            showRect.anchoredPosition = new Vector3(-2 * widthScreen , 0, 0);
            showRect.gameObject.SetActive(true);
            showRect.DOLocalMove(Vector3.zero, 0.3f);
            hideRect.DOLocalMove(new Vector3(widthScreen * 2, 0, 0), 0.3f);
        }
        else
        {
            showRect.anchoredPosition = new Vector3(widthScreen * 2, 0, 0);
            showRect.gameObject.SetActive(true);
            showRect.DOLocalMove(Vector3.zero, 0.3f);
            hideRect.DOLocalMove(new Vector3(-widthScreen * 2, 0, 0), 0.3f);
        }

    }

    public void RecordMove(GameObject record, int dir)
    {
        var rect = record.GetComponent<RectTransform>();
        rect.DOLocalMoveY(rect.localPosition.y + dir * recordContentSize, 0.3f);
    }
}
