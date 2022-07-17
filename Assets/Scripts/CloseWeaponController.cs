using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�̿ϼ� Ŭ���� = �߻�Ŭ����
public abstract class CloseWeaponController : MonoBehaviour
{
    
    //���� ������ Hand�� Ÿ�� ����.
    [SerializeField]
    protected CloseWeapon currentWeapon;

    //������??
    protected bool isAttack = false;
    protected bool isSwing = false;

    protected RaycastHit hitInfo;

    protected void TryAttack()
    {
        if(!Inventory.inventoryActivated)
        {
            if (Input.GetButton("Fire1"))
            {
                if (!isAttack)
                {
                    //�ڷ�ƾ ����
                    StartCoroutine(AttackCoroutine());
                }
            }
        }
    }

    protected IEnumerator AttackCoroutine()
    {
        isAttack = true;
        currentWeapon.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentWeapon.attackDealyA);
        isSwing = true;

        //����Ȱ��ȭ ����.
        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentWeapon.attackDealyB);
        isSwing = false;

        yield return new WaitForSeconds(currentWeapon.attackDelay - currentWeapon.attackDealyA - currentWeapon.attackDealyB);
        isAttack = false;
    }


    //�̿ϼ� = �߻� �ڷ�ƾ
    protected abstract IEnumerator HitCoroutine();

    protected bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentWeapon.range))
        {
            return true;
        }
        return false;
    }


    //���� �Լ� - �ϼ��Լ� ������ �߰� ������ �Լ�
    public virtual void CloseWeaponChange(CloseWeapon _hand)
    {
        if (WeaponManager.currentWeapon != null)
            WeaponManager.currentWeapon.gameObject.SetActive(false);

        currentWeapon = _hand;
        WeaponManager.currentWeapon = currentWeapon.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentWeapon.anim;

        currentWeapon.transform.localPosition = Vector3.zero;

        currentWeapon.gameObject.SetActive(true);
    }
}
