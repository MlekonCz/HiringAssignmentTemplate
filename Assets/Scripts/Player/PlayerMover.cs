using Core;
using UnityEngine;

namespace Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;

        private Vector3 _targetPosition;
        private Vector3 _lastPosition;
        private Camera _camera;
        private float _playerSpeed = 5f;
        private bool _isAlive = true;
        private bool _isEndlessMode = false;

        private GameManager _gameManager;

        private void Awake()
        {
            _camera = Camera.main;
            _gameManager = PersistentObjects.Instance.GameManager;
            if (_gameManager == null)
            {
                Debug.LogError($"Player manager is null.");
            }

            SetPlayerSpeed(_gameManager.CurrentLevelDefinition.PlayerSpeed,
                _gameManager.CurrentLevelDefinition.IsEndlessMode);
        }


        private void Update()
        {
            if (!_isEndlessMode)
            {
                return;
            }

            _playerSpeed += Time.deltaTime / 4;
        }

        private void FixedUpdate()
        {
            MovePlayerToMousePosition();
        }

        private void MovePlayerToMousePosition()
        {
            if (!_isAlive)
            {
                return;
            }

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
            {
                _targetPosition = new Vector3(raycastHit.point.x, 0,
                    transform.position.z + _playerSpeed * Time.deltaTime);
                _lastPosition.x = raycastHit.point.x;
                transform.position = _targetPosition;
            }
            else
            {
                _lastPosition.z = transform.position.z + _playerSpeed * Time.deltaTime;
                transform.position = _lastPosition;
            }
        }

        private void SetPlayerSpeed(float speed, bool isEndlessMode)
        {
            Debug.Log($"Set speed to {speed}");
            _playerSpeed = speed;
            _isEndlessMode = isEndlessMode;
        }

        public void StopPlayer()
        {
            _isAlive = false;
        }
    }
}