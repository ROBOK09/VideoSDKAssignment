using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed = 1000;

    [SerializeField]
    private GameObject bulletSpawnPoint;

    private void Update()
    {
        // Spawn a new bullet from the gun if state is ingame and user presses the left mouse button

        if (Input.GetMouseButtonDown(0) && UIManager.Instance.CurrentGameState == GameState.INGAME)
        {
            GameObject bulletGameObject = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, transform.rotation);
            bulletGameObject.GetComponent<Rigidbody>().AddForce(transform.up * bulletSpeed);
        }
    }
}
