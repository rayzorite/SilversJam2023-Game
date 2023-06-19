using Player;
using UnityEngine;

namespace SilverDemons.Player
{
    public class Footsteps : MonoBehaviour
    {
        public AudioClip[] footstepSounds;
        public float footstepInterval = 0.4f;
        public float footstepVolume = 0.15f;

        private float _footstepTimer;
        private AudioSource _audioSource;
        private CharacterController _characterController;
        private PlayerController _playerController;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
            
            // Get the reference to the Character Controller component.
            _characterController = GetComponent<CharacterController>();

            // Add an AudioSource component to the game object.
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.spatialBlend = 1f; // Set spatial blend to make footstep sounds 3D.
        }

        private void Update()
        {
            // Check if the player is grounded and moving, and if enough time has passed for the next footstep.
            if (IsGrounded() && _characterController.velocity.magnitude > 0.1f && _footstepTimer <= 0f)
            {
                // Play a random footstep sound.
                PlayRandomFootstepSound();

                // Reset the footstep timer.
                _footstepTimer = footstepInterval;
                
                //change footstep interval
                footstepInterval = 0.4f;
            }
            
            //Check if the player is sprinting and moving, and if enough time has passed for the next footstep.
            if (IsGrounded() && _characterController.velocity.magnitude > 0.1f && _footstepTimer <= 0f && _playerController.IsSprinting)
            {
                // Play a random footstep sound.
                PlayRandomFootstepSound();

                // Reset the footstep timer.
                _footstepTimer = footstepInterval;
                
                //change footstep interval
                footstepInterval = 0.3f;
            }

            // Update the footstep timer.
            _footstepTimer -= Time.deltaTime;
        }

        // Checks if the player is grounded.
        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, 2f);
        }

        // Plays a random footstep sound.
        private void PlayRandomFootstepSound()
        {
            // Check if there are any footstep sounds available.
            if (footstepSounds.Length > 0)
            {
                // Choose a random footstep sound from the array.
                int randomIndex = Random.Range(0, footstepSounds.Length);
                AudioClip randomFootstepSound = footstepSounds[randomIndex];

                // Play the footstep sound using the AudioSource component.
                _audioSource.PlayOneShot(randomFootstepSound, footstepVolume);
            }
        }
    }
}
