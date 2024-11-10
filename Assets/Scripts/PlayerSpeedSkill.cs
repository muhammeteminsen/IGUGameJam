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
      if (_playerMovement.isSpring) return;
      _playerMovement.movementSpeed = manaSystem.isSpeedSkill && _playerMovement.isSpring
         ? skillSpeed
         : _playerMovement.defaultSpeed;
   }
}
