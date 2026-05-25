using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Game.PlayerCanvas
{
    public class Heart: MonoBehaviour
    {
        [SerializeField] private Image _fillHeartImage;
        public void Disappear()
        {
            _fillHeartImage.fillAmount = 0;
        }
    }
}