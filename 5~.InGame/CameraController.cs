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

    private float rotSensitive = 3f;//ī�޶� ȸ�� ����
    public float dis;//ī�޶�� �÷��̾������ �Ÿ�
    private float RotationMin = 10f;//ī�޶� ȸ������ �ּ�
    private float RotationMax = 80f;//ī�޶� ȸ������ �ִ�
    private float smoothTime = 0.12f;//ī�޶� ȸ���ϴµ� �ɸ��� �ð�

    private Vector3 targetRotation;
    private Vector3 currentVel;

    public bool hill;
    public bool isOnIce;

    public Vector3 offset;      // ī�޶�� �÷��̾� ������ �Ÿ�
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

        Cursor.visible = false;//���콺 Ŀ�� �� ���̰� ���ش�.
        Cursor.lockState = CursorLockMode.Locked;//���콺 ��ġ�� �⺻ ��ġ���� �������� �ʵ���, 

        //Cursor.visible = true;//���콺 Ŀ�� ǥ������.
        //Cursor.lockState = CursorLockMode.None; // Ŀ���� �����̰� ��
    }
    float PlayerX;
    float PlayerY;
    float PlayerZ;

    float high = 0; // ���� 
    void LateUpdate()//Player�� �����̰� �� �� ī�޶� ���󰡾� �ϹǷ� LateUpdate
    {
        if (GameManager.Instance.status != GameManager.GameStatus.��Ʈ��)
        {
            //Yaxis = Yaxis + Input.GetAxis("Mouse X") * rotSensitive;//���콺 �¿�������� �Է¹޾Ƽ� ī�޶��� Y���� ȸ����Ų��
             //Xaxis = Xaxis - Input.GetAxis("Mouse Y") * rotSensitive;//���콺 ���Ͽ������� �Է¹޾Ƽ� ī�޶��� X���� ȸ����Ų��
            //Xaxis�� ���콺�� �Ʒ��� ������(�������� �Է� �޾�����) ���� �������� ī�޶� �Ʒ��� ȸ���Ѵ� 

            //Yaxis = Yaxis + Input.GetAxis("Horizontal")/5 * rotSensitive;

           // Yaxis = Player.transform.rotation.y;
             //Xaxis = Mathf.Clamp(Xaxis, RotationMin, RotationMax);
             //X��ȸ���� �Ѱ�ġ�� �����ʰ� �������ش�.

             //targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis, Yaxis), ref currentVel, smoothTime);
             //this.transform.eulerAngles = targetRotation;
             //SmoothDamp�� ���� �ε巯�� ī�޶� ȸ��
            
             //transform.position = target.position - transform.forward * dis;
            //ī�޶��� ��ġ�� �÷��̾�� ������ ����ŭ �������ְ� ��� ����ȴ�.
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
