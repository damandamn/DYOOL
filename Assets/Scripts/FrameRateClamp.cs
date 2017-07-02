using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateClamp : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;
	}
}
