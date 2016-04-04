using UnityEngine;
using System.Collections;

public class MapTile : MonoBehaviour {

	Sprites spriteScript;

	// Use this for initialization
	void Start () {
	
		spriteScript = GameObject.Find("SpritesHolder").GetComponent<Sprites>();

		GameObject childSprite = this.transform.GetChild(0).gameObject;
		SpriteRenderer tile = childSprite.GetComponent<SpriteRenderer>();
		tile.sprite = spriteScript.GetTileSprite();

		int rndRot = Random.Range(0, 4);

		if(rndRot == 0)
			childSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
		if(rndRot == 1)
			childSprite.transform.rotation = Quaternion.Euler(0, 0, 90);
		if(rndRot == 2)
			childSprite.transform.rotation = Quaternion.Euler(0, 0, 180);
		if(rndRot == 3)
			childSprite.transform.rotation = Quaternion.Euler(0, 0, 270);
	}
}
