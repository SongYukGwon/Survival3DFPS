using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    [SerializeField] protected string animalName; //동물의 이름
    [SerializeField] protected int hp; // 동물의 체력.

    [SerializeField] protected float walkSpeed; //걷기 스피드
    [SerializeField] protected float runSpeed; //걷기 스피드

    protected Vector3 destination; //목적지

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

    protected NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        currentTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ElapseTime();
    }

    protected void Move()
    {
        if (isWalking || isRunning)
        {
            //rigid.MovePosition(transform.position + (transform.forward * walkSpeed * Time.deltaTime));
            nav.SetDestination(transform.position + destination * 5f);
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
        nav.speed = walkSpeed;
        nav.ResetPath();
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        destination.Set(Random.Range(-0.2f, 0.2f),0f, Random.Range(0.5f, 1f));
    }


    protected void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", true);
        currentTime = walkTime;
        nav.speed = walkSpeed;
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
