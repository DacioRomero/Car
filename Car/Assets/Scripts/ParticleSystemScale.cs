using UnityEngine;
using System.Collections;

public class ParticleSystemScale : MonoBehaviour
{
	public ParticleSystem[] particleSystems;
	[HideInInspector]
	public float percent = 1;
	float[] lifeTimes;

	void Awake ()
	{ 
		lifeTimes = new float[particleSystems.Length];
		
		for (short i = 0; i < particleSystems.Length; i++) {
			lifeTimes [i] = particleSystems [i].startLifetime;
			particleSystems [i].Play ();
			Debug.Log (i + " lifetime is  " + lifeTimes [i]);
		}
	}

	void Update () {
		for (short i = 0; i < particleSystems.Length; i++) {
			if (!particleSystems [i].isPlaying) {
				if (percent > 0f) {
					particleSystems [i].Play (false);
					particleSystems [i].startLifetime = lifeTimes [i] * percent;
				}
			} 

			else {
				if (percent == 0) {
					particleSystems [i].Stop (false);
				} else {
					particleSystems [i].startLifetime = lifeTimes [i] * percent;
				}
			}
		}
	}
}