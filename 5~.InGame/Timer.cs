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
    bool isRunning = true; // Ÿ�̸Ӱ� ���� ������ ���θ� ��Ÿ���� ����

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
        // Ÿ�̸� �ʱ� ����
        timerText.text = "00:00.00";

        Debug.Log(PlayerPrefs.GetInt(MapNum + "BastTime") +" " + "���� �� ����� �� Ȯ��");
        Bast_minutes= PlayerPrefs.GetInt(MapNum + "Bast_minutes");
        Bast_seconds= PlayerPrefs.GetInt(MapNum + "Bast_seconds");
        Bast_hundredths= PlayerPrefs.GetInt(MapNum + "Bast_hundredths");

    }

    // Update�� �� �����Ӹ��� ȣ��˴ϴ�.
    void Update()
    {
        if (GameManager.Instance.status == GameManager.GameStatus.����)
        {
            if (!isRunning)
            {
                return; // Ÿ�̸Ӱ� ���� ���� �ƴϸ� Update�� ����
            }
            elapsedTime += Time.deltaTime;

            // ��, ��, �׸��� 0.01�� ������ ����մϴ�.
            minutes = Mathf.FloorToInt(elapsedTime / 60);
            seconds = Mathf.FloorToInt(elapsedTime % 60);
            hundredths = Mathf.FloorToInt((elapsedTime * 100) % 100);

            // �ؽ�Ʈ ������ "MM:SS.SS"�� �����մϴ�.
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
        }
    }
    public void StopTimer()
    {
        Debug.Log(PlayerPrefs.HasKey(MapNum + "BastTime") + " " + "�ְ� ����");
        isRunning = false;
        //�� ����
        if (PlayerPrefs.GetInt(MapNum + "BastTime") == 1)
        {
            Debug.Log(PlayerPrefs.HasKey(MapNum + "BastTime") + " " + "�轺Ʈ�� ����� �ִٸ�");
            //����Ʈ ���� ������ �� �� �轺Ʈ ���� ���
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
            Debug.Log(PlayerPrefs.HasKey(MapNum + "BastTime") + " " + "�轺Ʈ�� ����� ����");
            //����Ʈ ���� ������ ����Ʈ���� ����
            saveTimer();
            //�ְ� ��� �ؽ�Ʈ ���
            BastTextTime();
            Debug.Log(7);
        }
    }
    void saveTimer() //����Ʈ �� ���
    {
        PlayerPrefs.SetInt(MapNum + "Bast_minutes", minutes);
        PlayerPrefs.SetInt(MapNum + "Bast_seconds", seconds);
        PlayerPrefs.SetInt(MapNum + "Bast_hundredths", hundredths);
        Debug.Log(PlayerPrefs.HasKey(MapNum + "BastTime") + " " + "����!");

    }

    void TextTime()
    {
        g_BastTime.SetActive(true);
        g_MiniMap.SetActive(false);
        g_Time.SetActive(true);
        g_Time_Image.SetActive(false);
        g_bast_Text.SetActive(false);
        BastTime.text = string.Format("�轺Ʈ ��" + " " + "{0:00}:{1:00}:{2:00}", Bast_minutes, Bast_seconds, Bast_hundredths);
        T_Time.text = string.Format("���� ��" + " " + "{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
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
        BastTime.text = string.Format("�轺Ʈ ��" + " " + "{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
        timerText.text = "".ToString();
    }
}
