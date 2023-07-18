using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopUpController : MonoBehaviour
{
    public Button settingBtn;
    public Button closeBtn;
    public Button vibrationBtn;
    public Button noAdBtn;
    public Button rateBtn;
    public GameObject settingPopUp;
    public GameObject ratePopUp;
    public GameObject vibrationActive;
    public GameObject vibrationInActive;
    public GameObject noAdActive;
    public GameObject noAdInActive;
    private void Awake()
    {
        settingBtn.onClick.AddListener(OnSettingBtn);
        closeBtn.onClick.AddListener(OnCloseBtn);
        vibrationBtn.onClick.AddListener(OnVibrationBtnClicked);
        noAdBtn.onClick.AddListener(OnNoAdBtnClicked);
        rateBtn.onClick.AddListener(OnRateBtnClicked);

        if(PlayerData.vibration == 1)
        {
            vibrationActive.SetActive(true);
            vibrationInActive.SetActive(false);
        }
        else
        {
            vibrationInActive.SetActive(true);
            vibrationActive.SetActive(false);
        }

        if(PlayerData.noAD == 1)
        {
            noAdActive.SetActive(true);
            noAdInActive.SetActive(false);
        }
        else
        {
            noAdActive.SetActive(false) ;
            noAdInActive.SetActive(true);
        }
    }
    public void OnSettingBtn()
    {
        PopUpMovement.Instance.ShowPopUp(settingPopUp);
    }
    public void OnCloseBtn()
    {
        PopUpMovement.Instance.HidePopUp(settingPopUp);
    }
    public void OnVibrationBtnClicked()
    {
        PlayerData.vibration = 1 - PlayerData.vibration;
        int vibra = PlayerData.vibration;
        if(vibra == 0)
        {
            vibrationInActive.SetActive(true);
            vibrationActive.SetActive(false);
        }
        else
        {
            vibrationActive.SetActive(true);
            vibrationInActive.SetActive(false);
        }
    }
    public void OnNoAdBtnClicked()
    {
        PlayerData.noAD = 1 - PlayerData.noAD;
        if(PlayerData.noAD == 0)
        {
            noAdInActive.SetActive(true);
            noAdActive.SetActive(false);
        }
        else
        {
            noAdActive.SetActive(true);
            noAdInActive.SetActive(false);
        }
    }
    public void OnRateBtnClicked()
    {
        PopUpMovement.Instance.ShowPopUp(ratePopUp);
    }
}