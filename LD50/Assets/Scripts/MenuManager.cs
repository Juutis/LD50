using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameState State;

    public List<Button> LevelSelect;

    public TextMeshProUGUI Balance;

    public List<Button> CoolerUpgrades;
    public List<Button> StorageUpgrades;
    public List<Button> EngineUpgrades;

    List<int> CoolerCosts = new List<int>(){ 10, 20, 30 };
    List<int> StorageCosts = new List<int>(){ 10, 20, 30 };
    List<int> EngineCosts = new List<int>(){ 10, 20, 30 };


    // Start is called before the first frame update
    void Start()
    {
        enableLevels();
        enableUpgrades();
    }

    // Update is called once per frame
    void Update()
    {
        Balance.text = "$" + State.Money;
    }

    void enableLevels() {
        for (var i = 0; i < LevelSelect.Count; i++) {
            var button = LevelSelect[i];
            button.interactable = i <= State.LevelsPassed;
        }
    }

    void enableUpgrades() {
        for (var i = 0; i < CoolerUpgrades.Count; i++) {
            var button = CoolerUpgrades[i];
            button.interactable = i == State.CoolerLevel && State.Money >= CoolerCosts[i];
            if (i < State.CoolerLevel) {
                button.transform.parent.Find("Stamp").gameObject.SetActive(true);
                button.transform.Find("Cost").gameObject.SetActive(false);
            }
            else {
                button.transform.Find("Cost").GetComponentInChildren<TextMeshProUGUI>().text = "$" + CoolerCosts[i];
            }
        }
        for (var i = 0; i < StorageUpgrades.Count; i++) {
            var button = StorageUpgrades[i];
            button.interactable = i == State.StorageLevel && State.Money >= StorageCosts[i];
            if (i < State.StorageLevel) {
                button.transform.parent.Find("Stamp").gameObject.SetActive(true);
                button.transform.Find("Cost").gameObject.SetActive(false);
            }
            else {
                button.transform.Find("Cost").GetComponentInChildren<TextMeshProUGUI>().text = "$" + StorageCosts[i];
            }
        }
        for (var i = 0; i < EngineUpgrades.Count; i++) {
            var button = EngineUpgrades[i];
            button.interactable = i == State.EngineLevel && State.Money >= EngineCosts[i];
            if (i < State.EngineLevel) {
                button.transform.parent.Find("Stamp").gameObject.SetActive(true);
                button.transform.Find("Cost").gameObject.SetActive(false);
            }
            else {
                button.transform.Find("Cost").GetComponentInChildren<TextMeshProUGUI>().text = "$" + EngineCosts[i];
            }
        }
    }

    public void LoadLevel(int levelNumber) {
        SceneManager.LoadScene("SampleScene");
        State.CurrentLevelIndex = levelNumber;
    }

    public void UpgradeCooler(int level) {
        State.Money -= CoolerCosts[level - 1];
        State.CoolerLevel = level;
        enableUpgrades();
    }

    public void UpgradeStorage(int level) {
        State.Money -= StorageCosts[level - 1];
        State.StorageLevel = level;
        enableUpgrades();
    }

    public void UpgradeEngine(int level) {
        State.Money -= EngineCosts[level - 1];
        State.EngineLevel = level;
        enableUpgrades();
    }
}
