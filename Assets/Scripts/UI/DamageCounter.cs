using UnityEngine.UI;
using UnityEngine;

public class DamageCounter : MonoBehaviour {

    public PlayerController damageSource;
    Text textbox;

    void Start()
    {
        textbox = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		if (damageSource!= null)
        {
            textbox.text = ((int)damageSource.currDamage).ToString() + "%";
        }
	}
}
