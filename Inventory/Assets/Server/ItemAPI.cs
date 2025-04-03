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
	[ExecuteInEditMode]
	public class ItemAPI : MonoBehaviour
	{
		private ConfigLoader _configLoader;

		public void SetConfig(ConfigLoader configLoader)
		{
			_configLoader = configLoader;
		}

		public void API_Create(Item item, Action<bool> responseAction = null)
		{
			StartCoroutine(CreateItemCO(item, responseAction));
		}

		public void API_Get(string itemId, Action<ItemBase> responseAction = null)
		{
			StartCoroutine(GetItemCO(itemId, responseAction));
		}

		public void API_GetAll(Action<IEnumerable<ItemBase>> responseAction = null)
		{
			StartCoroutine(GetItemsCO(responseAction));
		}

		public void API_Update(Item item, Action<bool> responseAction = null)
		{
			StartCoroutine(UpdateItemCO(item, responseAction));
		}

		public void API_Delete(string item, Action<bool> responseAction = null)
		{
			StartCoroutine(DeleteItemCO(item, responseAction));
		}

		private IEnumerator CreateItemCO(Item newItem, Action<bool> responseAction = null)
		{
			string endpoint = _configLoader.ItemEndpoints[EndPoints.Create];
			string jsonData = JsonConvert.SerializeObject(newItem);
			UnityWebRequest request =
				RequestManager.RequestUploadBuilder(_configLoader.Config.ApiUrl,
					endpoint,
					UnityWebRequest.kHttpVerbPOST,
					Encoding.UTF8.GetBytes(jsonData),
					true);
			yield return request.SendWebRequest();
			Debug.Log(request.result == UnityWebRequest.Result.Success
				? $"{newItem.ItemId} was created successfully"
				: "Error: " + request.error);
			responseAction?.Invoke(request.result == UnityWebRequest.Result.Success);
		}

		private IEnumerator GetItemCO(string itemId, Action<ItemBase> responseAction = null)
		{
			string endpoint = _configLoader.ItemEndpoints[EndPoints.Get];
			UnityWebRequest request =
				RequestManager.RequestQueryBuilder(_configLoader.Config.ApiUrl,
					endpoint+itemId,
					UnityWebRequest.kHttpVerbGET);
			yield return request.SendWebRequest();

			Debug.Log(request.result == UnityWebRequest.Result.Success
				? $"{itemId} was retrieved successfully"
				: "Error: " + request.error);
			ItemBase innerItem = null;
			if (request.result == UnityWebRequest.Result.Success)
			{
				string jsonResponse = request.downloadHandler.text;
				Item item = JsonConvert.DeserializeObject<Item>(jsonResponse);
				innerItem = ItemBase.DeserializeItem(item.ItemDescription);
			}
			responseAction?.Invoke(innerItem);
		}

		private IEnumerator GetItemsCO(Action<IEnumerable<ItemBase>> responseAction = null)
		{
			string endpoint = _configLoader.ItemEndpoints[EndPoints.GetAll];
			UnityWebRequest request =
				RequestManager.RequestQueryBuilder(_configLoader.Config.ApiUrl,
					endpoint,
					UnityWebRequest.kHttpVerbGET);
			yield return request.SendWebRequest();

			Debug.Log(request.result == UnityWebRequest.Result.Success
				? "all items were retrieved successfully"
				: "Error: " + request.error);
			IEnumerable<ItemBase> innerItems = null;
			if (request.result == UnityWebRequest.Result.Success)
			{
				string jsonResponse = request.downloadHandler.text;
				Item[] items = JsonConvert.DeserializeObject<Item[]>(jsonResponse);
				innerItems = items.Select(i => ItemBase.DeserializeItem(i.ItemDescription));
			}
			responseAction?.Invoke(innerItems);
		}

		private IEnumerator UpdateItemCO(Item item, Action<bool> responseAction = null)
		{
			string endpoint = _configLoader.ItemEndpoints[EndPoints.Update];
			string jsonData = JsonConvert.SerializeObject(item);
			UnityWebRequest request =
				RequestManager.RequestUploadBuilder(_configLoader.Config.ApiUrl,
					endpoint,
					UnityWebRequest.kHttpVerbPUT,
					Encoding.UTF8.GetBytes(jsonData),
					true);
			yield return request.SendWebRequest();

			Debug.Log(request.result == UnityWebRequest.Result.Success
				? $"{item.ItemId} was updated successfully"
				: "Error: " + request.error);
			responseAction?.Invoke(request.result == UnityWebRequest.Result.Success);
		}

		private IEnumerator DeleteItemCO(string itemId, Action<bool> responseAction = null)
		{
			string endpoint = _configLoader.ItemEndpoints[EndPoints.Delete];
			UnityWebRequest request =
				RequestManager.RequestQueryBuilder(_configLoader.Config.ApiUrl,
					endpoint+itemId,
					UnityWebRequest.kHttpVerbDELETE);
			yield return request.SendWebRequest();

			Debug.Log(request.result == UnityWebRequest.Result.Success
				? $"{itemId} was deleted successfully"
				: "Error: " + request.error);
			responseAction?.Invoke(request.result == UnityWebRequest.Result.Success);
		}
	}

	[Serializable]
	public class Item
	{
		public string ItemId { get; set; } = "";
		public string ItemDescription { get; set; } = "";
	}
}