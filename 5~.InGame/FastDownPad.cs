using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class FastDownPad : MonoBehaviour
{
    public GameObject postProcessVolume;
    LensDistortion _speedEffect;

    float speedDecreaseAmount = 5f; // �ӵ� ���ҷ�
    float decreaseDuration = 3f;    // �ӵ� ���� ���� �ð�
    //float animationSpeedFactor = 2f; // �ִϸ��̼� �ӵ� ����

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
                if (_AudioManager.instance != null)
                {
                    _AudioManager.instance.PlaySfx(_AudioManager.Sfx.���ǵ����ȿ����);
                }

                playerMovement.FastSpeed(speedDecreaseAmount, decreaseDuration);
                playerMovement.GetComponent<Animator>().SetBool("booster", true);

                StartCoroutine(speedCam(0.01f, 0.005f));
                StartCoroutine(speedEffect(1f , 0.01f));
                //playerMovement.SetRunAnimationSpeed(animationSpeedFactor);
                StartCoroutine(ResetAnimationSpeedAfterDelay(playerMovement, decreaseDuration));
            }
        }
    }

    private IEnumerator ResetAnimationSpeedAfterDelay(PlayerMovement playerMovement, float delay)
    {
        yield return new WaitForSeconds(delay);
        playerMovement.SetRunAnimationSpeed("booster"); // �ִϸ��̼� �ӵ��� ������� ����
    }

    float CamSpeedEfffectVal = -50; //ī�޶� �ְ� ��ġ
    IEnumerator speedEffect(float value , float Interval)
    {
        // ������
        while ( _speedEffect.intensity.value > CamSpeedEfffectVal && playerMovement.GetComponent<Animator>().GetBool("booster"))
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
