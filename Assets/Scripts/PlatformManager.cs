using Definitions;
using TMPro;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private float platformSpeed = 5f;

    [SerializeField] private PlatformDefinition platformDefinition;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;

    private void Start()
    {
        AssignEquations();
    }

    private void AssignEquations()
    {
        leftWall.GetComponent<TMP_Text>().text = platformDefinition.equation;
        rightWall.GetComponent<TMP_Text>().text = platformDefinition.equation;
    }

    private void Update()
    {
        
    }

}
