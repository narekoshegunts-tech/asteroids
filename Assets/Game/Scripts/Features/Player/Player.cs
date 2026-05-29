using Game.Scripts.Features.Player.Interfaces;
using UnityEngine;

namespace Game.Scripts.Features.Player
{
    public class Player: MonoBehaviour, IPlayerTransform
    {
        public Transform Transform => transform;
    }
}