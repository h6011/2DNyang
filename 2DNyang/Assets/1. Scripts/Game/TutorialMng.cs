using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialMng : MonoBehaviour
{
    public static TutorialMng Instance;

    [Header("INS")]
    private GameCamMng gameCamMng;

    [Header("UI")]
    public GameObject TutorialCanvas;
    public Transform TutorialCameraPos;

    [Header("Dialog")]
    public GameObject DialogUI;
    public TMP_Text DialogTitle;

    [Header("STAT")]
    [HideInInspector] public bool IsDoingTutorial;


    private void Awake()
    {
        SELF_INS();
    }

    private void Start()
    {
        OTHER_INS();
        initActions();
        if (GameStatus.CurrentLevel == 0)
        {
            StartTutorial();
        }
    }

    private void Update()
    {
        
    }

    private void OTHER_INS()
    {
        gameCamMng = GameCamMng.Instance;
    }

    private void SELF_INS()
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


    private void initActions()
    {
        DialogUI.SetActive(false);
    }


    private void ShowTitle(string Text, bool DebugMode = false)
    {
        DialogUI.SetActive(true);
        DialogTitle.text = Text;
        if (DebugMode)
        {
            Debug.Log(Text);
        }
    }

    private void SetActiveUI(bool Value)
    {
        DialogUI.SetActive(Value);
    }

    public void StartTutorial()
    {
        StartCoroutine(StartTutorialCoroutine());
    }

    IEnumerator WaitForClick()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;
    }

    IEnumerator WaitForCameraMoveInput()
    {
        yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)));
        yield return null;
    }

    IEnumerator WaitForAllySpawn()
    {
        Debug.Log("WaitForAllySpawn");
        int SaveallySpawnCount = EntityMng.Instance.allySpawnCount;
        yield return new WaitUntil(() => (SaveallySpawnCount != EntityMng.Instance.allySpawnCount));
        yield return null;
    }
    IEnumerator WaitForNoEnemy()
    {
        yield return new WaitUntil(() => (EntityMng.Instance.CurrentEnemyCount <= 0));
        yield return null;
    }

    IEnumerator WaitForGameEnd()
    {
        yield return new WaitUntil(() => (LobbyMng.Instance.GetisGameEnded()));
        yield return null;
    }




    IEnumerator StartTutorialCoroutine()
    {
        /* memo
        
        [���̴� ��� UI ����, ��� ��ȣ�ۿ� ��Ȱ��ȭ]
        Ʃ�丮�� ���Ű� ȯ���մϴ�!
        Welcome to the tutorial!
        
        �켱 ������ ��ǥ!
        First, our objective!

        [ī�޶� �� ������ ����]
        ���⿡ �ִ� ���� ������ �μ��⸸ �ϸ� �˴ϴ�, �� ����?
        All we need to do is destroy the enemy base over there. Sounds easy, right?

        �ϴ� �� ������ ���� �⺻���� ����Ű�� �˾ƾ߰���?
        But first, you need to learn the basic controls.

        [ī�޶� ����� �̵��� ������� ���� ����]
        ī�޶� �����̰� ���� A, D Ű�� ���� ������!
        Try pressing the A and D keys to move the camera!

        ���ϼ̽��ϴ�!
        Great job!

        �׸��� �츮�� �������� �Ʊ� ������ ��ȯ�� ���ô�
        Now, let's summon some friendly units from our base.

        [��Ȱ��ȭ �ߴ� UI �ٽ� Ű��]
        ���� �ؿ��ִ� ��ư���� ���ϴ� ������ ��ȯ �غ�����
        Go ahead and select any unit from the buttons below to summon it.

        ���� �鸶�� ������ HP�� �̵��ӵ��� ���� �մϴ�!
        Each unit has unique HP and movement speed!

        ���� ���ֵ� ���� ��ȯ �ڽ�Ʈ�� �ٸ��ϴ�
        Of course, different units also have different summon costs.

        [�� ��ȭ�� ������ ���� 3 ���� ���� ��ȯ]
        �� ���� �ѹ� �� ������ ��ȯ �غ��ڽ��ϴ� �ѹ� ���� ���� ������!!
        Now, I'll summon some enemy units. Try to stop them!

        (���� ����� �ְ�, �� ��ȯ, ���� �� ������ ���� ���)
        (Gives enough money, spawns enemies, waits until all enemies are defeated)

        ��!
        Nice!

        ���� �� ���·� ���� �ִ� ���� ������ ���ʶ߷� ������!
        Now, take this opportunity to bring down the enemy base over there!
         
         */

        //gameCamMng.CurrentState = GameCamMng.GameCamMngState.Default;

        yield return new WaitForSeconds(1f);

        IsDoingTutorial = true;

        GameCanvasMng.Instance.SemiSetActiveUI(false, "Main");

        gameCamMng.CurrentState = GameCamMng.GameCamMngState.Disabled;

        ShowTitle("Welcome to the tutorial!!");
        yield return WaitForClick();

        ShowTitle("First, our objective!");
        yield return WaitForClick();

        gameCamMng.CurrentState = GameCamMng.GameCamMngState.Custom;
        
        gameCamMng.CustomPosition = TutorialCameraPos.Find("EnemyBase").position;

        ShowTitle("All we need to do is destroy the enemy base over there. Sounds easy, right?");
        yield return WaitForClick();

        gameCamMng.CustomPosition = gameCamMng.saveCameraPos;

        ShowTitle("But first, you need to learn the basic controls.");
        yield return WaitForClick();


        

        ShowTitle("Try pressing the A and D keys to move the camera!");
        yield return WaitForCameraMoveInput();

        gameCamMng.CurrentState = GameCamMng.GameCamMngState.Default;


        ShowTitle("Great job!");
        yield return WaitForClick();

        ShowTitle("Now, let's summon some friendly units from our base.");
        yield return WaitForClick();

        GameCanvasMng.Instance.SemiSetActiveUI(true, "Main");

        LobbyMng.Instance.WhenTutorialTestGiveMoney1();

        ShowTitle("Go ahead and select any unit from the buttons below to summon it.");
        yield return WaitForAllySpawn();

        ShowTitle("Each unit has unique HP and movement speed!");
        yield return WaitForClick();

        ShowTitle("Of course, different units also have different summon costs.!");
        yield return WaitForClick();

        ShowTitle("Now, I'll summon some enemy units. Try to stop them!");
        yield return WaitForClick();
        LobbyMng.Instance.WhenTutorialTestGiveMoney2();
        SetActiveUI(false);

        int count = 3;

        for (int i = 0; i < count; i++)
        {
            EntityMng.Instance.TrySpawnEnemy(eEnemyType.Sword);
            yield return new WaitForSeconds(1f);
        }

        yield return WaitForNoEnemy();







        ShowTitle("Nice!");
        yield return WaitForClick();

        ShowTitle("Now, take this opportunity to bring down the enemy base over there!");
        yield return WaitForClick();

        SetActiveUI(false);
        yield return WaitForGameEnd();


        ShowTitle("The tutorial is over! Now go ahead and enjoy the game to your heart's content!");
        yield return WaitForClick();


        IsDoingTutorial = false;

        yield return null;
    }






}
