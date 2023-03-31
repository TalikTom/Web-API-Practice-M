﻿using Practice.Common;
using Practice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Service.Common
{
    public interface IChefService
    {
        Task<List<ChefModelDTO>> FindAsync(Paging paging, Sorting sorting, ChefFilter filteringChef);

        Task<ChefModelDTO> GetByIdAsync(Guid id);

        //Task<bool> DeleteAsync(Guid id);

        //Task<ChefModel> PostAsync(ChefModel chef);

        //Task<bool> PutAsync(Guid id, ChefModel chef);
    }
}
