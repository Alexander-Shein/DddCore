﻿using DddCore.Contracts.BLL.Domain.Models.Audit.At;

namespace DddCore.Contracts.BLL.Domain.Events
{
    public interface IDomainEvent : ICreatedAt
    {
    }
}