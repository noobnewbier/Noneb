﻿using Common;
using Common.BoardItems;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProvider
{
    //todo : link this with the actual BoardItemsHolderRepository
    public class BoardItemsHolderProviderRepository<THolderProvider, THolder> : DataRepository<THolderProvider>
        where THolderProvider : BoardItemsHolderProvider<THolder>
    {
    }
}