using System;
using MongoDB.Bson;

namespace CQRS_ByMe.Events
{
    public interface IEvent
    {
        Guid Id { get; set; }
        int Version { get; set; }

    }
}