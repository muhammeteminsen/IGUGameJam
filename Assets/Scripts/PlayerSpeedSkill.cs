using System;
using UnityEngine;

public class PlayerSpeedSkill : MonoBehaviour
{
   [SerializeField] private ManaSystem manaSystem;
   [SerializeField] private float skillSpeed = 50f;
   private PlayerMovement _playerMovement;
   private void Start()
   {
      _playerMovement = GetComponent<PlayerMovement>();
      manaSystem = FindObjectOfType<ManaSystem>();
   }

   private void Update()
   {
      if (manaSystem.isSpeedSkill)
      {
         _playerMovement.movementSpeed = skillSpeed;
      }
      else
      {
         _playerMovement.movementSpeed = _playerMovement.defaultSpeed;
      }
   }
}
