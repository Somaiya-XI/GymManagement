using System.Text.Json.Serialization;

namespace Contracts.Subscriptions;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SubscriptionType
{
    Free,
    Starter,
    Pro
    
}