using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AymanStore.DAL.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace AymanStore.BLL.Interfaces
{
    public interface IMySPECIALGUID
    {
        string GetUniqueKey();
        string GetUniqueKey(int length);
        string GetUniqueKey(int first, int second);
        string GetUniqueKey(string first, string usercreation, string last);
    }
}
