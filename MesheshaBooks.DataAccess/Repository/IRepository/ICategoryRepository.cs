﻿using MesheshaBooks.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MesheshaBooks.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
