using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSkillController : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList;
    private float maxSize;
    private float growSpeed;
    private float shrinkSpeed;
    private bool canGrow = true;
    private bool canShrink;

    private bool canCreateHotKeys = true;
    private bool cloneAttackReleased;
    private int amountOfAttacks = 8;
    private float cloneAttackCooldown = .3f;
    private float cloneAttackTimer;
    private bool playerCanDisaper = true;

    private float blackHoleDuration = 5f;

    private List<Transform> targets = new List<Transform>();
    private List<GameObject> createHotKey = new List<GameObject>();

    public bool playerCanExitState { get; private set; }
    public void SetupBlackHole(float maxSize, float growSpeed, float shrinkSpeed, int amountOfAttacks, float cloneAttackCooldown)
    {
        this.maxSize = maxSize;
        this.growSpeed = growSpeed;
        this.shrinkSpeed = shrinkSpeed;
        this.amountOfAttacks = amountOfAttacks;
        this.cloneAttackCooldown = cloneAttackCooldown;
    }
    private void Update()
    {
        cloneAttackTimer -= Time.deltaTime;
        blackHoleDuration -= Time.deltaTime;

        if (blackHoleDuration < 0)
        {
            blackHoleDuration = Mathf.Infinity;
            if (targets.Count > 0)
            {
                ReleaseCloneAttack();
            }
            else
            {
                FinishBlackHoleAbility();
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ReleaseCloneAttack();
        }

        CloneAttack();

        if (canGrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }

        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);

            if (transform.localScale.x < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ReleaseCloneAttack()
    {
        if (targets.Count <= 0)
        {
            return;
        }

        DestroyHotKey();
        cloneAttackReleased = true;
        canCreateHotKeys = false;
        if (playerCanDisaper)
        {
            PlayerManager.instance.player.MakeTransprent(true);
            playerCanDisaper = false;
        }
    }

    private void CloneAttack()
    {
        if (cloneAttackTimer <= 0 && cloneAttackReleased && amountOfAttacks > 0)
        {
            cloneAttackTimer = cloneAttackCooldown;

            int randomIndex = Random.Range(0, targets.Count);

            float xOffset;

            if (Random.Range(0, 100) > 50)
            {
                xOffset = 2;
            }
            else
            {
                xOffset = -2;
            }

            SkillManager.instance.clone.CreateClone(targets[randomIndex], new Vector3(xOffset, 0));

            amountOfAttacks--;
            if (amountOfAttacks <= 0)
            {
                Invoke("FinishBlackHoleAbility", 1f);
            }
        }
    }

    private void FinishBlackHoleAbility()
    {
        DestroyHotKey();
        playerCanExitState = true;
        canShrink = true;
        cloneAttackReleased = false;
    }

    private void DestroyHotKey()
    {
        if (createHotKey.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < createHotKey.Count; i++)
        {
            Destroy(createHotKey[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().FreezeTimer(true);
            //在敌人头上使用预制件
            CreatHotKey(other);

        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.GetComponent<Enemy>() != null)
    //    {
    //        collision.GetComponent<Enemy>().FreezeTimer(false);
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision) => collision.GetComponent<Enemy>()?.FreezeTimer(false);



    private void CreatHotKey(Collider2D other)
    {
        if (keyCodeList.Count <= 0)
        {
            return;
        }

        if (!canCreateHotKeys)
        {
            return;
        }

        GameObject newHotKey = Instantiate(hotKeyPrefab, other.transform.position + new Vector3(0, 2), Quaternion.identity);
        createHotKey.Add(newHotKey);

        KeyCode choosenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
        keyCodeList.Remove(choosenKey);

        BlackHoleHotKeyController newHotKeyScript = newHotKey.GetComponent<BlackHoleHotKeyController>();
        newHotKeyScript.SetupHotKey(choosenKey, other.transform, this);
    }

    public void AddEnemyToList(Transform enemyTransform) => targets.Add(enemyTransform);
}
