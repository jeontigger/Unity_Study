using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
public class PlayerController : BaseController
{



    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    PlayerStat _stat;
    bool _stopSkill = false;


    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        _stat = gameObject.GetComponent<PlayerStat>();
        // Managers.Input.KeyAction -= OnKeyboard;
        // Managers.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpace<UI_HPBar>(transform);

    }


    protected override void UpdateMoving()
    {

        // 몬스터가 내 사정거리보다 가까우면 공격
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= 1)
            {
                State = Define.State.Skill;
                return;
            }
        }
        Vector3 dir = _destPos - transform.position;
        dir.y = 0;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            //transform.position = transform.position + dir.normalized * moveDist;
            Debug.DrawRay(transform.position, dir, Color.red);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (!Input.GetMouseButton(0))
                    State = Define.State.Idle;
                return;
            }


            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);

        }
        // 현재 게임 상태에 대한 정보를 넘겨준다

    }

    protected override void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat);
        }

        if (_stopSkill)
            State = Define.State.Idle;
        else
            State = Define.State.Skill;

    }
    //GameObject (Player)
    // Transform
    // PlayerController (*)
    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:
                if (evt == Define.MouseEvent.PointerUp)
                    _stopSkill = true;
                break;
        }
    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        State = Define.State.Moving;
                        _stopSkill = false;
                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;
                    }
                }
                break;
            case Define.MouseEvent.Press:
                if (_lockTarget == null && raycastHit)
                    _destPos = hit.point;
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
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

