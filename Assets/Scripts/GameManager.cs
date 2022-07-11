using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float rotationYSensitivity;
    public float rotationXSensitivity;

    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustRotationXSensitivity(float _rotationXSensitivity)
    {
        rotationXSensitivity = _rotationXSensitivity;
    }

    public void AdjustRotationYSensitivity(float _rotationYSensitivity)
    {
        rotationYSensitivity = _rotationYSensitivity;
    }

    /*public void AssignSrting(InputField inpField)
    {
        switch (inpField.name)
        {
            case "SentivityYInputField":
                {
                    rotationYSensitivityS = inpField.text;
                    break;
                }
            case "SentivityXInputField":
                {
                    rotationXSensitivityS = inpField.text;
                    break;
                }
            case "EnemiesPerHordeInputField":
                {
                    enemiesPerHordeS = inpField.text;
                    break;
                }
        }
    }*/

    public void StartGame()
    {
        /*if (
            rotationYSensitivityS != "" &&
            rotationXSensitivityS != "" &&
            enemiesPerHordeS != ""
            )
        {
            rotationYSensitivity = float.Parse(rotationYSensitivityS);
            rotationXSensitivity = float.Parse(rotationXSensitivityS);
            enemiesPerHorde = int.Parse(enemiesPerHordeS);
            
        }*/
        SceneManager.LoadScene("GameScene");
    }
}
