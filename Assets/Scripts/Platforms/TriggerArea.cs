using UnityEngine;

namespace Platforms
{
    public class MathGateTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject platform;
        
        [SerializeField] private bool isLeft;
        [SerializeField] private bool isEnemyArea;
        private void OnTriggerEnter(Collider other)
        {
        
            if (other.gameObject.CompareTag(TagManager.Player))
            {
                platform.GetComponent<Platform>().TriggerMathGate(isLeft);
            }
        }
    }
}
