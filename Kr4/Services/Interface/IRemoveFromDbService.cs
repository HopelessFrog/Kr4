using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kr4.Model.Entities;

namespace Kr4.Services.Interface
{
    public interface IRemoveFromDbService
    {
        void Remove(IAstronomicalObject obj);
    }
}
