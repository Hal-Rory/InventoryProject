using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Controllers;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Server
{
	public class PlayerAPI : MonoBehaviour
	{
		public void API_Create(Player player, Action<bool> responseAction = null)
		{
			StartCoroutine(CreatePlayerCO(player, responseAction));
		}

		public void API_Get(int playerId, Action<Player> responseAction = null)
		{
			StartCoroutine(GetPlayerCO(playerId, responseAction));
		}

		public void API_GetAll(Action<IEnumerable<Player>> responseAction = null)
		{
			StartCoroutine(GetPlayersCO(responseAction));
		}

		public void API_Delete(int playerId, Action<bool> responseAction = null)
		{
			StartCoroutine(DeletePlayerCO(playerId, responseAction));
		}

		private IEnumerator CreatePlayerCO(Player newPlayer, Action<bool> responseAction = null)
		{
			string endpoint = GameController.Instance.ConfigLoader.PlayerEndpoints[EndPoints.Create];
			string jsonData = JsonConvert.SerializeObject(newPlayer);
			UnityWebRequest request =
				RequestManager.RequestUploadBuilder(
					endpoint,
					UnityWebRequest.kHttpVerbPOST,
					Encoding.UTF8.GetBytes(jsonData),
					true);
			yield return request.SendWebRequest();
			Debug.Log(request.result == UnityWebRequest.Result.Success
				? $"{newPlayer.PlayerId} was created successfully"
				: "Error: " + request.error);
			responseAction?.Invoke(request.result == UnityWebRequest.Result.Success);
		}

		private IEnumerator GetPlayerCO(int playerId, Action<Player> responseAction = null)
		{
			string endpoint = GameController.Instance.ConfigLoader.PlayerEndpoints[EndPoints.Get];
			UnityWebRequest request =
				RequestManager.RequestQueryBuilder(
					endpoint+playerId,
					UnityWebRequest.kHttpVerbGET);
			yield return request.SendWebRequest();

			Debug.Log(request.result == UnityWebRequest.Result.Success
				? $"{playerId} was retrieved successfully"
				: "Error: " + request.error);
			Player player = null;
			if (request.result == UnityWebRequest.Result.Success)
			{
				string jsonResponse = request.downloadHandler.text;
				player = JsonConvert.DeserializeObject<Player>(jsonResponse);
			}
			responseAction?.Invoke(player);
		}

		private IEnumerator GetPlayersCO(Action<IEnumerable<Player>> responseAction = null)
		{
			string endpoint = GameController.Instance.ConfigLoader.PlayerEndpoints[EndPoints.GetAll];
			UnityWebRequest request =
				RequestManager.RequestQueryBuilder(
					endpoint,
					UnityWebRequest.kHttpVerbGET);
			yield return request.SendWebRequest();

			Debug.Log(request.result == UnityWebRequest.Result.Success
				? "all items were retrieved successfully"
				: "Error: " + request.error);
			Player[] players = null;
			if (request.result == UnityWebRequest.Result.Success)
			{
				string jsonResponse = request.downloadHandler.text;
				players = JsonConvert.DeserializeObject<Player[]>(jsonResponse);
			}
			responseAction?.Invoke(players);
		}

		private IEnumerator DeletePlayerCO(int playerId, Action<bool> responseAction = null)
		{
			string endpoint = GameController.Instance.ConfigLoader.PlayerEndpoints[EndPoints.Delete];
			UnityWebRequest request =
				RequestManager.RequestQueryBuilder(
					endpoint+playerId,
					UnityWebRequest.kHttpVerbDELETE);
			yield return request.SendWebRequest();

			Debug.Log(request.result == UnityWebRequest.Result.Success
				? $"{playerId} was deleted successfully"
				: "Error: " + request.error);
			responseAction?.Invoke(request.result == UnityWebRequest.Result.Success);
		}
	}

	[Serializable]
	public class Player
	{
		public int PlayerId { get; set; } = 0;
		public string PlayerName { get; set; } = "";

		[JsonIgnore]
		public HashSet<string> Items;
	}
}