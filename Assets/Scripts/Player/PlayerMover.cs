using UnityEngine;

namespace Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
    
        private Vector3 _targetPosition;
        private Vector3 _lastPosition;
        private Camera _camera;

        [SerializeField] private float playerSpeed = 5f;
        private bool isAlive = true;

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
            if (!isAlive){return;}
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit,float .MaxValue,layerMask))
            {
                _targetPosition = new Vector3(raycastHit.point.x,0,transform.position.z + playerSpeed);
                _lastPosition.x = raycastHit.point.x;
                transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * 2);
            }
            else
            {
                _lastPosition.z = transform.position.z + playerSpeed;
               transform.position = Vector3.Lerp(transform.position, _lastPosition, Time.deltaTime * 2);
            }
        }

        public void StopPlayer()
        {
            isAlive = false;
        }


    }
}

