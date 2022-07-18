using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    [SerializeField] protected string animalName; //������ �̸�
    [SerializeField] protected int hp; // ������ ü��.

    [SerializeField] protected float walkSpeed; //�ȱ� ���ǵ�
    [SerializeField] protected float runSpeed; //�ȱ� ���ǵ�

    protected Vector3 destination; //������

    //���º���
    protected bool isWalking; // �ȴ��� �� �ȴ��� �Ǻ�.
    protected bool isAction; // �ൿ������ �ƴ��� �Ǻ� 
    protected bool isRunning;
    protected bool isDead = false;

    [SerializeField] protected float walkTime; //�ȱ� �ð�
    [SerializeField] protected float waitTime; //��� �ð�
    [SerializeField] protected float runTime; //�ٴ� �ð�
    protected float currentTime;


    //�ʿ��� ������Ʈ
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
                ReSet();//���� ���� �ൿ �Խ�
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
        Debug.Log("�ȱ�");
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
