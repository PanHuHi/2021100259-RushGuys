using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SlowDownPad : MonoBehaviour
{
    public GameObject postProcessVolume;
    LensDistortion _speedEffect;

    float speedDecreaseAmount = 5f; // �ӵ� ���ҷ�
    float decreaseDuration = 3f;    // �ӵ� ���� ���� �ð�
    float animationSpeedFactor = 0.5f; // �ִϸ��̼� �ӵ� ���� ����

    PlayerMovement playerMovement;
    void Start()
    {
        if (postProcessVolume == null)
        {
            Debug.Log("����Ʈ���μ��� null");
            return;
        }
        postProcessVolume.GetComponent<PostProcessVolume>().profile.TryGetSettings(out _speedEffect);
    }
    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾�� �浹�ߴ��� Ȯ�� (�±׸� ���)
        if (other.CompareTag("Player"))
        {
            playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.DecreaseSpeed(speedDecreaseAmount, decreaseDuration);
                playerMovement.GetComponent<Animator>().SetBool("booster", false);

                StartCoroutine(speedEffect(1f, 0.01f));
                StartCoroutine(speedCam(0.01f, 0.01f));
                // playerMovement.SetRunAnimationSpeed(animationSpeedFactor);
                Debug.Log("�̼Ӱ��� �� �ִϸ��̼� �ӵ� ����");
                StartCoroutine(ResetAnimationSpeedAfterDelay(playerMovement, decreaseDuration));
            }
        }
    }

    private IEnumerator ResetAnimationSpeedAfterDelay(PlayerMovement playerMovement, float delay)
    {
        yield return new WaitForSeconds(delay);
        playerMovement.SetRunAnimationSpeed("Slow"); // �ִϸ��̼� �ӵ��� ������� ����
    }

    float CamSpeedEfffectVal = -50; //ī�޶� �ְ� ��ġ
    IEnumerator speedEffect(float value, float Interval)
    {
        while (_speedEffect.intensity.value > CamSpeedEfffectVal && playerMovement.GetComponent<Animator>().GetBool("booster"))
        {
            _speedEffect.intensity.value -= value;
            yield return new WaitForSeconds(Interval);
        }
        if (playerMovement.GetComponent<Animator>().GetBool("booster"))
        {
            yield return new WaitForSeconds(decreaseDuration);
        }
        while (_speedEffect.intensity.value < 0 && !playerMovement.GetComponent<Animator>().GetBool("booster"))
        {
            _speedEffect.intensity.value += value;
            yield return new WaitForSeconds(Interval);
        }
        yield return null;
    }
    float CamSpeedEfffectDisVal = 7;
    IEnumerator speedCam(float value, float Interval)
    {
        while (CameraController.instance.dis < CamSpeedEfffectDisVal && playerMovement.GetComponent<Animator>().GetBool("booster"))
        {
            CameraController.instance.dis += value;
            yield return new WaitForSeconds(Interval);
        }
        if (playerMovement.GetComponent<Animator>().GetBool("booster"))
        {
            yield return new WaitForSeconds(decreaseDuration);
        }
        while (CameraController.instance.dis > 5 && !playerMovement.GetComponent<Animator>().GetBool("booster"))
        {
            CameraController.instance.dis -= value;
            yield return new WaitForSeconds(Interval);
        }
    }
}
