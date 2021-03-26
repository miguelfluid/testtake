using System;
using System.Collections.Generic;
using System.Text;
using TestTake.Core.Business;
using TestTake.Data;
using TestTake.Views.BusinessCrud;
using System.Linq;
using TestTake.Views.BusinessList;
using TestTake.Core.Interface;

namespace TestTake.Services.Auth
{
  public class ServiceChat : IServiceChat
  {
    private readonly Repository<User> serviceUser;
    private readonly Repository<Room> serviceRoom;
    private readonly Repository<Chat> serviceChat;

    #region constructor
    public ServiceChat(DataContext context)
    {
      serviceUser = new Repository<User>(context);
      serviceRoom = new Repository<Room>(context);
      serviceChat = new Repository<Chat>(context);
    }
    #endregion

    #region crud
    

    public ViewCrudUser GetUser(int id)
    {
      try
      {
        return serviceUser.GetByID(id).GetViewCrud();
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public List<ViewListUser> ListUser(int pageSize, int page, string filter)
    {
      try
      {
        return serviceUser.Get(p => p.Name.ToUpper().Contains(filter.ToUpper())).Select(p => p.GetViewList())
          .OrderBy(o => o.Name).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public string NewUser(ViewCrudUser view)
    {
      try
      {
        if (string.IsNullOrEmpty(view.Name))
        {
          throw new Exception("USER02");
        }
        if (serviceUser.Get(p => p.Name == view.Name).FirstOrDefault() != null)
        {
          throw new Exception("USER03");
        }

        serviceUser.Insert(new User()
        {
          Name = view.Name,
          Mail = view.Mail,
          Password = view.Password
        });
        return "USER05";
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public string NewRoom(ViewCrudRoom view)
    {
      try
      {
        if (string.IsNullOrEmpty(view.Name))
        {
          throw new Exception("ROOM02");
        }
        
        serviceRoom.Insert(new Room()
        {
          Name = view.Name
        });
        return "ROOM05";
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public string New(ViewCrudChat view)
    {
      try
      {
        if (string.IsNullOrEmpty(view.Message))
        {
          throw new Exception("CHAT02");
        }
        serviceChat.Insert(new Chat()
        {
          NameRoom = view.Name,
          IdRoom = view.IdRoom,
          IdUserSend = view.IdUserSend,
          IdUserReceived = view.IdUserReceived,
          Date = DateTime.Now,
          NameUserReceived = view.NameUserReceived,
          NameUserSend = view.NameUserSend,
          Message = view.Message,
          Private = view.Private
        });
        return "CHAT01";
      }catch(Exception e)
      {
        throw e;
      }
    }

    public List<ViewListRoom> ListRoom(int pageSize, int page, string filter)
    {
      try
      {
        return serviceRoom.Get(p => p.Name.ToUpper().Contains(filter.ToUpper())).Select(p => p.GetViewList())
          .OrderBy(o => o.Name).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public List<ViewCrudChat> List(int idroom, int pageSize, int page, string filter)
    {
      try
      {
        return serviceChat.Get(p => p.IdRoom == idroom && p.NameRoom.ToUpper().Contains(filter.ToUpper())).OrderBy(o => o.Date).Select(p => p.GetViewCrud())
          .Skip(pageSize * (page - 1)).Take(pageSize).ToList();
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    #endregion
  }
}
