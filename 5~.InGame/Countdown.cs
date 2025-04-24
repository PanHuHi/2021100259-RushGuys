using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public static Countdown instance;

    public TMP_Text countdownText;
    public float countdownDuration = 3f;

     void Awake()
    {
        instance = this;
    }
    private void Start()
    {
       // StartCoroutine(ActivateCountdownTextAfterDelay(1f));
    }

    public IEnumerator ActivateCountdownTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (_AudioManager.instance != null)
        {
            _AudioManager.instance.PlaySfx(_AudioManager.Sfx.ī��Ʈ�ٿ�ȿ����);
        }
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(true); // �ؽ�Ʈ Ȱ��ȭ

        // ī��Ʈ�ٿ� ����
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {

        countdownText.gameObject.SetActive(true); // �ؽ�Ʈ Ȱ��ȭ


        int count = 3;
        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }

        GameManager.Instance.status = GameManager.GameStatus.����;
        GameManager.Instance.GameStart();

        countdownText.text = "Rush!";
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);

    }
}
