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
    protected Collider2D _collider;
    protected SpriteRenderer _spriteRenderer;

    protected Controls _controls;

    public UnityEvent _onDeath;

    SpawnManager spawnManager;
    List<GameObject> targetList;


    protected float _size = 1;

    public bool IsGrounded;

    void Start()
    {
        spawnManager = SpawnManager.Instance;
        _controls = GetComponent<Controls>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
<<<<<<< HEAD
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
=======
        // Flip if Animal is heading the other way
        if (_rigidbody.velocity.x < 0)
            transform.localScale = Vector3.one * _size;
        else if (_rigidbody.velocity.x > 0)
            transform.localScale = new Vector3(-_size, _size, _size);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
            IsGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
            IsGrounded = false;
    }

>>>>>>> ae963d0f3dfb6acdce6f6b024192aaa0c2ffb3fc
}
