using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 위치벡터
// 2. 방향벡터
// struct MyVector
// {
//     public float x;
//     public float y;
//     public float z;

//     public float magnitude { get { return Mathf.Sqrt(x * x + y * y + z * z); } }
//     public MyVector normalized { get { return new MyVector(x / magnitude, y / magnitude, z / magnitude); } }

//     public MyVector(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
//     public static MyVector operator +(MyVector a, MyVector b)
//     {
//         return new MyVector(a.x + b.x, a.y + b.y, a.z + b.z);
//     }
//     public static MyVector operator -(MyVector a, MyVector b)
//     {
//         return new MyVector(a.x - b.x, a.y - b.y, a.z - b.z);
//     }
//     public static MyVector operator *(MyVector a, float d)
//     {
//         return new MyVector(a.x * d, a.y * d, a.z * d);
//     }
// }
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    Vector3 _destPos;
    void Start()
    {
        // Managers.Input.KeyAction -= OnKeyboard;
        // Managers.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;



    }

    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
    }
    PlayerState _state = PlayerState.Idle;


    void UpdateDie()
    {

    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position = transform.position + dir.normalized * moveDist;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);

        }
        Animator anim = GetComponent<Animator>();
        // 현재 게임 상태에 대한 정보를 넘겨준다
        anim.SetFloat("speed", _speed);
    }

    void UpdateIdle()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }
    //GameObject (Player)
    // Transform
    // PlayerController (*)
    void Update()
    {
        //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);
        //transform.Rotate(new Vector3(0.0f, _yAngle, 0.0f));

        //transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));

        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
        }


    }
    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die)
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;
            //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}!");
        }

    }
    // void OnKeyboard()
    // {
    //     if (Input.GetKey(KeyCode.W))
    //     {
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.3f);
    //         transform.position += Vector3.forward * Time.deltaTime * _speed;
    //     }
    //     if (Input.GetKey(KeyCode.S))
    //     {
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.3f);
    //         transform.position += Vector3.back * Time.deltaTime * _speed;
    //     }
    //     if (Input.GetKey(KeyCode.A))
    //     {
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.3f);
    //         transform.position += Vector3.left * Time.deltaTime * _speed;
    //     }
    //     if (Input.GetKey(KeyCode.D))
    //     {
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.3f);
    //         transform.position += Vector3.right * Time.deltaTime * _speed;
    //     }
    //     _moveToDest = false;
    // }
}

