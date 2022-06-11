using UnityEngine;

namespace Platforms
{
    public class TriggerArea : MonoBehaviour
    {
        [SerializeField] private GameObject platform;
        
        [SerializeField] private bool isLeft;
        [SerializeField] private bool isEnemyArea;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(TagManager.Player)){return;}
            
            if (!isEnemyArea)
            {
                platform.GetComponent<NormalPlatform>().TriggerMathGate(isLeft, other.gameObject);
            }
            else
            {
                platform.GetComponent<Platform>().TriggerEnemyArea(gameObject,other.gameObject);
            }
        }
    }
}
