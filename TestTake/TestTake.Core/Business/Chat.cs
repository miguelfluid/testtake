using System;
using TestTake.Core.Base;
using TestTake.Views.BusinessCrud;
using TestTake.Views.BusinessList;

namespace TestTake.Core.Business
{
  public class Chat: BaseEntity
  {
    public int IdRoom { get; set; }
    public string NameRoom { get; set; }
    public int IdUserSend { get; set; }
    public int? IdUserReceived { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
    public bool Private { get; set; }

    public ViewListChat GetViewList()
    {
      return new ViewListChat()
      {
        Id = Id,
        Name = NameRoom
      };
    }

    public ViewCrudChat GetViewCrud()
    {
      return new ViewCrudChat()
      {
        Id = Id,
        IdRoom = IdRoom,
        Name = NameRoom,
        IdUserReceived = IdUserReceived,
        IdUserSend = IdUserSend,
        Message = Message,
        Date = Date,
        Private = Private
      };
    }
  }
}
