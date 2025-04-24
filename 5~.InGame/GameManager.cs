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

    public enum GameStatus{ 인트로 ,레디, 시작, 끝};
    public GameStatus status;
    void Awake()
    {
        status = GameStatus.인트로;
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
            _AudioManager.instance.PlayBgm(_AudioManager.Bgm.봄bgm);
            return;
        }
        if (SceneManager.GetActiveScene().name == "Map_2")
        {
            _AudioManager.instance.PlayBgm(_AudioManager.Bgm.여름bgm);
            return;
        }
        if (SceneManager.GetActiveScene().name == "Map_3")
        {
            _AudioManager.instance.PlayBgm(_AudioManager.Bgm.가을bgm);
            return;
        }
        if (SceneManager.GetActiveScene().name == "Map_4")
        {
            _AudioManager.instance.PlayBgm(_AudioManager.Bgm.겨울bgm);
            return;
        }
    }
    void Start()
    {
        Debug.Log("게임 시작");
    }

    public void Ready()
    {
        status = GameStatus.레디;
        CameraController.instance.gameStartCam();
        InGameUI.SetActive(true);

        //카운트 다운 시작
        Countdown.instance.StartCoroutine(Countdown.instance.ActivateCountdownTextAfterDelay(1f));


    }
    public void GameStart()
    {
        status = GameStatus.시작;
        //캐릭터 무브먼트 스크립트 활성화
        CharacterScriptControl.instance.GetComponent<PlayerMovement>().enabled = true;
        CharacterScriptControl.instance.GetComponent<PlayerAnimation>().enabled = false;
        
        //타이머 작동
    }
    public void GameFinish()
    {
        status = GameStatus.끝;
        _AudioManager.instance.transform.Find("BgmObject").GetComponent<AudioSource>().Pause();
        if (_AudioManager.instance != null)
        {
            _AudioManager.instance.PlayBgm(_AudioManager.Bgm.완주bgm);
        }
        //완주 이벤트
        Timer.instance.StopTimer();
        StartCoroutine(endGame());
        Debug.Log("게임 종료");
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
        //카메라
        CameraController.instance.endCamPos();
        //랩 기록
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
