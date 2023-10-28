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

    public float Experience;
    public AnimalSo AnimalSo;

    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;
    protected SpriteRenderer _spriteRenderer;
    protected AnimalAttackTrigger _attackTrigger;

    public UnityEvent OnDeath;

    protected Controls _controls;

    //public UnityEvent OnDeath;

    private SpawnManager _spawnManager;

    protected float _size = 1;

    public bool IsGrounded;

    private float _attackCooldown;

    void Start()
    {
        _spawnManager = SpawnManager.Instance;
        var controlComponents = GetComponents<Controls>();
        _controls = controlComponents.FirstOrDefault(x => x.enabled);
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _attackTrigger = GetComponentInChildren<AnimalAttackTrigger>();

        _collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (_attackCooldown > 0)
            _attackCooldown -= Time.deltaTime;
        else if (_controls.Attack)
        {
            _attackCooldown = 1f;
            Attack();
        }

        // Flip if Animal is heading the other way
        if (_rigidbody.velocity.x < 0)
            transform.localScale = Vector3.one * _size;
        else if (_rigidbody.velocity.x > 0)
            transform.localScale = new Vector3(-_size, _size, _size);
    }

    public void Attack()
    {
        var animal = _attackTrigger.TargetList.FirstOrDefault();
        if (animal != null)
        {
            if (Experience > animal.Experience)
            {
                Experience += animal.Experience;

                if (Experience >= AnimalSo.XpUntilLevelup)
                    _spawnManager.LevelUp(this);

                LeanTween.move(gameObject, animal.transform.position, 0.5f)
                    .setEaseInOutBounce();

                animal.Kill();
            }
            else
            {
                LeanTween.move(animal.gameObject, transform.position, 0.5f)
                    .setEaseInOutBounce();
                Kill();
            }
        }
    }

    public void Kill()
    {
        OnDeath.Invoke();
        EffectsManager.Instance.SpawnEffect(_prefabDeathParticles ?? GlobalDataSo.Instance.PrefabDefaultDeathEffect, transform.position);
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
