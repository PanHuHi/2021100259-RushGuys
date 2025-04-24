using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroCamera : MonoBehaviour
{
    public GameObject FadeImage;

    public GameObject MainCamera;
    public GameObject[] Cam = new GameObject[2];

    void Update()
    {
        if (Input.anyKey && GameManager.Instance.status == GameManager.GameStatus.��Ʈ��)
        {
            MainCamera.SetActive(true);
            Cam[0].SetActive(false);
            Cam[1].SetActive(false);
            GameManager.Instance.status = GameManager.GameStatus.����;
            GameManager.Instance.Ready();
        }
    }

    IEnumerator Start()
    {
        if(GameManager.Instance.status == GameManager.GameStatus.��Ʈ��)
        {
            FadeImage.SetActive(true);
            StartCoroutine(fadeIn());
            for (int i = 0; i < Cam.Length; i++)
            {
                if (GameManager.Instance.status == GameManager.GameStatus.��Ʈ��)
                {
                    Cam[i].SetActive(true);
                    for (int Q = 0; Q < 400; Q++)
                    {
                        Cam[i].transform.eulerAngles += new Vector3(0, -0.1f, 0);
                        yield return new WaitForSecondsRealtime(0.01f);
                    }
                }
            }
            if (GameManager.Instance.status == GameManager.GameStatus.��Ʈ��)
            {
                StartCoroutine(fadeOut());
                yield return new WaitForSecondsRealtime(2);
                MainCamera.SetActive(true);
                Cam[0].SetActive(false);
                Cam[1].SetActive(false);
                FadeImage.SetActive(false);
                //���� �޴������� ���ӷ��� ���۽�Ű��
                GameManager.Instance.Ready();
            }
            yield return null;
        }
    }

    IEnumerator fadeIn()
    {
        FadeImage.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        while (FadeImage.GetComponent<Image>().color.a > 0)
        {
            FadeImage.GetComponent<Image>().color -= new Color32(0, 0, 0, 5);
            yield return null;
        }
        FadeImage.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        yield return null;
    }
    IEnumerator fadeOut()
    {
        FadeImage.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        while (FadeImage.GetComponent<Image>().color.a < 255)
        {
            FadeImage.GetComponent<Image>().color += new Color32(0, 0, 0, 5);
            yield return null;
        }
        FadeImage.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        yield return null;
    }
}
