using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPreview : MonoBehaviour
{
    public RawImage Image;

    // Start is called before the first frame update
    void Start()
    {
        PreviewCamera previewCamera = FindObjectOfType<PreviewCamera>();
        if(!previewCamera)
            Debug.LogError("There is no preview camera");
        Image.texture = previewCamera.WeaponTexture;
    }

}
