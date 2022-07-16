using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    [SerializeField]
    private int hp; // 바위의 체력

    [SerializeField]
    private float destoryTime; //파편제거 시간

    [SerializeField]
    private SphereCollider col; //구체 콜라이더

    [SerializeField]
    private GameObject go_rock; //일반 바위.
    [SerializeField]
    private GameObject go_debris; //깨진 바위
    [SerializeField]
    private GameObject go_effect_prefabs; //채굴 이펙트

    //필요한 사운드 이름
    [SerializeField]
    private string strike_Sound;
    [SerializeField]
    private string destory_Sound;

    public void Mining()
    {
        SoundManager.instance.PlaySE(strike_Sound);
        var clone = Instantiate(go_effect_prefabs, col.bounds.center, Quaternion.identity);

        Destroy(clone, 3f);

        hp--;
        if (hp <= 0)
            Destruction();
    }

    private void Destruction()
    {
        SoundManager.instance.PlaySE(destory_Sound);
        col.enabled = false;
        Destroy(go_rock);

        go_debris.SetActive(true);

        Destroy(go_debris, destoryTime);
    }
}
