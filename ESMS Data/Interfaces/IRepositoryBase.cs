﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMS_Data.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        public Task Add(T entity);
        public Task Delete(T entity);
        public Task Update(T entity);
        public IQueryable<T> GetAll();
    }
}