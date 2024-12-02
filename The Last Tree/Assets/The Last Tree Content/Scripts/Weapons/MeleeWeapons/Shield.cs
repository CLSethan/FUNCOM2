using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using JetBrains.Annotations;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;

public class Shield : MeleeWeapon
{
    [SerializeField] private float reloadTime;
    [SerializeField] private GameObject shieldCollider;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private List<GameObject> shieldObjects =  new List<GameObject>();
    private ShieldKnockback _shieldKnockback;
    private float currentScale;
    private float maxShieldObjects;
    private int spawnedShieldCount;
    private bool canTrigger;
    public bool isEvolving;
    public bool evolutionStatus;


    private void Awake()
    {
        _shieldKnockback = GetComponentInChildren<ShieldKnockback>();
        /*WeaponManager.Instance.ShieldWeapon = this;*/
        currentUpgradeLevel = 1;
        upgradeLevelMax = 7;
        currentAttackRate = 0.8f;
        maxAttackRate = 1.1f;
        reloadTime = 0.5f;
        evolutionStatus = false;
        isEvolving = false;
        canTrigger = true;
        maxShieldObjects = 4;
    }

    protected override IEnumerator ReloadTime(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        canTrigger = true;
    }

    protected override void TriggerAttack()
    {
        if (canTrigger)
        {
            StartCoroutine(PerformAttack(evolutionStatus));
        }
    }

    private IEnumerator PerformAttack(bool _isEvolved)
    {
        UnityEngine.Vector3 initialScale = new UnityEngine.Vector3(0.27f, 0.27f, 0.27f);
        float scaleDuration = 0.2f;

        if (!_isEvolved && !isEvolving)
            {
            canTrigger = false;
            shieldCollider.SetActive(true);
            UnityEngine.Vector3 targetScale = new UnityEngine.Vector3(currentAttackRate, currentAttackRate, currentAttackRate);
            
            // Scale up
            float elapsedTime = 0f;
            
            while (elapsedTime < scaleDuration)
            {
                transform.localScale = UnityEngine.Vector3.Lerp(initialScale, targetScale, elapsedTime / scaleDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            transform.localScale = targetScale;

            // Scale down
            elapsedTime = 0f;
            
            while (elapsedTime < scaleDuration)
            {
                transform.localScale = UnityEngine.Vector3.Lerp(targetScale, initialScale, elapsedTime / scaleDuration);
                elapsedTime += Time.deltaTime;
                yield return null; 
            }

            transform.localScale = initialScale;

            shieldCollider.SetActive(false);

            StartCoroutine(ReloadTime(reloadTime));
        }
        else if (evolutionStatus && !isEvolving)
        {
            maxAttackRate = 0.7f;
            currentAttackRate = 0.5f;
            reloadTime = 1f;
            canTrigger = false;
            initialScale = new UnityEngine.Vector3(0.27f, 0.27f, 0.27f);
            shieldCollider.SetActive(true);


            UnityEngine.Vector3 targetScale = new UnityEngine.Vector3(currentAttackRate, currentAttackRate, currentAttackRate);
            
            // Scale up
            float elapsedTime = 0f;
            
            while (elapsedTime < scaleDuration)
            {
                transform.localScale = UnityEngine.Vector3.Lerp(initialScale, targetScale, elapsedTime / scaleDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            transform.localScale = targetScale;
            
            yield return new WaitForSeconds(0.01f); 

            // Scale down
            elapsedTime = 0f;
            
            while (elapsedTime < scaleDuration)
            {
                transform.localScale = UnityEngine.Vector3.Lerp(targetScale, initialScale, elapsedTime / scaleDuration);
                elapsedTime += Time.deltaTime;
                yield return null; 
            }

            transform.localScale = initialScale;

            shieldCollider.SetActive(false);

            StartCoroutine(ReloadTime(reloadTime));
        }
    }

    protected void FixedUpdate()
    {
        if (canTrigger)
        {
                TriggerAttack();
        }
       
        if (Input.GetKeyDown(KeyCode.P))
        {
            Upgrade();
        }
    }

        public override void Upgrade()
        {
            if (currentUpgradeLevel == 6)
            {
                Evolve();
            }
            else
            {
                currentAttackRate = Mathf.Min(currentAttackRate + 0.05f, maxAttackRate);
                currentUpgradeLevel = Mathf.Min(currentUpgradeLevel + 1, upgradeLevelMax);
                _shieldKnockback.fixLevel(currentUpgradeLevel);
            }
        }

        public override void Evolve()
        {
            isEvolving = true;
            if (currentUpgradeLevel <= 7 & !evolutionStatus)
            {
                currentUpgradeLevel = 7;
                evolutionStatus = true;
            }

            UnityEngine.Vector2 _initialPosition = transform.position;

            for (int i = 0; i < maxShieldObjects; i++)
            {
                GameObject _shieldObject = shieldPrefab;
                shieldObjects.Add(_shieldObject);
            }

            shieldCollider.SetActive(true);
            createAndInstantiateShieldObjects(shieldObjects, _initialPosition);
        }

        public void createAndInstantiateShieldObjects(List<GameObject> _shieldList, UnityEngine.Vector2 _initialPosition)
        {
            UnityEngine.Vector2 newPos = new UnityEngine.Vector2();
            int shieldNumber = 0;
            float offset = 1f;
            canTrigger = false;

            foreach (GameObject shieldObject in shieldObjects)
            {

                Shield _shieldClass = shieldObject.GetComponent<Shield>();

                if (_shieldClass != null  && spawnedShieldCount < 4)
                {
                    newPos = new UnityEngine.Vector2();
                    if (shieldNumber > -1)
                    {

                        shieldObject.GetComponentInChildren<ShieldKnockback>().fixLevel(currentUpgradeLevel);

                        switch  (shieldNumber)
                        {
                            case 0:
                                newPos = new UnityEngine.Vector2(_initialPosition.x - offset, _initialPosition.y + offset);
                                break;                            
                            case 1:
                                newPos = new UnityEngine.Vector2(_initialPosition.x + offset, _initialPosition.y + offset);
                                break;
                            case 2:
                                newPos = new UnityEngine.Vector2(_initialPosition.x - offset, _initialPosition.y - offset);
                                break;
                            case 3:
                                newPos = new UnityEngine.Vector2(_initialPosition.x + offset, _initialPosition.y - offset);
                                break;
                        }
                        Instantiate(shieldObject, newPos, transform.rotation, transform.parent);
                        shieldObject.transform.localScale = new UnityEngine.Vector2(0.3f,  0.3f);
                    }
                    shieldNumber++;
                    spawnedShieldCount++;
                }
                GetComponentInChildren<CircleCollider2D>().enabled =  false;
                GetComponent<SpriteRenderer>().enabled = false;
            }
            StartCoroutine(resetShields());
        }

        private IEnumerator resetShields()
        {
            foreach (GameObject shieldObject in shieldObjects)
            {
                GameObject _currentShieldObject = GameObject.Find(shieldObject.name + "(Clone)");
                if (_currentShieldObject != null)
                {
                    _currentShieldObject.name = "Shield";
                    _currentShieldObject.GetComponent<ShieldKnockback>().fixLevel(currentUpgradeLevel);

                    Shield _currentShieldClass =  _currentShieldObject.GetComponent<Shield>();

                    _currentShieldClass.canTrigger = false;
                    _currentShieldClass.currentUpgradeLevel = currentUpgradeLevel;
                    _currentShieldClass.evolutionStatus = true;

                    yield return new WaitForSeconds(0.4f);
                    _currentShieldClass.isEvolving = false;
                    _currentShieldClass.canTrigger = true;
                }
            }
            yield return null;
        }
}
