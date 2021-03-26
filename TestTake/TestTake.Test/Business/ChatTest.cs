using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using TestTake.Services.Auth;
using TestTake.Test.Commons;
using TestTake.Views.BusinessCrud;
using Xunit;
using TestTake.Views.BusinessList;
using TestTake.Data;
using TestTake.Core.Business;

namespace TestTake.Test.Services.Auth
{
  public class UserTest : TestCommons
  {
    private readonly ServiceChat service;
    public UserTest()
    {
      service = new ServiceChat(context);
    }

    [Fact]
    public void CrudUser()
    {
      try
      {
        ViewCrudUser viewUser = new ViewCrudUser();

        
        Exception ex = Assert.Throws<Exception>(() => service.NewUser(viewUser));
        Assert.Equal("USER02", ex.Message);

        viewUser.Name = "test1";
        
        ex = Assert.Throws<Exception>(() => service.NewUser(viewUser));
        Assert.Equal("USER03", ex.Message);

        viewUser.Name = "test2";
        Assert.Equal("USER05", service.NewUser(viewUser));

        viewUser.Id = service.ListUser(999, 1, "").LastOrDefault().Id;
        
        // get id
        viewUser = service.GetUser(viewUser.Id);
        Assert.True(viewUser != null);

        //list
        List<ViewListUser> listUser = service.ListUser(10, 1, "");
        Assert.True(listUser.Count > 0);

        //delete
        Repository<User> repositoryUser = new Repository<User>(context);
        repositoryUser.Delete(viewUser.Id);

        viewUser.Name = "test1";

        ViewCrudRoom viewRoom = new ViewCrudRoom();
        ex = Assert.Throws<Exception>(() => service.NewRoom(viewRoom));
        Assert.Equal("ROOM02", ex.Message);

        viewRoom.Name = "room1";
        Assert.Equal("ROOM05", service.NewRoom(viewRoom));

        //list
        List<ViewListRoom> listRoom = service.ListRoom(10, 1, "");
        Assert.True(listRoom.Count > 0);
        viewRoom.Id = listRoom.FirstOrDefault().Id;

        ViewCrudChat view = new ViewCrudChat();
        ex = Assert.Throws<Exception>(() => service.New(view));
        Assert.Equal("CHAT02", ex.Message);

        
        view.IdUserSend = viewUser.Id;
        view.NameUserSend = viewUser.Name;
        view.Message = view.NameUserSend + ": " + "test";
        view.IdRoom = viewRoom.Id;
        view.Name = viewRoom.Name;
        view.Private = false;
        Assert.Equal("CHAT01", service.New(view));

        List<ViewCrudChat> list = service.List(view.IdRoom, 10, 1, "");
        Assert.True(list.Count > 0);
      }
      catch (Exception e)
      {
        throw e;
      }
    }
  }
}

