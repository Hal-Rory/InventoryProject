using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Server
{
	public class ItemAPI : MonoBehaviour
	{
		public void CreateItem(Item item, Action<bool> responseAction = null)
		{
			StartCoroutine(CreateItemCO(item, responseAction));
		}

		public void GetItem(string item, Action<ItemBase> responseAction = null)
		{
			StartCoroutine(GetItemCO(item, responseAction));
		}

		public void GetAllItems(Action<IEnumerable<ItemBase>> responseAction = null)
		{
			StartCoroutine(GetItemsCO(responseAction));
		}

		public void DeleteItem(string item, Action<bool> responseAction = null)
		{
			StartCoroutine(DeleteItemCO(item, responseAction));
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
				RequestManager.RequestQueryBuilder(
					ConfigLoader.Config.Construct(ConfigLoader.Config.Items, EndPoints.Get),
					UnityWebRequest.kHttpVerbGET,
					newItem);
			yield return request.SendWebRequest();

			Debug.Log(request.result == UnityWebRequest.Result.Success
				? $"{newItem} was retrieved successfully"
				: "Error: " + request.error);
			if (request.result != UnityWebRequest.Result.Success) yield break;
			string jsonResponse = request.downloadHandler.text;
			Item item = JsonConvert.DeserializeObject<Item>(jsonResponse);
			ItemBase innerItem = JsonConvert.DeserializeObject<ItemBase>(item.ItemDescription);

			responseAction?.Invoke(innerItem);
		}

		private IEnumerator GetItemsCO(Action<IEnumerable<ItemBase>> responseAction = null)
		{
			UnityWebRequest request =
				RequestManager.RequestQueryBuilder(
					ConfigLoader.Config.Construct(ConfigLoader.Config.Items, EndPoints.GetAll),
					UnityWebRequest.kHttpVerbGET);
			yield return request.SendWebRequest();

			Debug.Log(request.result == UnityWebRequest.Result.Success
				? "all items were retrieved successfully"
				: "Error: " + request.error);
			if (request.result != UnityWebRequest.Result.Success) yield break;
			string jsonResponse = request.downloadHandler.text;
			Item[] item = JsonConvert.DeserializeObject<Item[]>(jsonResponse);
			IEnumerable<ItemBase> innerItem = item.Select(i =>
				JsonConvert.DeserializeObject<ItemBase>(i.ItemDescription, ServerUtilities.JsonSerializer));
			responseAction?.Invoke(innerItem);
		}

		private IEnumerator DeleteItemCO(string itemId, Action<bool> responseAction = null)
		{
			UnityWebRequest request =
				RequestManager.RequestQueryBuilder(
					ConfigLoader.Config.Construct(ConfigLoader.Config.Items, EndPoints.Delete),
					UnityWebRequest.kHttpVerbDELETE,
					itemId);
			yield return request.SendWebRequest();

			Debug.Log(request.result == UnityWebRequest.Result.Success
				? $"{itemId} was deleted successfully"
				: "Error: " + request.error);
			responseAction?.Invoke(request.result == UnityWebRequest.Result.Success);
		}
	}
}

[Serializable]
public class Item
{
	public string ItemId { get; set; } = "";
	public string ItemDescription { get; set; } = "";
}