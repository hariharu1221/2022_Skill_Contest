using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject Icon;

    public ControllInputAction input;
    private InputAction movement;
    private InputAction lowFlying;
    private InputAction boosting;
    private InputAction rotation;

    private bool isReset;

    private void Awake()
    {
        SetVariable();
        SetKeyInput();
    }

    private void SetVariable()
    {
        input = new ControllInputAction();

        movement = input.InputAction_Player.Movement;
        lowFlying = input.InputAction_Player.LowFlying;
        boosting = input.InputAction_Player.Boosting;
        rotation = input.InputAction_Player.Rotation;

        isReset = true;
    }

    private void SetKeyInput()
    {
        //LowFlying KeySetting
        lowFlying.started += ctx => Player.Instance.lowFlying = ExcutionState.ready;
        lowFlying.canceled += ctx => Player.Instance.lowFlying = ExcutionState.end;

        //Boosting KeySetting
        boosting.started += ctx => Player.Instance.boosting = ExcutionState.ready;
        boosting.canceled += ctx => Player.Instance.boosting = ExcutionState.end;

        //Rotation mouse
        rotation.started += ctx => isReset = !isReset;
    }

    Vector3 targetPos = Vector3.zero;
    public Vector3 GetTargetPos(bool isReset = false)
    {
        if (isReset)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit rayhit))
            {
                targetPos = rayhit.point;
            }
        }
        return targetPos;
    }

    public Quaternion GetTargetToRot(Vector3 origin, bool isReset)
    {
        Vector3 target = GetTargetPos(isReset);
        Vector3 dir = target - origin;
        dir.y = 0;
        return Quaternion.LookRotation(dir);
    }

    private void Update()
    {
        Player.Instance.vec = movement.ReadValue<Vector2>();
        Player.Instance.rot = GetTargetToRot(Player.Instance.transform.position, isReset);
        Icon.transform.position = GetTargetPos(false);
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
