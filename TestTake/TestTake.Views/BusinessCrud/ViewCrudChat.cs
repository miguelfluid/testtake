using System;

namespace TestTake.Views.BusinessCrud
{
  public class ViewCrudChat: ViewBase
  {
    public int IdRoom { get; set; }
    public int IdUserSend { get; set; }
    public int? IdUserReceived { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
    public bool Private { get; set; }
  }
}
