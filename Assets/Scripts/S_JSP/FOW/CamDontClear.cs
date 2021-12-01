using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=qE2TW_8cSNQ&t=954s

public class CamDontClear : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private void Awake()
    {
        if (cam == null)
        {
            if (cam == null)
                cam = this.GetComponent<Camera>();
            Initalize();
        }
    }

    public void Initalize()
    {
        cam.clearFlags = CameraClearFlags.Color;
    }

    public void OnPostRender()
    {
        cam.clearFlags = CameraClearFlags.Nothing;
    }

}
