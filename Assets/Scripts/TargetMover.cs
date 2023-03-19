using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    MeshDrawer meshDrawer;
    [SerializeField] private DynamicJoystick joystick;
    Rigidbody rigidbody;
    Collider collider;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        meshDrawer = FindObjectOfType<MeshDrawer>();
        collider = GetComponent<Collider>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (meshDrawer.isDrawing)
        {
            Vector3 zMove = Vector3.forward *2;
            float x = Mathf.Lerp(transform.position.x,MousePos().x,Time.deltaTime*5);
            float xAmonut = 3f;
            Vector3 xMove = Vector3.right* joystick.Horizontal * xAmonut;
            rigidbody.velocity = zMove + xMove;
        }
        else
        {
            collider.isTrigger = true;
            if (meshDrawer.positions.Count <= 0)
            {
                rigidbody.velocity = Vector3.zero;
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, meshDrawer.positions[meshDrawer.positions.Count - 1], Time.deltaTime *8);
            
            if (meshDrawer.positions[meshDrawer.positions.Count - 1] == transform.position)
            {
                meshDrawer.positions.RemoveAt(meshDrawer.positions.Count - 1);
                
                meshDrawer.DrawBackward();
            }

        }
    }
    Vector3 MousePos()
    {
        Vector3 vectorPos = Input.mousePosition + Vector3.forward * Camera.main.transform.position.y;
        return Camera.main.ScreenToWorldPoint(vectorPos);
    }
}
