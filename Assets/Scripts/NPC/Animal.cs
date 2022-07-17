using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] protected string animalName; //������ �̸�
    [SerializeField] protected int hp; // ������ ü��.

    [SerializeField] protected float walkSpeed; //�ȱ� ���ǵ�
    [SerializeField] protected float runSpeed; //�ȱ� ���ǵ�
    [SerializeField] protected float turnningSpeed; //ȸ�� ���ǵ�
    protected float applySpeed;

    protected Vector3 direction; //����

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
                ReSet();//���� ���� �ൿ �Խ�
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
