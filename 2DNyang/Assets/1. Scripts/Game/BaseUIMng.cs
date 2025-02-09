using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;



public class BaseUIMng : MonoBehaviour
{
    public static BaseUIMng Instance;

    [Header("Prefab")]
    [SerializeField] Transform BarUI1;

    [Header("Base Transform")]
    [SerializeField] Transform EnemyBase;
    [SerializeField] Transform AllyBase;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Transform AllyUI = CreateNewBarUI(AllyBase, new Vector3(0, 2, 0), eBaseType.Ally);
        Transform EnemyUI = CreateNewBarUI(EnemyBase, new Vector3(0, 2, 0), eBaseType.Enemy);

    }

    public Transform CreateNewBarUI(Transform TargetTransform, Vector3 offset, eBaseType baseType)
    {
        Transform NewUI = Instantiate(BarUI1, TargetTransform.position, Quaternion.identity, transform);

        StartCoroutine(BarUICoroutine(NewUI, TargetTransform, offset, baseType));

        return NewUI;
    }

    private IEnumerator BarUICoroutine(Transform NewUI, Transform TargetTransform, Vector3 offset, eBaseType baseType)
    {
        LobbyMng lobbyMng = LobbyMng.Instance;

        Transform Bar = NewUI.Find("Bar");
        Transform InBar = Bar.Find("Bar");
        Transform TitleTras = Bar.Find("Title");
        TMP_Text Title = TitleTras.GetComponent<TMP_Text>();

        while (NewUI.parent)
        {

            NewUI.position = TargetTransform.position + offset;

            float hp = 0;
            float maxHp = 0;
            float Process = 0;

            if (baseType == eBaseType.Ally)
            {
                hp = lobbyMng.allyBaseHp;
                maxHp = lobbyMng.allyBaseMaxHp;
            }
            else if (baseType == eBaseType.Enemy)
            {
                hp = lobbyMng.enemyBaseHp;
                maxHp = lobbyMng.enemyBaseMaxHp;
            }
            Process = hp / maxHp;
            InBar.localScale = new Vector3(Process, 1);
            Title.text = $"{hp} / {maxHp}";

            yield return null;
        }

    }


}
