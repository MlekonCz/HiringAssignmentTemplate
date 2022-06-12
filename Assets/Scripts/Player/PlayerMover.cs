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
        public void SetPlayerSpeed(float speed){ _playerSpeed = speed;}
        
        private bool _isAlive = true;

        private void Awake()
        {
            _camera = Camera.main;
        }
        private void FixedUpdate()
        {
            MovePlayerToMousePosition();
        }

        private void MovePlayerToMousePosition()
        {
            if (!_isAlive){return;}
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit,float .MaxValue,layerMask))
            {
                _targetPosition = new Vector3(raycastHit.point.x,0,transform.position.z + _playerSpeed * Time.deltaTime);
                _lastPosition.x = raycastHit.point.x;
                transform.position = _targetPosition;
            }
            else
            {
                _lastPosition.z = transform.position.z + _playerSpeed * Time.deltaTime ;
                transform.position = _lastPosition;
            }
        }

        public void StopPlayer()
        {
            _isAlive = false;
        }


    }
}

