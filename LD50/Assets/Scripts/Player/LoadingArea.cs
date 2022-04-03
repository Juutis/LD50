using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingArea : MonoBehaviour
{
    MinimapArrow arrow;

    public GameObject loadEffect;

    // Start is called before the first frame update
    void Start()
    {
        arrow = GetComponentInChildren<MinimapArrow>();
        loadEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.main.Truck.HasGoods()) {
            activateEffects();
        }
        else {
            disableEffects();
        }
    }

    void activateEffects() {
        arrow.Show = true;
    }

    void disableEffects() {
        arrow.Show = false;
    }

    public void SetReadyToLoad(bool ready) {
        if (ready) {
            loadEffect.SetActive(true);
        } else {
            loadEffect.SetActive(false);
        }
    }
}
