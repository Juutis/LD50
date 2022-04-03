using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryTruck : MonoBehaviour
{
    LoadingArea activeLoadingArea;
    DeliveryArea activeDeliveryArea;
    TutorialArea activeTutorialArea;

    int loadedGoods {
        get {
            return goods.Count;
        }
    }
    int maxLoad = 1;

    List<Goods> goods = new List<Goods>();
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        maxLoad = 1 + GameManager.main.State.StorageLevel;
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.down * 0.75f;
    }

    void FixedUpdate() {
        rb.AddForce(Vector3.down * 10.0f, ForceMode.Acceleration);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetInteractKeyDown()) {
            if (activeLoadingArea != null) {
                if (canLoadMore()) {
                    loadGoods();
                }
            } 
        }

        if (activeDeliveryArea != null) {
            if (HasGoods()) {
                deliverGoods(activeDeliveryArea);
            }
        }

        if (activeLoadingArea != null) {
            if (canLoadMore()) {
                activeLoadingArea.SetReadyToLoad(true);
            } else {
                activeLoadingArea.SetReadyToLoad(false);
            }
        }

        if (activeDeliveryArea != null) {
            if (HasGoods()) {
                activeDeliveryArea.SetReadyToDeliver(true);
            } else {
                activeDeliveryArea.SetReadyToDeliver(false);
            }
        }

        foreach(var goods in goods) {
            goods.Update();
        }

        foreach(var goods in perishedGoods) {
            this.goods.Remove(goods);
        }
        perishedGoods.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        var loadingArea = other.GetComponent<LoadingArea>();
        if (loadingArea != null) {
            activeLoadingArea = loadingArea;
        }

        var deliveryArea = other.GetComponent<DeliveryArea>();
        if (deliveryArea != null) {
            activeDeliveryArea = deliveryArea;
        }
                
        var tutorialArea = other.GetComponent<TutorialArea>();
        if (tutorialArea != null) {
            tutorialArea.Show(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        var loadingArea = other.GetComponent<LoadingArea>();
        if (loadingArea != null) {
            activeLoadingArea.SetReadyToLoad(false);
            activeLoadingArea = null;
        }

        var deliveryArea = other.GetComponent<DeliveryArea>();
        if (deliveryArea != null) {
            activeDeliveryArea.SetReadyToDeliver(false);
            activeDeliveryArea = null;
        }
        
        var tutorialArea = other.GetComponent<TutorialArea>();
        if (tutorialArea != null) {
            tutorialArea.Show(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Prop") {
            var otherRb = collision.gameObject.GetComponent<Rigidbody>();
            otherRb.AddForce(collision.relativeVelocity / 2.0f, ForceMode.VelocityChange);
        }

        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.relativeVelocity.magnitude > 2) {
            //
        }
    }

    bool GetInteractKeyDown() {
        return Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E);
    }

    bool canLoadMore() {
        return loadedGoods < maxLoad;
    }

    float pickupForce = 200.0f;
    void loadGoods() {
        var newGoods = new Goods(this);
        goods.Add(newGoods);
        GoodsUI.main.AddGoods(newGoods);
        rb.AddForce(Vector3.down * pickupForce, ForceMode.Acceleration);
    }

    void deliverGoods(DeliveryArea area) {
        var deliveredGoods = goods[0];
        deliveredGoods.Deliver();
        goods.RemoveAt(0);
        area.Delivered();
        GameManager.main.SuccessfulDeliveries++;
    }

    private List<Goods> perishedGoods = new List<Goods>();

    public void GoodsPerished(Goods goods) {
        perishedGoods.Add(goods);
        GameManager.main.SpoiledDeliveries++;
    }

    public bool HasGoods() {
        return loadedGoods > 0;
    }
}


public class Goods {
    float decayRate = 1.0f;
    float health;
    float maxHealth = 30.0f;

    float baseHealth = 30.0f;
    float healthPerLevel = 20.0f;

    DeliveryTruck truck;
    
    public bool IsPerished;

    public float Goodness {
        get {
            return Mathf.Clamp(health / maxHealth, 0.0f, 1.0f);
        }
    }

    public bool IsDelivered;

    public Goods(DeliveryTruck truck) {
        maxHealth = baseHealth + GameManager.main.State.CoolerLevel * healthPerLevel;
        health = maxHealth;
        this.truck = truck;
    }

    public void Update() {
        health -= Time.deltaTime * decayRate;
        if (health <= 0.0f) {
            Perish();
        }
    }

    public void Perish() {
        IsPerished = true;
        truck.GoodsPerished(this);
    }

    public void Deliver() {
        IsDelivered = true;
    }
}