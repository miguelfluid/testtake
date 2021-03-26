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

        // name invalid 
        Exception ex = Assert.Throws<Exception>(() => service.NewUser(viewUser));
        Assert.Equal("USER02", ex.Message);

        viewUser.Name = "test1";
        // mail invalid
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

      }
      catch (Exception e)
      {
        throw e;
      }
    }
  }
}

