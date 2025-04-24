using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : Item
{
    public GameObject minimap_Key;
    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾�� �浹�ߴ��� Ȯ�� (�±׸� ���)
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                if (_AudioManager.instance != null)
                {
                    _AudioManager.instance.PlaySfx(_AudioManager.Sfx.����ȹ��ȿ����);
                }
                GameManager.Instance.key += 1;
                minimap_Key.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }
}
