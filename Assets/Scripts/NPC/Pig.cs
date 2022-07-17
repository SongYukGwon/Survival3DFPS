using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    [SerializeField] private string animalName; //������ �̸�
    [SerializeField] private int hp; // ������ ü��.

    [SerializeField] private float walkSpeed; //�ȱ� ���ǵ�
    [SerializeField] private float runSpeed; //�ȱ� ���ǵ�
    private float applySpeed;

    private Vector3 direction; //����

    //���º���
    private bool isWalking; // �ȴ��� �� �ȴ��� �Ǻ�.
    private bool isAction; // �ൿ������ �ƴ��� �Ǻ� 
    private bool isRunning; // �ൿ������ �ƴ��� �Ǻ� 

    [SerializeField] private float walkTime; //�ȱ� �ð�
    [SerializeField] private float waitTime; //��� �ð�
    [SerializeField] private float runTime; //�ٴ� �ð�
    private float currentTime;


    //�ʿ��� ������Ʈ
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider boxCol;

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

    private void Rotation()
    {
        if(isWalking || isRunning)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, direction, 0.01f);
            rigid.MoveRotation(Quaternion.Euler(_rotation));
        }
    }

    private void Move()
    {
        if(isWalking )
        {
            rigid.MovePosition(transform.position + (transform.forward * walkSpeed * Time.deltaTime));
        }
        else if(isRunning)
        {
            rigid.MovePosition(transform.position + (transform.forward * runSpeed * Time.deltaTime));
        }
    }

    private void ElapseTime()
    {
        if(isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                ReSet();//���� ���� �ൿ �Խ�
        }
    }

    private void ReSet()
    {
        isWalking = false;
        isRunning = false;
        isAction = true;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        direction.Set(0f, Random.Range(0f, 360f), 0f);
        RandomAction();
    }

    private void RandomAction()
    {

        int _random = Random.Range(0, 4); // ���, Ǯ���, �θ���, �ȱ�

        if (_random == 0)
            Wait();
        else if (_random == 1)
            Eat();
        else if (_random == 2)
            Peek();
        else if (_random == 3)
            TryWalk();
    }

    private void Wait()
    {
        currentTime = waitTime;
        Debug.Log("���");
    }
    private void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
        Debug.Log("Ǯ���");
    }
    private void Peek()
    {
        currentTime = waitTime;
        anim.SetTrigger("Peek");
        Debug.Log("�θ���");
    }
    public void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", true);
        currentTime = walkTime;
        Debug.Log("�ȱ�");
    }

    public void Run(Vector3 _targetPos)
    {
        direction = Quaternion.LookRotation(transform.position - _targetPos).eulerAngles;
        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        applySpeed = runSpeed;
        anim.SetBool("Running", isRunning);
    }
}
