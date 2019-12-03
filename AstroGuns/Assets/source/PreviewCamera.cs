using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	public MeshFilter	weaponView		= null;
	public Mesh         lockMesh		= null;
	public List<Mesh>   weaponsMeshes	= new List<Mesh>();
	private int         currentMesh		= 0;

	public Text         nameLabel       = null;
	public Text         orderLabel      = null;

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

	public void Start()
	{
        currentMesh = Mathf.Clamp(Inventory.GetBiggestWeaponId(), 0, Inventory.slots.Count);
		RefreshWeapon();
        SaveManager.OnLoad.AddListener(() => currentMesh = Inventory.GetBiggestWeaponId());
	}

	void Update()
    {
        Weapon.transform.Rotate(0, Time.deltaTime * RotateSpeed, 0);
    }

	public void NextWeapon()
	{
		++currentMesh;
		if(currentMesh > weaponsMeshes.Count - 1) currentMesh = 0;
		RefreshWeapon();
	}

	public void PrevWeapon()
	{
		--currentMesh;
		if(currentMesh < 0) currentMesh = weaponsMeshes.Count - 1;
		RefreshWeapon();
	}

	public void RefreshWeapon()
	{
		if(currentMesh >= InventorySystem.Instance.biggestWeaponId)
		{
			weaponView.mesh = lockMesh;
			nameLabel.text = "lock";
		}
		else
		{
            Debug.Log(currentMesh);
            Debug.Log(weaponsMeshes.Count);
			weaponView.mesh = weaponsMeshes[currentMesh];
			nameLabel.text = InventorySystem.Instance.weaponObjects[currentMesh].weaponName;
		}

		orderLabel.text = (currentMesh + 1) + "/" + weaponsMeshes.Count;
	}
}
