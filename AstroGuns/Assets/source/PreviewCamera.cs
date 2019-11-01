using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewCamera : MonoBehaviour
{
    public Camera Camera;
    public RenderTexture WeaponTexture
    {
        get;
        private set;
    }

    public GameObject Weapon;
    public float RotateSpeed;

    private void Awake()
    {
        WeaponTexture = new RenderTexture(512, 512, 24, RenderTextureFormat.ARGB32);

        WeaponTexture.antiAliasing = 8;
        WeaponTexture.autoGenerateMips = false;
        WeaponTexture.useMipMap = false;
        WeaponTexture.name = "WeaponPreviewTexture";
        WeaponTexture.filterMode = FilterMode.Bilinear;
        WeaponTexture.wrapMode = TextureWrapMode.Clamp;
        WeaponTexture.Create();

        Material mat = new Material(Shader.Find("Sprites/Default"));
        mat.mainTexture = WeaponTexture;

        Camera.enabled = true;
        Camera.targetTexture = WeaponTexture;

        Weapon.SetActive(true);
    }

    void Update()
    {
        Weapon.transform.Rotate(0, Time.deltaTime * RotateSpeed, 0);
    }
}
