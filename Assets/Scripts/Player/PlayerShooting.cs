using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ShotType
{
    public ShotType(int amount, GameObject bulletPrefab, int spreadInDegrees)
    {
        this.amount = amount;
        this.bulletPrefab = bulletPrefab;
        this.spreadInDegrees = spreadInDegrees;
    }

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
    private Animator animator;
    private Animator bulletAnimator;
    private bool isShooting;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        //bulletAnimator = gameObject.GetComponentInChildren<Animator>();
        shotTypes = new ShotType[1];
        shotTypes[0] = new ShotType(10, Resources.Load<GameObject>("BaseBullet"), 60);
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
        isShooting = true;
        transform.GetChild(0).gameObject.SetActive(true);
        animator.SetBool("shooting", isShooting);

        // TODO: make the sprite of the number on top of the dice disappear
        shotLoaded = false;
        ShotType shotType = shotTypes[activeShotType];
        Vector2 shotDirection = (target - (Vector2) transform.position).normalized;
        float halfShotSpread = shotType.spreadInDegrees / 2;

        for (int i = 0; i < shotType.amount; i++)
        {
            GameObject bulletObject = Instantiate(shotType.bulletPrefab, transform.position, Quaternion.identity);
            BaseBullet bulletScript = bulletObject.GetComponent<BaseBullet>();
            Quaternion randomSpread = Quaternion.Euler(0, 0, Random.Range(0, shotType.spreadInDegrees) - halfShotSpread);
            bulletScript.Initialize(randomSpread * shotDirection, targetLayers, Color.green);
        }
        //bulletAnimator.Play("bang");
    }

    public void StopShooting() {
        transform.GetChild(0).gameObject.SetActive(false);
        isShooting = false;
        animator.SetBool("shooting", isShooting);
    }
}
