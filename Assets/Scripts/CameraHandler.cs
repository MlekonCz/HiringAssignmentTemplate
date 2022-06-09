using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
   private GameObject _player;
   private float distanceBetweenPlayer;
   private void Start()
   {
      _player = GameObject.FindGameObjectWithTag(TagManager.Player);
      distanceBetweenPlayer = transform.position.z - _player.transform.position.z;
      Debug.Log(distanceBetweenPlayer);
   }

   private void Update()
   {
      transform.position =new Vector3(transform.position.x,transform.position.y,
         _player.transform.position.z + distanceBetweenPlayer);
   }
}
