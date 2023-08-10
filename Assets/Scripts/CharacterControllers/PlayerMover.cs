using System;
using GameInputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterControllers
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        private GameInput _gameInput;

        private CharacterController _characterController;
        [SerializeField] private float gravity = 1.0f;
        [SerializeField] private Vector3 moveDir;
        [SerializeField] private float rightMovePower = 1.0f;

        private void Start()
        {
            //CharacterControllerを取得
            _characterController = this.GetComponent<CharacterController>();

            //InputActionを取得（GameInputはInputActionをソースコード化したもの）
            _gameInput = new GameInput();

            _gameInput.Player.Jump.started += OnJump;

            _gameInput.Player.Move.started += OnMove;
            _gameInput.Player.Move.performed += OnMove;
            _gameInput.Player.Move.canceled += OnMove;

            _gameInput.Enable();
        }

        private void OnMove(InputAction.CallbackContext obj)
        {
            var value = obj.ReadValue<Vector2>();
            Debug.Log(value);
            moveDir.x = value.x * rightMovePower;
        }

        private void OnDestroy()
        {
            _gameInput.Disable();
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            this._characterController.Move(Vector3.up);
            moveDir = Vector3.up;
        }

        private void Update()
        {
            moveDir.y -= 1.0f * Time.deltaTime;
            this._characterController.Move(moveDir * Time.deltaTime);
        }
    }
}