/*  Filename:           SceneManagerHelper.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        March 3, 2023
 *  Description:        
 *  Revision History:   March 3, 2023 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerHelper : MonoBehaviour
{
    [SerializeField] private bool reloadCurrentScene;
    [SerializeField] private int sceneIndex;

    public void LoadScene()
    {
        if (reloadCurrentScene)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
