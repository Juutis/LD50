using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    public DeliveryTruck Truck;

    public WinScreen Win;
    public WinScreen SuperWin;
    public LoseScreen Lose;
    public MenuScreen Menu;

    public int DeliveryAreas = 0;
    public GameState State;

    public List<GameObject> Levels;

    bool winnable = false;

    public int SuccessfulDeliveries, SpoiledDeliveries;

    int moneyPerSuccess = 10, moneyPerFail = 5;

    public GameObject DeathCamera;

    void Awake() {
        main = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Win.gameObject.SetActive(false);
        SuperWin.gameObject.SetActive(false);
        Lose.gameObject.SetActive(false);
        Menu.gameObject.SetActive(false);
        Instantiate(Levels[State.CurrentLevelIndex]);
        Invoke("EnableWin", 5.0f);
        Truck = GameObject.FindGameObjectWithTag("Player").GetComponent<DeliveryTruck>();
        DeathCamera = GameObject.FindGameObjectWithTag("DieCam");
        DeathCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (winnable && DeliveryAreas <= 0) {
            var winScreen = State.CurrentLevelIndex == 3 ? SuperWin : Win;
            if (State.CurrentLevelIndex + 1 > State.LevelsPassed) {
                State.LevelsPassed = State.CurrentLevelIndex + 1;
            }
            winScreen.gameObject.SetActive(true);
            winScreen.Deliveries = SuccessfulDeliveries;
            winScreen.SpoiledDeliveries = SpoiledDeliveries;
            var moneyMade = SuccessfulDeliveries * moneyPerSuccess;
            var moneyLost = SpoiledDeliveries * moneyPerFail;
            winScreen.MoneyMade = moneyMade;
            winScreen.MoneyLost = moneyLost;
            winScreen.UpdateTexts = true;
            State.Money = State.Money + Mathf.Max(0, moneyMade-moneyLost);
            winnable = false;
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            if (!Lose.gameObject.activeInHierarchy && !Win.gameObject.activeInHierarchy && !SuperWin.gameObject.activeInHierarchy && !Menu.gameObject.activeInHierarchy) {
                Menu.gameObject.SetActive(true);
            }
        }
    }

    void EnableWin() {
        winnable = true;
    }

    public void GameOver() {
        if (!Lose.gameObject.activeInHierarchy && !Win.gameObject.activeInHierarchy && !SuperWin.gameObject.activeInHierarchy) {
            Lose.gameObject.SetActive(true);
            Menu.gameObject.SetActive(false);
        }
        DeathCamera.SetActive(true);
    }
}
