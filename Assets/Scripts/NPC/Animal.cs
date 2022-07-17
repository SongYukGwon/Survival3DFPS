using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] protected string animalName; //동물의 이름
    [SerializeField] protected int hp; // 동물의 체력.

    [SerializeField] protected float walkSpeed; //걷기 스피드
    [SerializeField] protected float runSpeed; //걷기 스피드
    [SerializeField] protected float turnningSpeed; //회전 스피드
    protected float applySpeed;

    protected Vector3 direction; //방향

    //상태변수
    protected bool isWalking; // 걷는지 안 걷는지 판별.
    protected bool isAction; // 행동중인지 아닌지 판별 
    protected bool isRunning;
    protected bool isDead = false;

    [SerializeField] protected float walkTime; //걷기 시간
    [SerializeField] protected float waitTime; //대기 시간
    [SerializeField] protected float runTime; //뛰는 시간
    protected float currentTime;


    //필요한 컴포넌트
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected BoxCollider boxCol;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotation();
        ElapseTime();
    }

    protected void Rotation()
    {
        if (isWalking || isRunning)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, direction.y, 0f), turnningSpeed);
            rigid.MoveRotation(Quaternion.Euler(_rotation));
        }
    }

    protected void Move()
    {
        if (isWalking)
        {
            rigid.MovePosition(transform.position + (transform.forward * walkSpeed * Time.deltaTime));
        }
        else if (isRunning)
        {
            rigid.MovePosition(transform.position + (transform.forward * runSpeed * Time.deltaTime));
        }
    }

    protected void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                ReSet();//다음 랜덤 행동 게시
        }
    }

    protected virtual void ReSet()
    {
        isWalking = false;
        isRunning = false;
        isAction = true;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        direction.Set(0f, Random.Range(0f, 360f), 0f);
    }


    protected void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", true);
        currentTime = walkTime;
        Debug.Log("걷기");
    }

    public virtual void Damage(int _dmg, Vector3 _targetPos)
    {
        if(!isDead)
        {
            hp -= _dmg;

            if(hp <= 0)
            {
                Dead();
                return;
            }
        }

        anim.SetTrigger("Hurt");

    }

    protected void Dead()
    {
        anim.SetTrigger("Dead");
    }
}
