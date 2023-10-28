using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Controls))]
public abstract class Animal : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _prefabDeathParticles;

    public float expCurrent;
    public float expToUpgrade;
    public float expDefault;
    public AnimalType animalType;

    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;
    protected SpriteRenderer _spriteRenderer;
    protected AnimalAttackTrigger _attackTrigger;

    protected Controls _controls;

    public UnityEvent OnDeath;

    SpawnManager spawnManager;

    protected float _size = 1;

    public bool IsGrounded;

    void Start()
    {
        spawnManager = SpawnManager.Instance;
        var controlComponents = GetComponents<Controls>();
        _controls = controlComponents.FirstOrDefault(x => x.enabled);
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _attackTrigger = GetComponentInChildren<AnimalAttackTrigger>();

        _collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (_controls.Attack)
            Attack();

        // Flip if Animal is heading the other way
        if (_rigidbody.velocity.x < -0.1f)
            transform.localScale = Vector3.one * _size;
        else if (_rigidbody.velocity.x > 0.1f)
            transform.localScale = new Vector3(-_size, _size, _size);
    }

    public void Attack()
    {
        var animal = _attackTrigger.TargetList.FirstOrDefault();
        if (animal != null)
        {
            if (expCurrent > animal.expCurrent)
            {
                expCurrent += animal.expCurrent;

                if (expCurrent >= expToUpgrade)
                {
                    //spawnManager.LevelUp(gameObject);
                }

                animal.Kill();
            }
            else
            {
                // TODO: Make them attack me instead for animation?
                Kill();
            }
        }
    }

    public void Kill()
    {
        OnDeath.Invoke();
        EffectsManager.Instance.SpawnEffect(_prefabDeathParticles.gameObject, transform.position);
        Destroy(gameObject);
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
}
