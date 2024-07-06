using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator playerAnimator;
    private StateMachine stateMachine = new StateMachine();

    private float horizontal;
    private float vertical;
    private Vector3 aim;
    private Quaternion playerRotation;
    private Transform playerTransform;
    void Start()
    {
        playerAnimator = this.gameObject.GetComponent<Animator>();

        stateMachine.RegisterState(new PlayerStateStanding(stateMachine, this));
        stateMachine.RegisterState(new PlayerStateWalking(stateMachine, this)); 
        stateMachine.RegisterState(new PlayerStateRunning(stateMachine, this));    

        // 初期ステートの設定
        stateMachine.ChangeState(StateType.STANDING.ToString());

        playerTransform = GetComponent<Transform> ();
        playerRotation = playerTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {        
        // ステートを更新
        stateMachine.Update();

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        var horizontalRotation = Quaternion.AngleAxis(Camera.main. transform.eulerAngles.y, Vector3.up);
        aim = horizontalRotation * new Vector3(horizontal, 0, vertical).normalized;
        
        if (aim.magnitude > 0.5f)
        {
            playerRotation = Quaternion.LookRotation(aim, Vector3.up);
        }    
        playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, playerRotation, 600 * Time.deltaTime);
    }
}
