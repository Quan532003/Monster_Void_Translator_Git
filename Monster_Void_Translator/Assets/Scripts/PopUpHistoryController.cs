using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopUpHistoryController : MonoBehaviour
{
    public List<RecordInHistory> recordList = new List<RecordInHistory>();
    List<GameObject> inforInRecord = new List<GameObject>();
    public RectTransform content;
    public RecordInHistory recordPrefab;
    public static PopUpHistoryController Instance;
    private void Awake()
    {
        Instance = this;
        
        SetClickedBtn();
    }
    void OnRecordClicked(int index)
    { 
        var rect = inforInRecord[index].GetComponent<RectTransform>();
        if(rect.gameObject.activeInHierarchy)
        {
            rect.DOScaleY(0, 0.3f).OnComplete(()=>
            {
                rect.gameObject.SetActive(false);
                
            });

            var contentSize = content.sizeDelta;
            contentSize.y -= 300f;
            content.sizeDelta = contentSize;
            for (int i = index + 1; i < recordList.Count; i++)
            {
                PopUpMovement.Instance.RecordMove(recordList[i].gameObject, 1);
            }
            
        }
        else
        {
            rect.localScale = new Vector3(1, 0, 0);
            rect.gameObject.SetActive(true);
            rect.DOScaleY(1, 0.3f);
            var contentSize = content.sizeDelta;
            contentSize.y += 300f;
            content.sizeDelta = contentSize; 
            for (int i = index + 1; i < recordList.Count; i++)
            {
                PopUpMovement.Instance.RecordMove(recordList[i].gameObject, -1);
            }
            
        }
    }
    void ResetListAndChildInContent()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        recordList.Clear();
        inforInRecord.Clear();
    }
    public void SetListRecord()
    {
        ResetListAndChildInContent();
        var numberRecord = PlayerData.numberRecord;
        for(int i = 0; i < numberRecord; i++)
        {
            var contentSize = content.sizeDelta;
            contentSize.y += 200f; 
            content.sizeDelta = contentSize;

            var recordIns = Instantiate(recordPrefab);
            var recordRect = recordIns.GetComponent<RectTransform>();
            var record = recordIns.GetComponent<RecordInHistory>();

            recordRect.SetParent(content);
            recordRect.localScale = Vector3.one;
            recordRect.anchoredPosition = new Vector3(0, -(i) * 200f ,0);


            record.recordDay = PlayerData.GetRecordDay(i);
            record.recordName = PlayerData.GetRecordName(i);
            record.recordLength = PlayerData.GetRecordLength(i);

            record.SetTextInRecord();

            recordList.Add(recordIns);
        }
        SetClickedBtn();
    }

    void SetClickedBtn()
    {
        for (int i = 0; i < recordList.Count; i++)
        {
            inforInRecord.Add(recordList[i].GetComponent<RectTransform>().GetChild(1).gameObject);
        }
        for (int i = 0; i < recordList.Count; i++)
        {
            int index = i;
            recordList[index].GetComponent<Button>().onClick.AddListener(() =>
            {
                recordList[index].ResetButtonAndImage();
                OnRecordClicked(index);
            });
            recordList[index].eraseBtn.onClick.AddListener(() =>
            {
                ButtonInHistoryRecord.Instance.OnEraseBtnInHistoryRecord(index);
            });
            recordList[index].shareBtn.onClick.AddListener(() =>
            {
                ButtonInHistoryRecord.Instance.OnShareBtnInHistoryRecord(index);
            });
            recordList[index].playBtn.onClick.AddListener(() =>
            {
                ButtonInHistoryRecord.Instance.OnPlayBtnInHistoryRecord(index);
                recordList[index].stopBtn.gameObject.SetActive(true);
                recordList[index].playBtn.gameObject.SetActive(false);
                recordList[index].isPlaying = true;
                recordList[index].length = PlayerData.GetRecordLength(index);
                recordList[index].timePlay = 0f;

            });
            recordList[index].stopBtn.onClick.AddListener(() =>
            {
                recordList[index].stopBtn.gameObject.SetActive(false);
                recordList[index].playBtn.gameObject.SetActive(true);
                recordList[index].isPlaying = false;
                SoundController.Instance.source.Stop();
            });
        }
    }

}
