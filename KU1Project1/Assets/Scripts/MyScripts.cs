using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScripts : MonoBehaviour
{	
	public string myName = "Kanittha";
    // Start is called before the first frame update
    void Start()
    {
        print("Hello " + myName);
    }
	
	private void Awake()
	{
		print("Awake !");  //ใช้ค้นหาอะไรบางอย่าง
	}

    // Update is called once per frame
    void Update()
    {
        print("Hello Update !");
    }
}
