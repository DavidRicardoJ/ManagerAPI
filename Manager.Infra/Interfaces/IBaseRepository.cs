﻿using Manager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Infra.Interfaces
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<T> Create(T obj);
        Task<T> Update(T obj);
        Task Remove(int id);
        Task<T> Get(int id);
        Task<List<T>> GetList();

    }
}
