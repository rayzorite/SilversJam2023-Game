using System.Collections;
using Managers;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        private bool CanMove { get; set; } = true;
        public bool IsSprinting => canSprint && Input.GetKey(sprintKey);
        private bool ShouldJump => _characterController.isGrounded && Input.GetKeyDown(jumpKey);
        private bool ShouldCrouch => !_duringCrouchAnimation && _characterController.isGrounded && Input.GetKeyDown(crouchKey);
        
        [Header("Functional Options")]
        [Tooltip("Check if Player should Sprint or not")]
        [SerializeField] private bool canSprint = true;
        [Tooltip("Check if Player should Jump or not")]
        [SerializeField] private bool canJump = true;
        [Tooltip("Check if Player should Crouch or not")]
        [SerializeField] private bool canCrouch = true;
        [Tooltip("Check if Player should Head bob or not")]
        [SerializeField] private bool canHeadBob = true;
        [Tooltip("Check if Player should Slide on Slopes or not")]
        [SerializeField] private bool canSlideOnSlopes = true;

        [Header("Controls")]
        [Tooltip("Keyboard Key for Sprint Function")]
        [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
        [Tooltip("Keyboard Key for Jump Function")]
        [SerializeField] private KeyCode jumpKey = KeyCode.Space;
        [Tooltip("Keyboard Key for Crouch Function")]
        [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

        [Header("Camera Settings")]
        [Tooltip("This is Vertical Mouse Speed")]
        [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
        [Tooltip("This is Horizontal Mouse Speed")]
        [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
        [Tooltip("This is how high you can see")]
        [SerializeField, Range(1, 90)] private float upperLookLimit = 60.0f;
        [Tooltip("This is how low you can see")]
        [SerializeField, Range(1, 90)] private float lowerLookLimit = 45.0f;

        [Header("HeadBob Settings")]
        [Tooltip("Walking HeadBob Speed")]
        [SerializeField] private float walkBobSpeed = 14f;
        [Tooltip("Walking HeadBob Camera Movement")]
        [SerializeField] private float walkBobAmount = 0.05f;
        [Tooltip("Sprinting HeadBob Speed")]
        [SerializeField] private float sprintBobSpeed = 18f;
        [Tooltip("Sprinting HeadBob Camera Movement")]
        [SerializeField] private float sprintBobAmount = 0.1f;
        [Tooltip("Crouching HeadBob Speed")]
        [SerializeField] private float crouchBobSpeed = 7f;
        [Tooltip("Crouching HeadBob Camera Movement")]
        [SerializeField] private float crouchBobAmount = 0.025f;
        private float _defaultYPos;
        private float _timer;

        [Header("Player Movement")]
        [Tooltip("This is the Player's Walk Speed")]
        [SerializeField] private float walkSpeed = 10.0f;
        [Tooltip("This is the Player's Sprint Speed")]
        [SerializeField] private float sprintSpeed = 20.0f;
        [Tooltip("This is the Player's Crouch Speed")]
        [SerializeField] private float crouchSpeed = 2.5f;
        [Tooltip("This is the Player's Slope Sliding Speed")]
        [SerializeField] private float slopeSlidingSpeed = 8.0f;

        [Header("Player Jump")]
        [Tooltip("This is the Player's Jump Force")]
        [SerializeField] private float jumpForce = 12.0f;
        [Tooltip("This is the Player's Gravitational Force")]
        [SerializeField] private float gravity = 30.0f;

        [Header("Player Crouch")]
        [Tooltip("This is the Player's Crouch limit")]
        [SerializeField] private float crouchHeight = 0.5f;
        [Tooltip("This is the Player's Standing Height")]
        [SerializeField] private float standingHeight = 2.0f;
        [Tooltip("How much time it takes Player to crouch")]
        [SerializeField] private float timeToCrouch = 0.25f;
        [Tooltip("This is the Player's Crouching Center Position")]
        [SerializeField] private Vector3 crouchingCenter = new Vector3(0f, 0.5f, 0f);
        [Tooltip("This is the Player's Standing Height Position")]
        [SerializeField] private Vector3 standingCenter = new Vector3(0f, 0f, 0f);
        private bool _isCrouching;
        private bool _duringCrouchAnimation;

        //Sliding Settings
        private Vector3 _hitPointNormal;

        private bool IsSliding
        {
            get
            {
                if (!_characterController.isGrounded || !Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 2f)) return false;
                _hitPointNormal = slopeHit.normal;
                return Vector3.Angle(_hitPointNormal, Vector3.up) > _characterController.slopeLimit;
            }
        }


        //Private Variables
        private Camera _playerCamera;
        private CharacterController _characterController;

        private Vector3 _moveDirection;
        private Vector2 _currentInput;

        private float _rotationX;

        private void Awake()
        {
            _playerCamera = GetComponentInChildren<Camera>();
            _characterController = GetComponent<CharacterController>();

            _defaultYPos = _playerCamera.transform.localPosition.y;

            //Locks and Hides Cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (!CanMove) return;
            if (PauseManager.instance.isGamePaused) return;
            
            Movement();
            CameraRotation();
            
            if (canHeadBob) HeadBob();
            if (canJump) Jump();
            if (canCrouch) Crouch();
            
            FinalUpdates();
        }

        private void HeadBob()
        {
            if (!_characterController.isGrounded) return;

            if (!(Mathf.Abs(_moveDirection.x) > 0.1f) && !(Mathf.Abs(_moveDirection.z) > 0.1f)) return;
            _timer += Time.deltaTime * (_isCrouching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed);
            _playerCamera.transform.localPosition = new Vector3(
                _playerCamera.transform.localPosition.x, 
                _defaultYPos + Mathf.Sin(_timer) * (_isCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount),
                _playerCamera.transform.localPosition.z);
        }
        
        private void Movement()
        {
            _currentInput = new Vector2((_isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (_isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));
            
            float moveDirectionY = _moveDirection.y;
            _moveDirection = (transform.TransformDirection(Vector3.forward) * _currentInput.x) + (transform.TransformDirection(Vector3.right) * _currentInput.y);
            _moveDirection.y = moveDirectionY;
        }

        private void Jump()
        { 
            if(ShouldJump) _moveDirection.y = jumpForce;
        }

        private void Crouch()
        {
            if (ShouldCrouch) StartCoroutine(CrouchStand());
        }
        
        private void CameraRotation()
        {
            _rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
            _rotationX = Mathf.Clamp(_rotationX, -upperLookLimit, lowerLookLimit);
            _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0f);
        }

        private void FinalUpdates()
        {
            if(!_characterController.isGrounded)
                _moveDirection.y -= gravity * Time.deltaTime;

            if (canSlideOnSlopes && IsSliding)
                _moveDirection += new Vector3(_hitPointNormal.x, -_hitPointNormal.y, _hitPointNormal.z) * slopeSlidingSpeed;

            _characterController.Move(_moveDirection * Time.deltaTime);
        }

        private IEnumerator CrouchStand()
        {
            if(_isCrouching && Physics.Raycast(_playerCamera.transform.position, Vector3.up, 1f))
                yield break;

            _duringCrouchAnimation = true;

            float timeElapsed = 0;
            float targetHeight = _isCrouching ? standingHeight : crouchHeight;
            float currentHeight = _characterController.height;
            Vector3 targetCenter = _isCrouching ? standingCenter : crouchingCenter;
            Vector3 currentCenter = _characterController.center;

            while(timeElapsed < timeToCrouch)
            {
                _characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
                _characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            _characterController.height = targetHeight;
            _characterController.center = targetCenter;

            _isCrouching = !_isCrouching;

            _duringCrouchAnimation = false;
            
        }
    }
}