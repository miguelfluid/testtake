using TestTake.Core.Base;
using TestTake.Data;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTake.Services.Auth;
using TestTake.Views.BusinessCrud;
using Microsoft.AspNetCore.Builder;

namespace TestTake.Services.Commons
{
  public class ServiceQueueChat
  {
    private readonly IQueueClient queueClient;
    private readonly ServiceChat _serviceChat;

    #region Contructor
    public ServiceQueueChat(DataContext context, ServiceChat serviceChat)
    {
      _serviceChat = serviceChat;
      queueClient = new QueueClient("Endpoint=sb://testtake.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=+gRsPWsjpFVCxtMHHWUJtAtaOLvZsEUTE8I/hiwy83c=", "chat");

    }
    #endregion

    #region Processamento da Fila
    public async void SendMessageAsync(ViewCrudChat view)
    {
      try
      {
        await queueClient.SendAsync(new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(view))));
      }
      catch
      {
      }
    }
    public void RegisterOnMessageHandlerAndReceiveMesssages()
    {
      try
      {
        MessageHandlerOptions messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
        {
          MaxConcurrentCalls = 1,
          AutoComplete = false
        };
        queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

      }
      catch (Exception e)
      {
        throw e;
      }
    }
    private async Task ProcessMessagesAsync(Message message, CancellationToken token)
    {
      ViewCrudChat view = JsonConvert.DeserializeObject<ViewCrudChat>(Encoding.UTF8.GetString(message.Body));
      if (view == null)
      {
        return;
      }

      var webSocketOptions = new WebSocketOptions()
      {
        KeepAliveInterval = TimeSpan.FromSeconds(120),
      };

      //app.UseWebSockets(webSocketOptions);

      await queueClient.CompleteAsync(message.SystemProperties.LockToken);
    }

    private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
    {
      _ = exceptionReceivedEventArgs.ExceptionReceivedContext;
      return Task.CompletedTask;
    }
    #endregion

  }
}
