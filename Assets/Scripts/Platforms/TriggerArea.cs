using UnityEngine;
using UnityEngine.Serialization;

namespace Platforms
{
    [RequireComponent(typeof(BoxCollider))]
    public class TriggerArea : MonoBehaviour
    {
        [SerializeField] private GameObject platform;
        [SerializeField] private bool _isLeft;
        [SerializeField] private bool IsEnemyArea;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(TagManager.Player)){return;}
            
            if (!IsEnemyArea)
            {
                platform.GetComponent<NormalPlatform>().TriggerMathGate(_isLeft, other.gameObject);
            }
            else
            {
                platform.GetComponent<Platform>().TriggerEnemyArea(other.gameObject);
            }
        }
    }
}
