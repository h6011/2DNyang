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
        
        [보이는 모든 UI 끄기, 모든 상호작용 비활성화]
        튜토리얼에 오신걸 환영합니다!
        Welcome to the tutorial!
        
        우선 저희의 목표!
        First, our objective!

        [카메라 적 기지로 고정]
        저기에 있는 적의 기지를 부수기만 하면 됩니다, 참 쉽죠?
        All we need to do is destroy the enemy base over there. Sounds easy, right?

        일단 이 게임의 가장 기본적인 조작키를 알아야겠죠?
        But first, you need to learn the basic controls.

        [카메라 가운데로 이동후 원래대로 돌려 놓기]
        카메라를 움직이게 위해 A, D 키를 눌러 보세요!
        Try pressing the A and D keys to move the camera!

        잘하셨습니다!
        Great job!

        그리고 우리의 기지에서 아군 유닛을 소환해 봅시다
        Now, let's summon some friendly units from our base.

        [비활성화 했던 UI 다시 키기]
        여기 밑에있는 버튼들중 원하는 유닛을 소환 해보세요
        Go ahead and select any unit from the buttons below to summon it.

        유닛 들마다 고유의 HP와 이동속도가 존재 합니다!
        Each unit has unique HP and movement speed!

        물론 유닛들 마다 소환 코스트도 다릅니다
        Of course, different units also have different summon costs.

        [이 대화가 끝나면 적을 3 마리 정도 소환]
        자 제가 한번 적 유닛을 소환 해보겠습니다 한번 직접 막아 보세요!!
        Now, I'll summon some enemy units. Try to stop them!

        (돈을 충분히 주고, 적 소환, 적이 다 죽을때 까지 대기)
        (Gives enough money, spawns enemies, waits until all enemies are defeated)

        굳!
        Nice!

        이제 이 상태로 저기 있는 적의 기지를 무너뜨려 보세요!
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
