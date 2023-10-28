using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAttackTrigger : MonoBehaviour
{
    public List<Animal> TargetList = new();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Animal animal))
            TargetList.Add(animal);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Animal animal))
        {
            if (TargetList.Contains(animal))
                TargetList.Remove(animal);
        }
    }
}
