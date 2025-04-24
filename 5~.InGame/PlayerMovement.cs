using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; // 플레이어 이동 속도
    public float rotationSpeed; // 플레이어 회전 속도
    public float acceleration; // 가속도
    public float deceleration; // 감속도
    public float jumpForce; // 점프 힘

    public LayerMask groundLayer; // 땅 레이어를 정의
    private bool isSpeedDecreased = false;
    private bool isSpeedFast = false;
    private float originalSpeed;
    bool isOnIce = false;

    private Rigidbody rb;
    private float currentSpeed;
    private bool isGrounded;
    private Animator animator;
    float iceSlidingFactor = 0.5f; // 빙판 위에서 미끄러지는 정도를 조절하는 변수

    public ParticleSystem Run_Particle;
    public ParticleSystem Speed_Particle;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        currentSpeed = 0f;
        originalSpeed = moveSpeed;  // 초기 이동 속도를 저장
    }

    public void DecreaseSpeed(float amount, float duration)
    {
        if (!isSpeedDecreased)
        {

            moveSpeed -= amount;
            isSpeedDecreased = true;
            Invoke("ResetSpeed", duration);
        }
    }
    public void FastSpeed(float amount, float duration)
    {
        if (!isSpeedFast)
        {
            StartCoroutine(SpeedEff());
            moveSpeed += amount;
            isSpeedFast = true;
            Invoke("ResetSpeed", duration);
        }
    }
    IEnumerator SpeedEff()
    {
        Speed_Particle.Play();
        yield return new WaitForSeconds(3);
        Speed_Particle.Stop();
    }

    private void ResetSpeed()
    {
        moveSpeed = originalSpeed;
        isSpeedDecreased = false;
        isSpeedFast = false;
    }

    // 애니메이션 속도를 조절하는 메서드
    // 특정 애니메이션 상태의 속도를 조절하는 메서드
    public void SetRunAnimationSpeed(string PadName)
    {
        if (PadName == "Slow")
        {
            Debug.Log("슬로우");
            return;
        }
        if (PadName == "booster")
        {
            animator.SetBool("booster", false);
            return;
        }
    }
    float groundCheckDistance = 1f;
    bool my_coroutine_is_jump = false;
    private void Update()
    {
        // 이동 처리
        if (GameManager.Instance.status != GameStatus.끝)
        {
            HandleMovementInput();
            // 점프 처리
            isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(jump());
            }
        }
        if (isGrounded && currentSpeed >= 1 && !Run_Particle.isPlaying)
        {
            Run_Particle.Play();
        } 
        else if (!isGrounded || currentSpeed < 1)
        {
            Run_Particle.Stop();
        }
        if (GameManager.Instance.status == GameStatus.끝)
        {
            StartCoroutine(run_particleStop());
        }
    }
    IEnumerator run_particleStop()
    {
        yield return new WaitForSeconds(3);
        Run_Particle.Stop();
    }
    IEnumerator jump()
    {
        if (isGrounded && !my_coroutine_is_jump)
        {
            Run_Particle.Stop();
            my_coroutine_is_jump = true;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("jump");
        }

        yield return new WaitForSecondsRealtime(1.3f);
        my_coroutine_is_jump = false;
    }


    private void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0f)
        {
            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        }
        // 빙판 여부에 따라 이동 방식 변경
        if (isOnIce)
        {
            IceMovement(horizontalInput, verticalInput);
        }
        else
        {
            // 속도 조절
            if (verticalInput > 0f)
            {
                currentSpeed += acceleration * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, 0f, moveSpeed);
                animator.SetBool("run", true);
            }
            else if (verticalInput < 0f)
            {
                if (currentSpeed > 0f)
                {
                    currentSpeed -= deceleration * 2 * Time.deltaTime;
                    currentSpeed = Mathf.Max(currentSpeed, 0f);
                }
                else if (currentSpeed <= 0f)
                {
                    currentSpeed += deceleration * 2 * Time.deltaTime;
                    currentSpeed = Mathf.Min(currentSpeed, 0f);
                }
                if (currentSpeed <= 0f)
                {
                    //currentSpeed -= (acceleration - 3) * Time.deltaTime;
                    currentSpeed -= acceleration * 4.5f * Time.deltaTime;
                    currentSpeed = Mathf.Clamp(currentSpeed, -moveSpeed, 0f);
                }
                animator.SetBool("run", true);
                //animator.SetBool("run", false);
            }
            else
            {
                if (currentSpeed > 0f)
                {
                    currentSpeed -= deceleration * Time.deltaTime;
                    currentSpeed = Mathf.Max(currentSpeed, 0f);
                }
                else if (currentSpeed < 0f)
                {
                    currentSpeed += deceleration * Time.deltaTime;
                    currentSpeed = Mathf.Min(currentSpeed, 0f);
                }
                else if (currentSpeed == 0f)
                {
                    animator.SetBool("run", false);
                }
            }

            // 이동
            Vector3 moveDirection = transform.forward * currentSpeed;
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        }

       
    }
    private void IceMovement(float horizontalInput, float verticalInput)
    {
        // IceMovement 함수로 빙판 위의 움직임 구현
        Vector3 moveDirection = transform.forward * moveSpeed * verticalInput * iceSlidingFactor;
        rb.AddForce(moveDirection * Time.deltaTime, ForceMode.VelocityChange);

        if (horizontalInput != 0f)
        {
            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime * iceSlidingFactor);
        }

        if (horizontalInput != 0f || verticalInput != 0f)
        {
            //animator.SetBool("run", true);
        }
        else
        {
           // animator.SetBool("run", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ice"))
        {
            isOnIce = true;
            CameraController.instance.isOnIce = true;
            animator.SetBool("Ice", true);
        }
        if (other.CompareTag("Hill"))
        {
            //카메라 회전
            CameraController.instance.hill = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ice"))
        {
            isOnIce = false;
            animator.SetBool("Ice", false);
            CameraController.instance.isOnIce = false;
        }
        if (other.CompareTag("Hill"))
        {
            //카메라 회전
            CameraController.instance.hill = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            if (_AudioManager.instance != null)
            {
                _AudioManager.instance.PlaySfx(_AudioManager.Sfx.장애물충돌효과음);
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            animator.SetBool("water", true);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("이동장애물"))
        {
            DecreaseSpeed(5,3);
        }
    }
   void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            animator.SetBool("water", false);
        }
    }

}
