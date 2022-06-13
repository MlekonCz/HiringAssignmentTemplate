using UnityEngine;

namespace Player
{
   public class CameraHandler : MonoBehaviour
   {
      private GameObject _player;
      private float _distanceBetweenPlayer;
      private void Start()
      {
         _player = GameObject.FindGameObjectWithTag(TagManager.Player);
         _distanceBetweenPlayer = transform.position.z - _player.transform.position.z;
      }

      private void LateUpdate()
      {
         transform.position =new Vector3(transform.position.x,transform.position.y,
            _player.transform.position.z + _distanceBetweenPlayer);
      }
   }
}
