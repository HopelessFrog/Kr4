using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kr4.Model.Entities;
using Kr4.View.Interface;

namespace Kr4.Services.Interface
{
    public interface IEditWindowsFactory
    {
        IEditWindow CreateEditWindow(IAstronomicalObject item);
    }
}
