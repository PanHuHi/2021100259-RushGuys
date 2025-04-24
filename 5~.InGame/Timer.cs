using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    [SerializeField]
    TextMeshProUGUI timerText;

    [SerializeField]
    GameObject g_BastTime;

    [SerializeField]
    GameObject g_Time;

    [SerializeField]
    GameObject g_Time_Image;

    [SerializeField]
    GameObject g_bast_Text;

   public GameObject g_MiniMap;

    TextMeshProUGUI BastTime;
    TextMeshProUGUI T_Time;
    float elapsedTime;
    bool isRunning = true; // 타이머가 실행 중인지 여부를 나타내는 변수

    int Bast_minutes;
    int Bast_seconds;
    int Bast_hundredths;

    int minutes;
    int seconds;
    int hundredths;

    string MapNum;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        MapNum = SceneManager.GetActiveScene().name;
        Debug.Log(MapNum);

        g_BastTime.SetActive(false);
        g_MiniMap.SetActive(true);
        BastTime = g_BastTime.GetComponent<TextMeshProUGUI>();
        T_Time = g_Time.GetComponent<TextMeshProUGUI>();
        // 타이머 초기 설정
        timerText.text = "00:00.00";

        Debug.Log(PlayerPrefs.GetInt(MapNum + "BastTime") +" " + "시작 전 저장된 값 확인");
        Bast_minutes= PlayerPrefs.GetInt(MapNum + "Bast_minutes");
        Bast_seconds= PlayerPrefs.GetInt(MapNum + "Bast_seconds");
        Bast_hundredths= PlayerPrefs.GetInt(MapNum + "Bast_hundredths");

    }

    // Update는 매 프레임마다 호출됩니다.
    void Update()
    {
        if (GameManager.Instance.status == GameManager.GameStatus.시작)
        {
            if (!isRunning)
            {
                return; // 타이머가 실행 중이 아니면 Update를 중지
            }
            elapsedTime += Time.deltaTime;

            // 분, 초, 그리고 0.01초 단위를 계산합니다.
            minutes = Mathf.FloorToInt(elapsedTime / 60);
            seconds = Mathf.FloorToInt(elapsedTime % 60);
            hundredths = Mathf.FloorToInt((elapsedTime * 100) % 100);

            // 텍스트 형식을 "MM:SS.SS"로 설정합니다.
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
        }
    }
    public void StopTimer()
    {
        Debug.Log(PlayerPrefs.HasKey(MapNum + "BastTime") + " " + "최고랩 여부");
        isRunning = false;
        //값 저장
        if (PlayerPrefs.GetInt(MapNum + "BastTime") == 1)
        {
            Debug.Log(PlayerPrefs.HasKey(MapNum + "BastTime") + " " + "배스트랩 기록이 있다링");
            //베스트 랩이 있으면 비교 후 배스트 랩에 기록
            if (minutes < Bast_minutes)
            {
                saveTimer();
                BastTextTime();
                Debug.Log(0);
            }
            else if (minutes > Bast_minutes)
            {
                TextTime();
                Debug.Log(1);
            }
            else if (minutes == Bast_minutes && seconds < Bast_seconds)
            {
                saveTimer();
                BastTextTime();
                Debug.Log(2);
            }
            else if (seconds > Bast_seconds)
            {
                TextTime();
                Debug.Log(3);
            }
            else if (minutes == Bast_minutes && seconds == Bast_seconds && hundredths < Bast_hundredths)
            {
                saveTimer();
                BastTextTime();
                Debug.Log(4);
            }
            else if (minutes == Bast_minutes && seconds == Bast_seconds && hundredths > Bast_hundredths)
            {
                TextTime();
                Debug.Log(5);
            }
            else if (minutes == Bast_minutes && seconds == Bast_seconds && hundredths == Bast_hundredths)
            {
                saveTimer();
                BastTextTime();
                Debug.Log(6);
            }
        }
        if(!PlayerPrefs.HasKey(MapNum + "BastTime"))
        {
            Debug.Log(PlayerPrefs.HasKey(MapNum + "BastTime") + " " + "배스트랩 기록이 없다");
            //베스트 랩이 없으면 베스트랩에 저장
            saveTimer();
            //최고 기록 텍스트 출력
            BastTextTime();
            Debug.Log(7);
        }
    }
    void saveTimer() //베스트 랩 기록
    {
        PlayerPrefs.SetInt(MapNum + "Bast_minutes", minutes);
        PlayerPrefs.SetInt(MapNum + "Bast_seconds", seconds);
        PlayerPrefs.SetInt(MapNum + "Bast_hundredths", hundredths);
        Debug.Log(PlayerPrefs.HasKey(MapNum + "BastTime") + " " + "저장!");

    }

    void TextTime()
    {
        g_BastTime.SetActive(true);
        g_MiniMap.SetActive(false);
        g_Time.SetActive(true);
        g_Time_Image.SetActive(false);
        g_bast_Text.SetActive(false);
        BastTime.text = string.Format("배스트 랩" + " " + "{0:00}:{1:00}:{2:00}", Bast_minutes, Bast_seconds, Bast_hundredths);
        T_Time.text = string.Format("현재 랩" + " " + "{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
        timerText.text = "".ToString();
    }
    void BastTextTime()
    {
        PlayerPrefs.SetInt(MapNum + "BastTime", 1);
        g_MiniMap.SetActive(false);
        g_BastTime.SetActive(true);
        g_Time.SetActive(false);
        g_Time_Image.SetActive(false);
        g_bast_Text.SetActive(true);
        BastTime.text = string.Format("배스트 랩" + " " + "{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
        timerText.text = "".ToString();
    }
}
