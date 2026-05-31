using UnityEngine;
using Unity.Entities;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class UserInputSystem : ComponentSystem
{
    private EntityQuery _inputQuery;
    private InputAction _moveAction;
    private InputAction _shootAction;
    private InputAction _jerkAction;

    private float2 _moveInput;
    private float _shootInput;
    private float _jerkInput;



    protected override void OnCreate()
    {
        _inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
    }
    protected override void OnStartRunning()
    {
        _moveAction = new InputAction("move", binding: "<GamePad>/rightStick");
        _moveAction.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w>")
            .With("Down", "<Keyboard>/s>")
            .With("Left", "<Keyboard>/a>")
            .With("Right", "<Keyboard>/d>");

        _moveAction.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.started += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.canceled += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.Enable();

        _shootAction = new InputAction("shoot", binding: "<Keyboard>/space");

        _shootAction.performed += context => { _shootInput = 1f; };
        _shootAction.started += context => { _shootInput = 1f; };
        _shootAction.canceled += context => { _shootInput = 0f; };
        _shootAction.Enable();


        _jerkAction = new InputAction("jerk", binding: "<Keyboard>/leftCtrl");
        _jerkAction.started += context => { _jerkInput = 1f; };
        _jerkAction.performed -= context => { _jerkInput = 1f; };
        _jerkAction.canceled += context => { _jerkInput = 0f; };
        _jerkAction.Enable();
    }
    protected override void OnUpdate()
    {
        Entities.With(_inputQuery).ForEach((Entity entity, ref InputData inputData) =>
        {
            inputData.move = _moveInput;
            inputData.shoot = _shootInput;
            inputData.jerk = _jerkInput;
        });
    }

    protected override void OnStopRunning()
    {
        _moveAction.Disable();
        _shootAction.Disable();
        _jerkAction.Disable();
    }
}
