using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlller : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCamearaRotationX = 0;



    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;



    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {
        CameraRotation();
        Move();
        CharacterRotation();
    }

    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCamearaRotationX -= _cameraRotationX;
        currentCamearaRotationX = Mathf.Clamp(currentCamearaRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCamearaRotationX, 0f, 0f);
    }

    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        //100
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        // 001
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        // 101 합계가 2인것을 합계를 1로 바꿔줌 0.5 0 0.5 1초에 이동하는 값을 일정화시키는 것
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);


        
    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");

        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;

        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }
}
