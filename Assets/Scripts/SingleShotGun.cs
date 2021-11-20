using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotGun : Gun
{
    [SerializeField] Camera cam;

    PhotonView PV;
    AudioSource audioSource;

    public AudioClip shotSound;
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
    }

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void Use()
    {
        Shoot();
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("is Reloading...");
        yield return new WaitForSeconds(reloadTime - .25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        currentAmmo--;

        audioSource.PlayOneShot(shotSound, 0.7f);
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);
            PV.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
        }
    }

    private void Update()
    {
        if (isReloading)
        {
            return;
        }
        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
    }

    [PunRPC]
    void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
        if(colliders.Length != 0) 
        {
            GameObject bulletImpactObject = Instantiate(bulletImpactPrefab, hitPosition + hitNormal * 0.001f, Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpactPrefab.transform.rotation);
            Destroy(bulletImpactObject, 10f);
            bulletImpactObject.transform.SetParent(colliders[0].transform);
        }
    }

}
