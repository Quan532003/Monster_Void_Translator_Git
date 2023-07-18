using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Slider slider;
    public void Load(int scene)
    {
        slider.gameObject.SetActive(true);
        StartCoroutine(loadScene(scene));
    }
    bool start = false;

    private void Update()
    {
        if(!start)
        {
            Load(1);
            start = true;
        }
    }

    IEnumerator loadScene(int scene)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene);

        while (!loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
