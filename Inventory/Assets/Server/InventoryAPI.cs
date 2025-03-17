using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ServerItems;
using UnityEngine;
using UnityEngine.Networking;

namespace Server
{
	public class InventoryAPI : MonoBehaviour
	{
		private IEnumerator Start()
		{
			yield return new WaitForSeconds(2);
			//StartCoroutine(GetItemCO("sw001"));
			StartCoroutine(GetItemsCO());
		}

		public void CreateItem(Item item, Action<bool> responseAction = null)
		{
			StartCoroutine(CreateItemCO(item, responseAction));
		}

		public void GetItem(string item, Action<ItemBase> responseAction = null)
		{
			StartCoroutine(GetItemCO(item, responseAction));
		}

		public void GetAllItem(Action<IEnumerable<ItemBase>> responseAction = null)
		{
			StartCoroutine(GetItemsCO(responseAction));
		}

		public void DeleteItem(string item, Action<bool> responseAction = null)
		{
			//StartCoroutine(CreateItemCO(item, responseAction));
		}

		private IEnumerator CreateItemCO(Item newItem, Action<bool> responseAction = null)
		{
			string jsonData = JsonConvert.SerializeObject(newItem);
			UnityWebRequest request =
				RequestManager.RequestUploadBuilder(
					ConfigLoader.Config.Construct(ConfigLoader.Config.Items, EndPoints.Create),
					UnityWebRequest.kHttpVerbPOST,
					Encoding.UTF8.GetBytes(jsonData),
					true);
			yield return request.SendWebRequest();
			Debug.Log(request.result == UnityWebRequest.Result.Success
				? $"{newItem.ItemId} was created successfully"
				: "Error: " + request.error);
			responseAction?.Invoke(request.result == UnityWebRequest.Result.Success);
		}

		private IEnumerator GetItemCO(string newItem, Action<ItemBase> responseAction = null)
		{
			UnityWebRequest request =
				RequestManager.RequestDownloadBuilder(
					ConfigLoader.Config.Construct(ConfigLoader.Config.Items, EndPoints.Get),
					UnityWebRequest.kHttpVerbGET,
					newItem);
			yield return request.SendWebRequest();

			if (request.result != UnityWebRequest.Result.Success) yield break;
			Debug.Log(request.result == UnityWebRequest.Result.Success
				? $"{newItem} was retrieved successfully"
				: "Error: " + request.error);
			string jsonResponse = request.downloadHandler.text;
			Item item = JsonConvert.DeserializeObject<Item>(jsonResponse);
			ItemBase innerItem = JsonConvert.DeserializeObject<ItemBase>(item.ItemDescription);

			responseAction?.Invoke(innerItem);
		}

		private IEnumerator GetItemsCO(Action<IEnumerable<ItemBase>> responseAction = null)
		{
			UnityWebRequest request =
				RequestManager.RequestDownloadBuilder(
					ConfigLoader.Config.Construct(ConfigLoader.Config.Items, EndPoints.GetAll),
					UnityWebRequest.kHttpVerbGET);
			yield return request.SendWebRequest();

			if (request.result != UnityWebRequest.Result.Success) yield break;
			Debug.Log(request.result == UnityWebRequest.Result.Success
				? "all items were retrieved successfully"
				: "Error: " + request.error);
			string jsonResponse = request.downloadHandler.text;
			Item[] item = JsonConvert.DeserializeObject<Item[]>(jsonResponse);
			IEnumerable<ItemBase> innerItem = item.Select(i =>
				JsonConvert.DeserializeObject<ItemBase>(i.ItemDescription, ServerUtilities.JsonSerializer));
			responseAction?.Invoke(innerItem);
		}
	}
}

[Serializable]
	public class Item
	{
		public string ItemId { get; set; } = "";
		public string ItemDescription { get; set; } = "";
	}