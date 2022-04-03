using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryArea : MonoBehaviour
{
    public GameObject Area;
    public GameObject loadEffect;
    public Transform stall;
    MinimapArrow arrow;

    // Start is called before the first frame update
    void Start()
    {
        arrow = GetComponentInChildren<MinimapArrow>();
        loadEffect.SetActive(false);
        GameManager.main.DeliveryAreas++;
        stall.parent = transform.parent;
        Area.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.main.Truck.HasGoods()) {
            activateEffects();
        }
        else {
            disableEffects();
        }
    }

    public void Delivered() {
        Destroy(gameObject);
        GameManager.main.DeliveryAreas--;
    }

    void activateEffects() {
        arrow.Show = true;
        Area.SetActive(true);
    }

    void disableEffects() {
        arrow.Show = false;
        Area.SetActive(false);
    }

    public void SetReadyToDeliver(bool ready) {
        if (ready) {
            loadEffect.SetActive(true);
        } else {
            loadEffect.SetActive(false);
        }
    }
}
