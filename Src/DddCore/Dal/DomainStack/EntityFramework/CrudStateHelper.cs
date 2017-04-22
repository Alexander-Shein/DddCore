using System;
using DddCore.Contracts.Domain.Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace DddCore.Dal.DomainStack.EntityFramework
{
    public class CrudStateHelper
    {
        public static EntityState ConvertState(CrudState state)
        {
            switch (state)
            {
                case CrudState.Added:
                    return EntityState.Added;

                case CrudState.Modified:
                    return EntityState.Modified;

                case CrudState.Deleted:
                    return EntityState.Deleted;

                default:
                    return EntityState.Unchanged;
            }
        }

        public static CrudState ConvertState(EntityState state)
        {
            switch (state)
            {
                case EntityState.Detached:
                    return CrudState.Unchanged;

                case EntityState.Unchanged:
                    return CrudState.Unchanged;

                case EntityState.Added:
                    return CrudState.Added;

                case EntityState.Deleted:
                    return CrudState.Deleted;

                case EntityState.Modified:
                    return CrudState.Modified;

                default:
                    throw new ArgumentOutOfRangeException(nameof(state));
            }
        }
    }
}
