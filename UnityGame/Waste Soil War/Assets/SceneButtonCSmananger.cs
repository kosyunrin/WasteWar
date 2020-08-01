using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButtonCSmananger :MonoBehaviour
{
    private GameObject _camera = null;
    private void Awake()
    {
        _camera=
        GameObject.Find("Main Camera");
    }
    public void isActionMode()
    {
        var _cscameraController = _camera.GetComponent<CSCameraController>().enabled = !_camera.GetComponent<CSCameraController>().enabled;
    }
}
