using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : Item
{
    public GameObject minimap_Key;
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와 충돌했는지 확인 (태그를 사용)
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                if (_AudioManager.instance != null)
                {
                    _AudioManager.instance.PlaySfx(_AudioManager.Sfx.열쇠획득효과음);
                }
                GameManager.Instance.key += 1;
                minimap_Key.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }
}
