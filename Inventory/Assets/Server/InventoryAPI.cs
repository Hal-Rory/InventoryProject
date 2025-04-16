using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controllers;
using Newtonsoft.Json;
using ServerItems;
using UnityEngine;
using UnityEngine.Networking;

namespace Server
{
    public class InventoryAPI : MonoBehaviour
    {
	    private ConfigLoader _configLoader;

	    public void SetConfig(ConfigLoader configLoader)
	    {
		    _configLoader = configLoader;
	    }
	    public void API_Create(InventoryItem inventory, Action<bool> responseAction = null)
		{
			StartCoroutine(CreatePlayerItemCO(inventory, responseAction));
		}

		public void API_Get(int playerId, string itemId, Action<PlayerItem> responseAction = null)
		{
			StartCoroutine(GetPlayerItemCO(playerId, itemId, responseAction));
		}

		public void API_GetAll(int playerId, Action<IEnumerable<PlayerItem>> responseAction = null)
		{
			StartCoroutine(GetPlayerItemsCO(playerId, responseAction));
		}

		public void API_Update(InventoryItem item, Action<bool> responseAction = null)
		{
			StartCoroutine(UpdatePlayerItemCO(item, responseAction));
		}

		public void API_UpdateAll(InventoryItem[] item, Action<bool> responseAction = null)
		{
			StartCoroutine(UpdatePlayerItemsCO(item, responseAction));
		}

		public void API_Delete(int playerId, string itemId, Action<bool> responseAction = null)
		{
			StartCoroutine(DeletePlayerItemCO(playerId, itemId, responseAction));
		}

		private IEnumerator CreatePlayerItemCO(InventoryItem newInventoryItem, Action<bool> responseAction = null)
		{
			string endpoint = _configLoader.InventoryEndpoints[EndPoints.Create];
			string jsonData = JsonConvert.SerializeObject(newInventoryItem);
			UnityWebRequest request =
				RequestManager.RequestUploadBuilder(_configLoader.Config.ApiUrl,
					endpoint,
					UnityWebRequest.kHttpVerbPOST,
					Encoding.UTF8.GetBytes(jsonData),
					true);
			yield return request.SendWebRequest();
			Debug.Log(request.result == UnityWebRequest.Result.Success
				? $"{newInventoryItem.Player}:{newInventoryItem.Item} was created successfully"
				: "Error: " + request.error);
			responseAction?.Invoke(request.result == UnityWebRequest.Result.Success);
		}

		private IEnumerator GetPlayerItemCO(int playerId, string itemId, Action<PlayerItem> responseAction = null)
		{
			string endpoint = _configLoader.InventoryEndpoints[EndPoints.Get];
			UnityWebRequest request =
				RequestManager.RequestQueryBuilder(_configLoader.Config.ApiUrl,
					string.Format(endpoint, playerId, itemId),
					UnityWebRequest.kHttpVerbGET);
			yield return request.SendWebRequest();
			Debug.Log(request.result == UnityWebRequest.Result.Success
				? $"{playerId}:{itemId} was retrieved successfully"
				: "Error: " + request.error);

			PlayerItem playerItem = null;
			if (request.result == UnityWebRequest.Result.Success)
			{
				string jsonResponse = request.downloadHandler.text;
				playerItem = JsonConvert.DeserializeObject<PlayerItem>(jsonResponse);
				playerItem.Item = ItemBase.DeserializeItem(playerItem.ItemDescription);
			}
			responseAction?.Invoke(playerItem);
		}

		private IEnumerator GetPlayerItemsCO(int playerId, Action<PlayerItem[]> responseAction = null)
		{
			string endpoint = _configLoader.InventoryEndpoints[EndPoints.GetAll];
			UnityWebRequest request =
				RequestManager.RequestQueryBuilder(_configLoader.Config.ApiUrl,
					endpoint+playerId,
					UnityWebRequest.kHttpVerbGET);
			yield return request.SendWebRequest();
			Debug.Log(request.result == UnityWebRequest.Result.Success
				? $"all items for Player {playerId} were retrieved successfully"
				: "Error: " + request.error);

			PlayerItem[] items = null;
			if (request.result == UnityWebRequest.Result.Success)
			{
				string jsonResponse = request.downloadHandler.text;
				items = JsonConvert.DeserializeObject<PlayerItem[]>(jsonResponse);
				foreach (PlayerItem item in items)
				{
					item.Item = ItemBase.DeserializeItem(item.ItemDescription);
					print(item.Item.ItemName + " " + item.ItemQuantity);
				}
			}
			responseAction?.Invoke(items);
		}

		private IEnumerator UpdatePlayerItemCO(InventoryItem item, Action<bool> responseAction = null)
		{
			string endpoint = _configLoader.InventoryEndpoints[EndPoints.Update];
			string jsonData = JsonConvert.SerializeObject(item);
			UnityWebRequest request =
				RequestManager.RequestUploadBuilder(_configLoader.Config.ApiUrl,
					endpoint,
					UnityWebRequest.kHttpVerbPOST,
					Encoding.UTF8.GetBytes(jsonData),
					true);
			yield return request.SendWebRequest();

			Debug.Log(request.result == UnityWebRequest.Result.Success
				? $"{item.Player}:{item.Item} was updated successfully"
				: "Error: " + request.error);
			responseAction?.Invoke(request.result == UnityWebRequest.Result.Success);
		}

		private IEnumerator UpdatePlayerItemsCO(InventoryItem[] items, Action<bool> responseAction = null)
		{
			string endpoint = _configLoader.InventoryEndpoints[EndPoints.UpdateAll];
			string jsonData = JsonConvert.SerializeObject(items);
			UnityWebRequest request =
				RequestManager.RequestUploadBuilder(_configLoader.Config.ApiUrl,
					endpoint,
					UnityWebRequest.kHttpVerbPOST,
					Encoding.UTF8.GetBytes(jsonData),
					true);
			yield return request.SendWebRequest();

			Debug.Log(request.result == UnityWebRequest.Result.Success
				? "Items modified successfully"
				: "Error: " + request.error);
			responseAction?.Invoke(request.result == UnityWebRequest.Result.Success);
		}

		private IEnumerator DeletePlayerItemCO(int playerId, string itemId, Action<bool> responseAction = null)
		{
			string endpoint = _configLoader.InventoryEndpoints[EndPoints.Delete];
			UnityWebRequest request =
				RequestManager.RequestQueryBuilder(_configLoader.Config.ApiUrl,
					string.Format(endpoint, playerId, itemId),
					UnityWebRequest.kHttpVerbDELETE);
			yield return request.SendWebRequest();

			Debug.Log(request.result == UnityWebRequest.Result.Success
				? $"{playerId}:{itemId} was deleted successfully"
				: "Error: " + request.error);
			responseAction?.Invoke(request.result == UnityWebRequest.Result.Success);
		}
	}

	[Serializable]
	public class InventoryItem
	{
		public int Player { get; set; } = 0;
		public string Item { get; set; } = "";
		public int ItemQuantity { get; set; } = 0;
	}

	[Serializable]
	public class PlayerItem
	{
		public string ItemId { get; set; } = "";
		public string ItemDescription { get; set; } = "";
		public int ItemQuantity { get; set; } = 0;
		[JsonIgnore]
		public ItemBase Item { get; set; }
	}
}