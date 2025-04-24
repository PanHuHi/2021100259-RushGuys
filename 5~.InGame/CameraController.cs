using System.Collections;
using System.Collections.Generic;
using System.Net;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public GameObject endCam;

    public float Yaxis;
    public float Xaxis;

   GameObject Player;
    public Transform target;//Player

    private float rotSensitive = 3f;//카메라 회전 감도
    public float dis;//카메라와 플레이어사이의 거리
    private float RotationMin = 10f;//카메라 회전각도 최소
    private float RotationMax = 80f;//카메라 회전각도 최대
    private float smoothTime = 0.12f;//카메라가 회전하는데 걸리는 시간

    private Vector3 targetRotation;
    private Vector3 currentVel;

    public bool hill;
    public bool isOnIce;

    public Vector3 offset;      // 카메라와 플레이어 사이의 거리
    void Awake()
    {
        instance = this;
        Xaxis = 0;
    }
    GameObject playerCharacter_Clone;

    public void gameStartCam()
    {
        dis = 8;
        hill = false;
        isOnIce = false;

        playerCharacter_Clone = GameObject.Find("PlayerCharacter(Clone)");
        for (int i = 0; i< playerCharacter_Clone.transform.childCount; i++)
        {
            if (playerCharacter_Clone.transform.GetChild(i).gameObject.activeSelf)
            {
                Player = playerCharacter_Clone.transform.GetChild(i).gameObject;
            }
        }
        target = Player.transform;

        // offset = transform.position - Player.transform.position;

        Xaxis = 10;

        Cursor.visible = false;//마우스 커서 안 보이게 해준다.
        Cursor.lockState = CursorLockMode.Locked;//마우스 위치를 기본 위치에서 움직이지 않도록, 

        //Cursor.visible = true;//마우스 커서 표시해줌.
        //Cursor.lockState = CursorLockMode.None; // 커서를 움직이게 함
    }
    float PlayerX;
    float PlayerY;
    float PlayerZ;

    float high = 0; // 높이 
    void LateUpdate()//Player가 움직이고 그 후 카메라가 따라가야 하므로 LateUpdate
    {
        if (GameManager.Instance.status != GameManager.GameStatus.인트로)
        {
            //Yaxis = Yaxis + Input.GetAxis("Mouse X") * rotSensitive;//마우스 좌우움직임을 입력받아서 카메라의 Y축을 회전시킨다
             //Xaxis = Xaxis - Input.GetAxis("Mouse Y") * rotSensitive;//마우스 상하움직임을 입력받아서 카메라의 X축을 회전시킨다
            //Xaxis는 마우스를 아래로 했을때(음수값이 입력 받아질때) 값이 더해져야 카메라가 아래로 회전한다 

            //Yaxis = Yaxis + Input.GetAxis("Horizontal")/5 * rotSensitive;

           // Yaxis = Player.transform.rotation.y;
             //Xaxis = Mathf.Clamp(Xaxis, RotationMin, RotationMax);
             //X축회전이 한계치를 넘지않게 제한해준다.

             //targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis, Yaxis), ref currentVel, smoothTime);
             //this.transform.eulerAngles = targetRotation;
             //SmoothDamp를 통해 부드러운 카메라 회전
            
             //transform.position = target.position - transform.forward * dis;
            //카메라의 위치는 플레이어보다 설정한 값만큼 떨어져있게 계속 변경된다.
            transform.position = new Vector3(target.position.x, target.position.y + high , target.position.z) - transform.forward * dis;

            //transform.position = new Vector3(Player.transform.position.x , Player.transform.position.y, Player.transform.position.z + offset.z);
            //transform.rotation = Quaternion.Euler(targetRotation);
            // transform.LookAt(Player.transform.position);

            /* Vector3 dir = target.position - this.transform.position;
             Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.deltaTime, dir.y * cameraSpeed * Time.deltaTime, 0.0f);
             this.transform.Translate(moveVector);
            */
            PlayerX = Player.transform.eulerAngles.x;
            PlayerY = Player.transform.eulerAngles.y;
            PlayerZ = Player.transform.eulerAngles.z;


            if (hill)
            {
                transform.eulerAngles = new Vector3(-20, PlayerY, PlayerZ - PlayerZ);
            }

            if (isOnIce)
            {
                transform.eulerAngles = new Vector3(30, PlayerY, PlayerZ - PlayerZ);
            }

            if (!isOnIce && !hill)
            {
                transform.eulerAngles = new Vector3(10, PlayerY, PlayerZ - PlayerZ);
            }
        }
    }
    public void endCamPos()
    {
        gameObject.GetComponent<AudioListener>().enabled = false;
        endCam.SetActive(true);
        endCam.transform.position = new Vector3(target.position.x-2.5f, target.position.y+4, target.position.z) + transform.forward * (dis+20);
        endCam.transform.eulerAngles = new Vector3(-10, PlayerY+200, PlayerZ- PlayerZ);
    }
   
}
