using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitItem : MonoBehaviour
{
    public enum FruitType
    {
        Cherry,
        Strawberry,
        Grape,
        Dekopon,
        Persimmon,
        Apple,
        Pear,
        Peach,
        Pineapple,
        Melon,
        Watermelon
    }


    private FruitDropper m_fruitDropper;

    private SpriteRenderer m_spriteRenderer = null;
    private CircleCollider2D m_circleCollider2D = null;

    [SerializeField] private Sprite[] m_sprites;

    private FruitType m_curFruitType;
    private bool m_bisCanCallUpgrade = true;


    public void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        FruitItem fruitItem = collision.transform.GetComponent<FruitItem>();
        if (fruitItem != null
            && fruitItem.m_curFruitType == m_curFruitType
            && m_bisCanCallUpgrade)
        {
            fruitItem.m_bisCanCallUpgrade = false;
            m_fruitDropper.UpgradeFruit(this, fruitItem);
        }
    }

    internal void Init(FruitDropper fruitDropper, FruitType fruitType)
    {
        m_fruitDropper = fruitDropper;
        m_curFruitType = fruitType;

        m_spriteRenderer.sprite = m_sprites[(int)m_curFruitType];
        transform.localScale = new Vector3(0.5f * (int)m_curFruitType + 1.0f, 0.5f * (int)m_curFruitType + 1.0f, 1.0f);

        m_spriteRenderer.enabled = true;
    }
    internal void ActiveCollider()
    {
        m_circleCollider2D.enabled = true;
    }

    internal FruitType GetFruitType()
    {
        return m_curFruitType;
    }
}
