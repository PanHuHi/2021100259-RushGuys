using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscSetting : MonoBehaviour
{
    public static EscSetting esc_Setting;

    public Button MainButton;

    public GameObject fadeImage;

    public GameObject _EscSetting;

    void Awake()
    {
        esc_Setting = this;
    }
    void Start()
    {
        MainButton.onClick.AddListener(Go_Main);
        _EscSetting.gameObject.SetActive(false);
    }

    void Update()
    {
        if (GameManager.Instance.status != GameManager.GameStatus.¿Œ∆Æ∑Œ)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!_EscSetting.gameObject.activeSelf)
                {
                    Time.timeScale = 0;
                    _AudioManager.instance.transform.Find("BgmObject").GetComponent<AudioSource>().Pause();
                    _AudioManager.instance.transform.Find("SfxObject").GetComponent<AudioSource>().Pause();
                    _EscSetting.gameObject.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Time.timeScale = 1;
                    _AudioManager.instance.transform.Find("BgmObject").GetComponent<AudioSource>().Play();
                    _AudioManager.instance.transform.Find("SfxObject").GetComponent<AudioSource>().Play();
                    _EscSetting.gameObject.SetActive(false);
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }
  
   public void Go_Main()
    {
        StartCoroutine(go_main());
    }
    IEnumerator go_main()
    {
        fadeImage.SetActive(true);
        fadeImage.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        while (fadeImage.GetComponent<Image>().color.a < 2)
        {
            fadeImage.GetComponent<Image>().color += new Color32(0, 0, 0, 1);
            yield return null;
        }
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
