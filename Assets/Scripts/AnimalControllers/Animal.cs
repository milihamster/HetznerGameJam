using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Controls))]
public abstract class Animal : MonoBehaviour
{
    public float expCurrent;
    public float expToUpgrade;
    public float expDefault;
    public AnimalType animalType;

    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;

    protected Controls _controls;

    public UnityEvent _onDeath;

    SpawnManager spawnManager;
    List<GameObject> targetList;


    // Start is called before the first frame update
    void Start()
    {
        spawnManager = SpawnManager.Instance;
        _controls = GetComponent<Controls>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if(_controls.Attack)
        {
            Attack();
        }
    }

    protected void Attack()
    {
        foreach (GameObject target in targetList)
        {
            Animal animalComponent = target.GetComponent<Animal>();

            if (expCurrent > animalComponent.expCurrent)
            {
                expCurrent += animalComponent.expCurrent;

                animalComponent._onDeath.Invoke();

                if (expCurrent >= expToUpgrade)
                {
                    spawnManager.LevelUp(gameObject);
                }
            }
            else
            {
                //TODO: Die?
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Animal animal))
        {
            targetList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (targetList.Contains(collision.gameObject))
        {
            targetList.Remove(collision.gameObject);
        }
    }
}
