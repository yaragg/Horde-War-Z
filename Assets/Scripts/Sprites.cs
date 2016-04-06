using UnityEngine;
using System.Collections;

public class Sprites : MonoBehaviour {

	public Sprite [] spriteTiles = new Sprite[20];

	public Sprite [] spriteZombiesM = new Sprite[3];
	public Sprite [] spriteZombiesF = new Sprite[2];

	public Sprite GetTileSprite()
	{
		int rnd = Random.Range(0, spriteTiles.Length);

		return spriteTiles[rnd];
	}

	public Sprite GetZombieSprite(char genderChar)
	{
		//if male caller...
		if (genderChar == 'M')
		{
			int rnd = Random.Range(0, spriteZombiesM.Length);
			 return spriteZombiesM[rnd];
		}

		//else...
		else
		{
			int rnd = Random.Range(0, spriteZombiesF.Length);
			return spriteZombiesF[rnd];
		}
	}
	
}
