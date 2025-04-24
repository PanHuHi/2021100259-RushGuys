using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static Thry.AnimationParser;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject InGameUI;

    public GameObject g_Key;

    public GameObject[] Get_key = new GameObject[3];
    public int key = 0;

    public int Point;

    public int Rap;

    public enum GameStatus{ ��Ʈ�� ,����, ����, ��};
    public GameStatus status;
    void Awake()
    {
        status = GameStatus.��Ʈ��;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        InGameUI.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Map_1")
        {
            _AudioManager.instance.PlayBgm(_AudioManager.Bgm.��bgm);
            return;
        }
        if (SceneManager.GetActiveScene().name == "Map_2")
        {
            _AudioManager.instance.PlayBgm(_AudioManager.Bgm.����bgm);
            return;
        }
        if (SceneManager.GetActiveScene().name == "Map_3")
        {
            _AudioManager.instance.PlayBgm(_AudioManager.Bgm.����bgm);
            return;
        }
        if (SceneManager.GetActiveScene().name == "Map_4")
        {
            _AudioManager.instance.PlayBgm(_AudioManager.Bgm.�ܿ�bgm);
            return;
        }
    }
    void Start()
    {
        Debug.Log("���� ����");
    }

    public void Ready()
    {
        status = GameStatus.����;
        CameraController.instance.gameStartCam();
        InGameUI.SetActive(true);

        //ī��Ʈ �ٿ� ����
        Countdown.instance.StartCoroutine(Countdown.instance.ActivateCountdownTextAfterDelay(1f));


    }
    public void GameStart()
    {
        status = GameStatus.����;
        //ĳ���� �����Ʈ ��ũ��Ʈ Ȱ��ȭ
        CharacterScriptControl.instance.GetComponent<PlayerMovement>().enabled = true;
        CharacterScriptControl.instance.GetComponent<PlayerAnimation>().enabled = false;
        
        //Ÿ�̸� �۵�
    }
    public void GameFinish()
    {
        status = GameStatus.��;
        _AudioManager.instance.transform.Find("BgmObject").GetComponent<AudioSource>().Pause();
        if (_AudioManager.instance != null)
        {
            _AudioManager.instance.PlayBgm(_AudioManager.Bgm.����bgm);
        }
        //���� �̺�Ʈ
        Timer.instance.StopTimer();
        StartCoroutine(endGame());
        Debug.Log("���� ����");
        g_Key.SetActive(true);
        Debug.Log(key);
        if (key >0)
        {
            for (int i = 1; i < key+1; i++)
            {
                Get_key[i-1].SetActive(true);
            }
        }
        StartCoroutine(Go_Main());
    }
    IEnumerator endGame()
    {
        //ī�޶�
        CameraController.instance.endCamPos();
        //�� ���
        yield return new WaitForSecondsRealtime(0.5f);
        CharacterScriptControl.instance.GetComponent<PlayerMovement>().enabled = false;
        CharacterScriptControl.instance.GetComponent<PlayerAnimation>().enabled = true;
        CharacterScriptControl.instance.GetComponent<Animator>().SetBool("run",false);
    }

    IEnumerator Go_Main()
    {
        yield return new WaitForSecondsRealtime(10);
        _AudioManager.instance.transform.Find("BgmObject").GetComponent<AudioSource>().Pause();
        EscSetting.esc_Setting.Go_Main();
    }
}
