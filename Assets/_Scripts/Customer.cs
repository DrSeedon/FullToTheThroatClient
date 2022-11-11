using System.Collections;
using System.Collections.Generic;
using Riptide;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public static Dictionary<ushort, Customer> list = new Dictionary<ushort, Customer>();

    public ushort Id { get; private set; }
    public bool IsLocal { get; private set; }
    
    public List<Order> orders = new List<Order>();
    public string name;
    public string cardNUmber;

    private void OnDestroy()
    {
        list.Remove(Id);
    }

    public static void Spawn(ushort id, string username)
    {
        Customer customer;
        customer = Instantiate(ShopLogic.Instance.clientPrefab, Vector3.zero, Quaternion.identity)
            .GetComponent<Customer>();
        
        customer.IsLocal = id == NetworkManager.Instance.Client.Id;
        
        customer.name = $"Player {id} ({(string.IsNullOrEmpty(username) ? "Guest" : username)})";
        customer.Id = id;
        customer.name = string.IsNullOrEmpty(username) ? $"Guest {id}" : username;

        list.Add(id, customer);
    }

    #region Messages
/// <summary>
/// Принять сообщение о спавне префаба клиента
/// </summary>
/// <param name="message"></param>
    [MessageHandler((ushort) ServerToClientId.playerSpawned)]
    private static void SpawnClient(Message message)
    {
        Spawn(message.GetUShort(), message.GetString());
    }

    #endregion
}
