using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpInforController : MonoBehaviour
{
    public RectTransform contentInfor;
    List<Button> monsterSelect = new List<Button>();
    public GameObject monsterInforPopUp;
    public GameObject monsterSelectPopUp;

    public Text monsterName;
    public Text monsterNickName;
    public Text monsterGender;
    public Text monsterBirthDay;
    public Text monsterOrigin;
    public Text monsterFeature;

    public List<Sprite> avatar = new List<Sprite>();
    public List<Sprite> cardAvatar = new List<Sprite>();

    public Button closeBtn;
    public Text textInTittle;

    public RectTransform contentInShowInfor;
    public List<MonsterInforSO> monsterInfor = new List<MonsterInforSO>();

    public Image monsterAvatar;
    List<GameObject> lockInfor = new List<GameObject>();
    public static PopUpInforController Instance;


    [SerializeField] List<Text> monsterNameInSceneSelect = new List<Text>();
    [SerializeField] List<Image> avataMonsterInSeclect = new List<Image>();
    private void Awake()
    {
        Instance = this;
        for(int i = 0; i < contentInfor.childCount; i++)
        {
            monsterSelect.Add(contentInfor.GetChild(i).GetComponent<Button>());
            monsterNameInSceneSelect.Add(monsterSelect[i].GetComponent<RectTransform>().GetChild(0).GetComponent<Text>());
            avataMonsterInSeclect.Add(monsterSelect[i].GetComponent<RectTransform>().GetChild(1).GetComponent<Image>());
        }
        for(int i = 0; i < monsterNameInSceneSelect.Count; i++)
        {
            monsterNameInSceneSelect[i].text = Helper.monsterName[i];
            avataMonsterInSeclect[i].sprite = cardAvatar[i];
        }
        for(int i = 0; i < monsterSelect.Count; i++)
        {
            lockInfor.Add(monsterSelect[i].GetComponent<RectTransform>().GetChild(2).gameObject);
        }
        SetLockBtn();
        SetBtnMonsterClicked();
        closeBtn.onClick.AddListener(CloseBtnClicked);
    }

    public void SetLockBtn()
    {
        for(int i =0; i < lockInfor.Count; i++)
        {
            lockInfor[i].SetActive(false);
        }
        var index = Helper.ConvertFromMonsterLockToInt();
        for (int i = 0; i < index.Count; i++)
        {
            lockInfor[index[i]].SetActive(true);
        }
    }
    public void SetBtnMonsterClicked()
    {
        for (int i = 0; i < monsterSelect.Count; i++)
        {
            int index = i;
            monsterSelect[index].onClick.AddListener(() =>
            {
                OnSelectMonsterBtnClicked(index);
            });
        }
    }
    void OnSelectMonsterBtnClicked(int index)
    {
        if (!lockInfor[index].activeInHierarchy)
        {
            monsterSelectPopUp.SetActive(false);
            monsterInforPopUp.SetActive(true);
            var anchorPos = contentInShowInfor.anchoredPosition;
            anchorPos.y = 0;
            contentInShowInfor.anchoredPosition = anchorPos;
            SetTextAndAvatar(index);
        }
        else
        {
            lockInfor[index].SetActive(false);
            Helper.SetLockMonster(index);
            monsterSelectPopUp.SetActive(false);
            monsterInforPopUp.SetActive(true); 
            var anchorPos = contentInShowInfor.anchoredPosition;
            anchorPos.y = 0;
            contentInShowInfor.anchoredPosition = anchorPos;
            SetTextAndAvatar(index);
        }
    }

    public void SetTextAndAvatar(int index)
    {
        monsterName.text = monsterInfor[index].monsterName;
        monsterNickName.text = monsterInfor[index].nickName;
        monsterBirthDay.text = monsterInfor[index].birthDay;
        monsterOrigin.text = monsterInfor[index].Origin;
        monsterGender.text = monsterInfor[index].gender;
        monsterFeature.text = monsterInfor[index].featuresAndStory;
        textInTittle.text = monsterInfor[index].monsterName;
        var contentSize = contentInShowInfor.sizeDelta;
        contentSize.y = monsterInfor[index].contentHeight * 1f;
        contentInShowInfor.sizeDelta = contentSize;
        monsterAvatar.sprite = avatar[index];
    }

    public void CloseBtnClicked()
    {
        monsterSelectPopUp.SetActive(true);
        monsterInforPopUp.SetActive(false);
    }
}
