using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : CloseWeaponController
{
    //Ȱ��ȭ ����
    public static bool isActivate = true;


    private void Start()
    {
        WeaponManager.currentWeapon = currentWeapon.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentWeapon.anim;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivate)
            TryAttack();
    }
    protected override IEnumerator HitCoroutine()
    {
        while(isSwing)
        {
            if(CheckObject())
            {
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }

    public override void CloseWeaponChange(CloseWeapon _hand)
    {
        base.CloseWeaponChange(_hand);
        isActivate = true;
    }
}