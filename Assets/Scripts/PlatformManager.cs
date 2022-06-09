using TMPro;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private float platformSpeed = 5f;

    [SerializeField] private GameObject baseModel;
    [SerializeField] private PlatformDefinition platformDefinition;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;

    private void Start()
    {
        OverrideModel();
        AssignEquations();
    }

    private void AssignEquations()
    {
        leftWall.GetComponent<TMP_Text>().text = platformDefinition.equation;
        rightWall.GetComponent<TMP_Text>().text = platformDefinition.equation;
    }
    private void OverrideModel()
    {
        
        if (platformDefinition.platformModelOverride != null)
        {
            baseModel = platformDefinition.platformModelOverride;
            baseModel.transform.position = new Vector3(20f, 1f, 40f);
        }
    }

    private void Update()
    {
        
    }

}
