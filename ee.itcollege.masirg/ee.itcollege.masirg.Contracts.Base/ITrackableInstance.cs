using System;

namespace ee.itcollege.masirg.Contracts.Base
{
    public interface ITrackableInstance
    {
        Guid InstanceId { get; }
    }
}