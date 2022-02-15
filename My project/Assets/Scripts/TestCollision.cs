using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{

    // 1. 나한테 RigidBody가 있어야 함(IsKinematic: Off)
    // 2. 둘 다 Collider가 있어야 함(IsTrigger: Off)
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision! @ {collision.gameObject.name} !");
    }
    // 1. 둘 다 Collider가 있어야 함
    // 2. 둘 중 하나는 ISTrigger : On
    // 3. 둘 중 하나는 RigidBody가 있어야 함
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger! {other.gameObject.name} !");
    }
    void Start()
    {

    }
    void Update()
    {
        // Local <-> World <-> (Viewport <-> Screen) (화면)
        // Debug.Log(Input.mousePosition); // Screen 좌표계
        // Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition)); // Viewport 좌표계

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            // Vector3 dir = mousePos - Camera.main.transform.position;
            // dir = dir.normalized;

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Monster") + LayerMask.GetMask("Wall");
            //int mask = (1 << 8) | (1 << 9);

            if (Physics.Raycast(ray, out hit, 100.0f, mask))
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}!");
        }
    }
}
