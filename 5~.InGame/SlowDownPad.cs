using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SlowDownPad : MonoBehaviour
{
    public GameObject postProcessVolume;
    LensDistortion _speedEffect;

    float speedDecreaseAmount = 5f; // 속도 감소량
    float decreaseDuration = 3f;    // 속도 감소 지속 시간
    float animationSpeedFactor = 0.5f; // 애니메이션 속도 감소 비율

    PlayerMovement playerMovement;
    void Start()
    {
        if (postProcessVolume == null)
        {
            Debug.Log("포스트프로세싱 null");
            return;
        }
        postProcessVolume.GetComponent<PostProcessVolume>().profile.TryGetSettings(out _speedEffect);
    }
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와 충돌했는지 확인 (태그를 사용)
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
                Debug.Log("이속감소 및 애니메이션 속도 감소");
                StartCoroutine(ResetAnimationSpeedAfterDelay(playerMovement, decreaseDuration));
            }
        }
    }

    private IEnumerator ResetAnimationSpeedAfterDelay(PlayerMovement playerMovement, float delay)
    {
        yield return new WaitForSeconds(delay);
        playerMovement.SetRunAnimationSpeed("Slow"); // 애니메이션 속도를 원래대로 복원
    }

    float CamSpeedEfffectVal = -50; //카메라 왜곡 수치
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
