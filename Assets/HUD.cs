using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HUD : MonoBehaviour
{
    //�ʿ��� ������Ʈ
    [SerializeField]
    private GunController theGunController;
    private Gun currentGun;


    //�ʿ��ϸ� HUD ȣ�� �ʿ������ ��Ȱ��ȭ
    [SerializeField]
    private GameObject go_BullerHUD;

    //�Ѿ� ���� �ؽ�Ʈ ���� �ݿ�
    [SerializeField]
    private TextMeshProUGUI[] text_Bullet;

    // Update is called once per frame
    void Update()
    {
        CheckBullet();
    }

    private void CheckBullet()
    {
        currentGun = theGunController.GetGun();
        text_Bullet[0].text = currentGun.carryBulletCount.ToString();
        text_Bullet[1].text = currentGun.reloadBulletCount.ToString();
        text_Bullet[2].text = currentGun.currentBulletCount.ToString();
    }
}