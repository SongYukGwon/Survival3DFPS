using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeapon : MonoBehaviour
{
    public string closeWeaponName; // �������� �̸�

    //���� ����
    public bool isAxe;
    public bool isPickaxe;
    public bool isHand;

    public float range; //���ݹ���
    public int damage; //���ݷ�.
    public float workSpeed; // �۾� �ӵ�
    public float attackDelay; //���� ������
    public float attackDealyA; // ���� Ȱ��ȭ ����.
    public float attackDealyB; // ���� ��Ȱ��ȭ ����.

    public Animator anim; // �ִϸ��̼�.
    


}
