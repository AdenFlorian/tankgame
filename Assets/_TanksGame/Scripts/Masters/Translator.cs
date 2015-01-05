using System;
using System.Collections;
using UnityEngine;

public class Translator : MonoBehaviour
{
    private Action KeyHoldW = Actions.TankMoveForwardAction;
    private Action KeyHoldS = Actions.TankMoveBackwardAction;
    private Action KeyHoldA = Actions.TankTurnLeftAction;
    private Action KeyHoldD = Actions.TankTurnRightAction;
    private Action MouseDown0 = Actions.TankFireAction;
    private Action MouseXAxis = Actions.TankLookHorizontalAction;
    private Action MouseYAxis = Actions.TankLookVerticalAction;

    private void Awake()
    {
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) && KeyHoldW != null) {
            KeyHoldW.Invoke();
        }
        if (Input.GetKey(KeyCode.S) && KeyHoldS != null) {
            KeyHoldS.Invoke();
        }
        if (Input.GetKey(KeyCode.A) && KeyHoldA != null) {
            KeyHoldA.Invoke();
        }
        if (Input.GetKey(KeyCode.D) && KeyHoldD != null) {
            KeyHoldD.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && MouseDown0 != null) {
            MouseDown0.Invoke();
        }

        // Axes (Ranges)
        if (MouseXAxis != null) {
            MouseXAxis.Invoke();
        }
        if (MouseYAxis != null) {
            MouseYAxis.Invoke();
        }
    }
}
