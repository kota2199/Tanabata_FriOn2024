using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigid;

    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float horizontalWalkSpeed;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private StageSwitcher playerLocker;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        playerLocker = GetComponent<StageSwitcher>();

        cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseMode();

        //Move
        Vector2 moveInput = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        // 移動方向を計算
        Vector3 movement = transform.forward * moveInput.x + transform.right * moveInput.y;
        movement = movement.normalized * walkSpeed * Time.deltaTime;

        // Rigidbodyを用いて移動

        if (!playerLocker.playerLock)
        {
            rigid.MovePosition(rigid.position + movement);
        }

        //RotatePlayer
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotationX = mouseX * rotateSpeed * Time.deltaTime;
        float rotationY = mouseY * rotateSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, rotationX);

        //RotateCamera
        //cam.transform.Rotate(Vector3.right, -rotationY * (MouseSensitivityController.instance.mouseSense * 10f));
    }

    private void UpdateMouseMode()
    {
        if (MenuController.instance.onMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
