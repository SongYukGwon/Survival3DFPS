using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    //���� ������ ��
    [SerializeField]
    private Gun currentGun;


    // ����ӵ� ���
    private float currentFireRate;


    //ȿ����
    private AudioSource audioSource;


    //���º���
    private bool isReload = false;
    [HideInInspector]
    public bool isFindSightMode = false;

    //���������ǰ�
    [SerializeField]
    private Vector3 originPos;

    //�浹 ���� �޾ƿ�.
    private RaycastHit hitInfo;

    //�ʿ��� ������Ʈ
    [SerializeField]
    private Camera theCam;

    //�ǰ� ����Ʈ
    [SerializeField]
    private GameObject hit_effect_prefab;


    private void Start()
    {
        originPos = Vector3.zero;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GunFireRateCalc();
        TryFire();
        TryReload();
        TryFindSight();
    }

    //����ӵ� ����
    private void GunFireRateCalc()
    {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime; //1�ʿ� 1�� ���ҽ�Ų��.


    }

    //�߻�õ�
    private void TryFire()
    {
        if(Input.GetButton("Fire1") && currentFireRate <= 0 && !isReload)
        {
            Fire();
        }
    }

    //�߻��� ���
    private void Fire()
    {
        if (!isReload)
        {
            if (currentGun.currentBulletCount > 0)
            {
                Shoot();
            }
            else
            {
                CancelFineSight();
                StartCoroutine(ReloadCouroutine());
            }
        }
    }

    //�߻��� ���
    private void Shoot()
    {
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate; // ����ӵ� ����
        PlaySE(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play();
        Hit();
        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());
    }

    private void Hit()
    {
        if (Physics.Raycast(theCam.transform.position, theCam.transform.forward, out hitInfo, currentGun.range))
        {
            GameObject clone = Instantiate(hit_effect_prefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(clone, 2);

        }
    }

    //������ �õ�
    private void TryReload()
    {
        if(Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.currentBulletCount < currentGun.reloadBulletCount)
        {
            CancelFineSight();
            StartCoroutine(ReloadCouroutine());
        }
    }

    //������
    IEnumerator ReloadCouroutine()
    {
        if(currentGun.carryBulletCount > 0)
        {
            isReload = true;
            currentGun.anim.SetTrigger("Reload");

            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;

            yield return new WaitForSeconds(currentGun.reloadTime);

            if(currentGun.carryBulletCount >= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;
            }
            else
            {
                currentGun.currentBulletCount = currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
            }

            isReload = false;
        }
        else
        {
            Debug.Log("������ �Ѿ��� �����ϴ�");
        }
    }

    //������ �õ�
    private void TryFindSight()
    {
        if(Input.GetButtonDown("Fire2") && !isReload)
        {
            FineSight();
        }
    }

    //������ ���
    public void CancelFineSight()
    {
        if (isFindSightMode)
            FineSight();
    }

    //������ ��������
    private void FineSight()
    {
        isFindSightMode = !isFindSightMode;
        currentGun.anim.SetBool("FineSightMode", isFindSightMode);

        if(isFindSightMode)
        {
            StopAllCoroutines();
            StartCoroutine(FindSightActivateCoroutine());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(FindSightDeactivateCoroutine());
        }
    }

    //������Ȱ��ȭ
    IEnumerator FindSightActivateCoroutine()
    {
        while(currentGun.transform.localPosition != currentGun.findSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.findSightOriginPos, 0.2f);
            yield return null;
        }
    }

    //�����غ�Ȱ��ȭ
    IEnumerator FindSightDeactivateCoroutine()
    {
        while(currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.2f);
            yield return null;
        }
    }

    //�ݵ� �ڷ�ƾ
    IEnumerator RetroActionCoroutine()
    {
        Vector3 recoilBack = new Vector3(currentGun.retroActionForce, originPos.y, originPos.z);
        Vector3 retroActionRecoilBack = new Vector3(currentGun.retroActionFineSightForce, currentGun.findSightOriginPos.y, currentGun.findSightOriginPos.z);

        if(!isFindSightMode)
        {
            currentGun.transform.localPosition = originPos;

            //�ݵ� ����
            while(currentGun.transform.localPosition.x <= currentGun.retroActionForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f);
                yield return null;
            }

            //����ġ

            while(currentGun.transform.localPosition != originPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
                yield return null;
            }
        }
        else
        {
            currentGun.transform.localPosition = currentGun.findSightOriginPos;

            //�ݵ� ����
            while (currentGun.transform.localPosition.x <= currentGun.retroActionFineSightForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                yield return null;
            }

            //����ġ

            while (currentGun.transform.localPosition != currentGun.findSightOriginPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.findSightOriginPos, 0.1f);
                yield return null;
            }
        }

    }


    //���� ���
    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }


    public Gun GetGun()
    {
        return currentGun;
    }
}
