using System;
using System.Collections.Generic;
using System.Text;
using TARge25Shop.Core.Domain;
using TARge25Shop.Core.Dto;

namespace TARge25Shop.Core.ServiceInterface
{
    public interface IKinderGartenServices
    {
        Task<KinderGarten> Create(KinderGartenDto dto);
        Task<KinderGarten> Update(KinderGartenDto dto);
        Task<KinderGarten> DetailAsync(Guid id);
        Task<KinderGarten> Delete(Guid id);
    }
}