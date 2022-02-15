using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace Tasevski.MessageBus
{
    public class AzureServiceBusMessageBus : IMessageBus
    {
        private string connectionString = "Endpoint=sb://aplikacijamango.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=4riYCdGpy6ACmfP4motqrlR+Y1u089OX7oCrzmnO0MM=";
        public async Task PublishMessage(BaseMessage message, string topicName)
        {
            //OVA E ZA AZURE.MESSAGING.SERVICEBUS(7.3)

           await using var client = new ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(topicName);

            var jsonMessage = JsonConvert.SerializeObject(message);
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await sender.SendMessageAsync(finalMessage);

            await client.DisposeAsync();
            
            
            //OVA E ZA MICROSOFT.AZURE.SERVICEBUS(5.1)

            //ISenderClient senderClient = new TopicClient(connectionString, topicName);

            //var jsonMessage = JsonConvert.SerializeObject(message);
            //var finalMessage = new Message(Encoding.UTF8.GetBytes(jsonMessage))
            //{
            //    CorrelationId = Guid.NewGuid().ToString()
            //};

            //await senderClient.SendAsync(finalMessage);

            //await senderClient.CloseAsync();
        }
    }
}
