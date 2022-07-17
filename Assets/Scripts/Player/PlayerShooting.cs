using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ShotType
{
    public int amount;
    public GameObject bulletPrefab;
    public int spreadInDegrees;
    // TODO: put a variable that remembers the sprite here
}

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayers;
    private bool shotLoaded = true;
    private ShotType[] shotTypes;
    private int activeShotType = 0;

    private void Awake()
    {
        shotTypes = new ShotType[1];
        shotTypes[0].amount = 1;
        shotTypes[0].bulletPrefab = Resources.Load<GameObject>("BaseBullet");
        RandomizeAndReloadBullet();
    }


    public void RandomizeAndReloadBullet()
    {
        shotLoaded = true;
        activeShotType = Random.Range(0, shotTypes.Length);
        // TODO: tell the sprite of the number on the dice to change and make the sprite appear
    }

    public void Shoot(Vector2 target)
    {
        if (!shotLoaded)
        {
            return;
        }
        // TODO: make the sprite of the number on top of the dice disappear
        shotLoaded = false;
        ShotType shotType = shotTypes[activeShotType];
        Vector2 shotDirection = (target - (Vector2) transform.position).normalized;

        for (int i = 0; i < shotType.amount; i++)
        {
            GameObject bulletObject = Instantiate(shotType.bulletPrefab, transform.position, Quaternion.identity);
            BaseBullet bulletScript = bulletObject.GetComponent<BaseBullet>();
            bulletScript.Initialize(shotDirection, targetLayers);
        }
    }
}
