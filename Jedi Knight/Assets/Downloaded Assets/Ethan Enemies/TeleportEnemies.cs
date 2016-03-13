﻿// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[RequireComponent(typeof(Collider))]
public class TeleportEnemies : MonoBehaviour {
	public Transform ethan;
	public Transform prefab;

  private Vector3 startingPosition;

  void Start() {
    startingPosition = transform.localPosition;
	transform.LookAt(ethan);
    SetGazedAt(false);
  }

  void LateUpdate() {
    Cardboard.SDK.UpdateState();
    if (Cardboard.SDK.BackButtonPressed) {
      Application.Quit();
    }
  }

  public void SetGazedAt(bool gazedAt) {
    GetComponent<Renderer>().material.color = gazedAt ? Color.red : Color.green;
	AudioSource sound = gameObject.GetComponent<AudioSource>();
	//sound.Play();
  }

  public void Reset() {
    transform.localPosition = startingPosition;
  }

  public void ToggleVRMode() {
    Cardboard.SDK.VRModeEnabled = !Cardboard.SDK.VRModeEnabled;
  }


	public void ReSpawn(){

		Instantiate (prefab);
		Vector3 direction = Random.onUnitSphere;
		//direction.y = Mathf.Clamp(direction.y, 0.5f, 1f);
		//float distance = 2 * Random.value + 1.5f;
		float howFarAwayIsEthan = Vector3.Distance(ethan.position, this.transform.position);
		transform.localPosition = direction * howFarAwayIsEthan;
		transform.LookAt(ethan);

	}

  public void TeleportRandomly() {
	Debug.Log ("I am teleporting randomly");

	Vector3 direction = Random.onUnitSphere;
    //direction.y = Mathf.Clamp(direction.y, 0.5f, 1f);
    //float distance = 2 * Random.value + 1.5f;
	float howFarAwayIsEthan = Vector3.Distance(ethan.position, this.transform.position);
	//have to change teleport position to one of the random ones around ethan
	transform.localPosition = direction * howFarAwayIsEthan;
	transform.LookAt(ethan);
	
  }


	public void OnTriggerEnter (Collider other){
		if ((other.gameObject.name == "Beam")) {
			TeleportRandomly ();		

		}


	}


}