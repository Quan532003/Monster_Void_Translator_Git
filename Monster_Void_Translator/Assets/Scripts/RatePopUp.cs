using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatePopUp : MonoBehaviour
{
    public List<Button> brownStars;
    public List<Button> yellowStars;
    public Button rateBtn;
    public Button laterBtn;
    public GameObject rateActive;
    public GameObject rateInActive;
    private void Awake()
    {
        if(PlayerData.rate == 0)
        {
            rateActive.SetActive(false) ;
            rateInActive.SetActive(true);
        }
        else
        {
            rateInActive.SetActive(false);
            rateActive.SetActive(true);
        }

        for(int i = 0; i < brownStars.Count; i++)
        {
            int index = i;
            brownStars[index].onClick.AddListener(() =>
            {
                OnBrownStarBtnClicked(index);
            });
            yellowStars[index].onClick.AddListener(() =>
            {
                OnBrownStarBtnClicked(index);
            });
        }
        laterBtn.onClick.AddListener(OnLaterBtnClicked);
        rateBtn.onClick.AddListener(OnRateBtnClicked);
    }
    void OnBrownStarBtnClicked(int index)
    {
        for(int i = 0; i <= index; i++)
        {
            brownStars[i].gameObject.SetActive(false);
            yellowStars[i].gameObject.SetActive(true);
        }
        for(int i = index + 1; i < brownStars.Count; i++)
        {
            brownStars[i].gameObject.SetActive(true);
            yellowStars[i].gameObject.SetActive(false);
        }
    }
    void OnLaterBtnClicked()
    {
        PopUpMovement.Instance.HidePopUp(this.gameObject);
    }
    void OnRateBtnClicked()
    {
        //link ra app
        PopUpMovement.Instance.HidePopUp(this.gameObject);
        if(PlayerData.rate == 0)
        {
            PlayerData.rate = 1;
            rateActive.SetActive(true);
            rateInActive.SetActive(false);
        }
    }
}
