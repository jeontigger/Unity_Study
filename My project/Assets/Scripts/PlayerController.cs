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
    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
    }

    //GameObject (Player)
    // Transform
    // PlayerController (*)
    float _yAngle = 0.0f;
    void Update()
    {
        _yAngle += Time.deltaTime * _speed;
        //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);
        //transform.Rotate(new Vector3(0.0f, _yAngle, 0.0f));

        //transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));

    }
    void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.3f);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.3f);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.3f);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.3f);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
    }
}

