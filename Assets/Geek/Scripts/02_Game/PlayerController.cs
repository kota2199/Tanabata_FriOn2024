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
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseMode();

        //KeyInput
        Vector2 moveInput = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        //カメラが向いている方向を取得
        Transform camVector = cam.transform;

        // 移動方向を計算
        Vector3 movement = camVector.forward * moveInput.x + camVector.right * moveInput.y;
        movement = movement.normalized * walkSpeed * Time.deltaTime;

        // Rigidbodyを用いて移動

        if (!playerLocker.playerLock)
        {
            rigid.MovePosition(rigid.position + movement);
        }

        //RotatePlayer
        float mouseX = Input.GetAxis("Mouse X");

        float rotationX = mouseX * rotateSpeed * Time.deltaTime;

        //transform.Rotate(Vector3.up, rotationX);
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
