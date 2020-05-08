using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectParticle : MonoBehaviour
{
    [SerializeField]
    public List<ParticleUnit> ParticleUnits;

    private float _duration = 4f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (ParticleUnit pUnit in ParticleUnits)
        {
            int _count = Random.Range(pUnit.CountRange.x, pUnit.CountRange.y + 1);
            for (int i = 0; i < _count; i++)
            {
                GameObject p = Instantiate(pUnit.Prefab, transform.position + Vector3.up * 0.7f, transform.rotation);
                Vector2 _direction = Vector2.right * Random.Range(-1f, 1f);
                p.transform.localScale = p.transform.localScale * pUnit.Scale;
                p.GetComponent<Rigidbody2D>().AddForce(_direction * pUnit.ExplosionPower);
            }
        }
        Destroy(gameObject, _duration);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

[System.Serializable]
public struct ParticleUnit
{
    public GameObject Prefab;
    public Vector2Int CountRange;
    public float Scale;
    public float ExplosionPower;
}
