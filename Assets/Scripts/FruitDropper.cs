using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FruitDropper : MonoBehaviour
{
    [SerializeField] private GameObject m_prefab_FruitItem;

    [SerializeField] private TMP_Text m_text_Score;
    private int m_score = 0;

    private FruitItem m_curHoldFruitItem = null;
    private bool m_bisFruitThrowEnd = true;

    private bool m_bisGameOver = false;


    public void Update()
    {
        if (m_bisGameOver)
        {
            return;
        }

        if (m_curHoldFruitItem == null && m_bisFruitThrowEnd)
        {
            m_curHoldFruitItem = Instantiate(m_prefab_FruitItem, new Vector3(0.0f, transform.position.y, 0.0f), Quaternion.identity).GetComponent<FruitItem>();
            m_curHoldFruitItem.Init(this, (FruitItem.FruitType)(Mathf.Floor(Mathf.Pow(20, Random.Range(0.0f, 1.0f) - 0.75f) + 0.5f)));
        }
        else if (m_curHoldFruitItem != null && m_bisFruitThrowEnd)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_curHoldFruitItem.transform.position = new Vector3()
            {
                x = mouseWorldPos.x,
                y = transform.position.y,
                z = 0.0f
            };
            if (m_curHoldFruitItem.transform.position.x < -3.7f)
            {
                m_curHoldFruitItem.transform.position = new Vector3(-3.7f, m_curHoldFruitItem.transform.position.y, 0.0f);
            }
            else if (m_curHoldFruitItem.transform.position.x > 3.7f)
            {
                m_curHoldFruitItem.transform.position = new Vector3(3.7f, m_curHoldFruitItem.transform.position.y, 0.0f);
            }

            if (Input.GetMouseButtonDown(0))
            {
                m_bisFruitThrowEnd = false;

                m_curHoldFruitItem.ActiveCollider();
                m_curHoldFruitItem.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                m_curHoldFruitItem = null;
                Invoke("ThrowFruit", 1.0f);
            }
        }
    }

    public void GameOver()
    {
        m_bisGameOver = true;
    }
    internal void UpgradeFruit(FruitItem fruitItem_0, FruitItem fruitItem_1)
    {
        FruitItem.FruitType curFruitType = fruitItem_0.GetFruitType();
        if (curFruitType == FruitItem.FruitType.Watermelon)
        {
            Destroy(fruitItem_0.gameObject);
            Destroy(fruitItem_1.gameObject);
        }
        else
        {
            Vector3 itemPos_0 = fruitItem_0.transform.position;
            Vector3 itemPos_1 = fruitItem_1.transform.position;

            Destroy(fruitItem_0.gameObject);
            Destroy(fruitItem_1.gameObject);

            FruitItem newFruitItem = Instantiate(m_prefab_FruitItem).GetComponent<FruitItem>();
            newFruitItem.transform.position = (itemPos_0 + itemPos_1) / 2.0f;
            newFruitItem.Init(this, (FruitItem.FruitType)((int)curFruitType + 1));
            newFruitItem.ActiveCollider();
        }

        switch (curFruitType)
        {
            case FruitItem.FruitType.Cherry:
                m_score += 1;
                break;

            case FruitItem.FruitType.Strawberry:
                m_score += 3;
                break;

            case FruitItem.FruitType.Grape:
                m_score += 6;
                break;

            case FruitItem.FruitType.Dekopon:
                m_score += 10;
                break;

            case FruitItem.FruitType.Persimmon:
                m_score += 15;
                break;

            case FruitItem.FruitType.Apple:
                m_score += 21;
                break;

            case FruitItem.FruitType.Pear:
                m_score += 28;
                break;

            case FruitItem.FruitType.Peach:
                m_score += 36;
                break;

            case FruitItem.FruitType.Pineapple:
                m_score += 45;
                break;

            case FruitItem.FruitType.Melon:
                m_score += 55;
                break;

            case FruitItem.FruitType.Watermelon:
                m_score += 66;
                break;
        }
        m_text_Score.text = $"Score : {m_score}";
    }

    private void ThrowFruit()
    {
        m_bisFruitThrowEnd = true;
    }

}
