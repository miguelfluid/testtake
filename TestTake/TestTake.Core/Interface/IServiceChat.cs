using System.Collections.Generic;
using TestTake.Views.BusinessCrud;
using TestTake.Views.BusinessList;

namespace TestTake.Core.Interface
{
  public interface IServiceChat
  {
    string NewUser(ViewCrudUser view);
    ViewCrudUser GetUser(int id);
    List<ViewListUser> ListUser(int pageSize, int page, string filter);
    string NewRoom(ViewCrudRoom view);
    List<ViewListRoom> ListRoom(int pageSize, int page, string filter);
    List<ViewCrudChat> List(int idroom, int pageSize, int page, string filter);
    string New(ViewCrudChat view);
  }
}
