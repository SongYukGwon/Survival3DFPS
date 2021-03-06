using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//미완성 클래스 = 추상클래스
public abstract class CloseWeaponController : MonoBehaviour
{
    
    //현재 장착된 Hand형 타입 무기.
    [SerializeField]
    protected CloseWeapon currentWeapon;

    //공격중??
    protected bool isAttack = false;
    protected bool isSwing = false;

    protected RaycastHit hitInfo;
    [SerializeField]
    protected LayerMask layerMask;

    protected void TryAttack()
    {
        if(!Inventory.inventoryActivated)
        {
            if (Input.GetButton("Fire1"))
            {
                if (!isAttack)
                {
                    //코루틴 실행
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

        //공격활성화 시점.
        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentWeapon.attackDealyB);
        isSwing = false;

        yield return new WaitForSeconds(currentWeapon.attackDelay - currentWeapon.attackDealyA - currentWeapon.attackDealyB);
        isAttack = false;
    }


    //미완성 = 추상 코루틴
    protected abstract IEnumerator HitCoroutine();

    protected bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentWeapon.range, layerMask))
        {
            return true;
        }
        return false;
    }


    //가상 함수 - 완성함수 이지만 추가 편집한 함수
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
