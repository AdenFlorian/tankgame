using System.Collections;
using UnityEngine;

public class InputMaster : MonoBehaviour
{
    private void Awake()
    {
    }

    private void Start()
    {
    }

    private void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) {
        }
    }
}