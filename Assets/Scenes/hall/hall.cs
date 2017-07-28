using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetWork;
using Room;
using Login;

public class hall : MonoBehaviour {
	private NetClient network;

	// Use this for initialization
	void Start () {
		try{
			Debug.Log("hall is starting...");
			network = NetClient.Instance ();

			Room.RoomListReq req = new Room.RoomListReq ();
			network.WriteMsg("Room.RoomListReq", req);
		}catch(Exception e){
			Debug.Log (e.ToString());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		NetWork.Msg msg = network.PeekMsg ();
		if (msg == null)
			return;

		if (msg.name == "Room.RoomListRsp") {
			onRoomList (msg);
		} else if (msg.name == "Room.EnterRsp") {
			onEnter (msg);
		}
	}

	void onRoomList(NetWork.Msg msg){
		Room.RoomListRsp rsp = (Room.RoomListRsp)msg.body;
		Debug.Log ("Room.RoomListRsp..."+ rsp);
		Debug.Log("Get Room List....");
		foreach (Room.RoomInfo room in rsp.list)
		{
			Debug.Log("Room:"+ room.ToString());
		}
		Debug.Log("Over....");

		Room.EnterReq req = new Room.EnterReq ();
		req.room_id = 1;
		network.WriteMsg ("Room.EnterReq",req);

	}

	void onEnter(NetWork.Msg msg){
		Debug.Log("Enter Room....");
		UnityEngine.SceneManagement.SceneManager.LoadScene ("playing");
	}
}
