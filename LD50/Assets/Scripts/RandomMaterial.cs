using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterial : MonoBehaviour
{
    public List<Material> Materials;
    public Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend.material = Materials[Random.Range(0, Materials.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
