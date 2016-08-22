using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class TestLambda : MonoBehaviour {

    int[] list = { 1, 7, 5, 9, 1, 5, 7, 221, 89, 4, 2, 8, 7, 52, 4, 8, 2, 7, 8, 2, 11, 8, 69, 2, 4, 8, 5, 2, 0, 5, 4, 6, 32, 7, 6, 2, 8, 2, 69, 8, 4, 2, 6, 58, 4, 26, 8, 7, 51, 326, 584, 94, 616, 57, 561, 51, 654 };

    // Use this for initialization
    void Start() {

        int[] test = list.OrderByDescending(x => x).ToArray();
     
     
       foreach (var x in test)
           print(x);


    }

    // Update is called once per frame
    void Update() {

    }
}
