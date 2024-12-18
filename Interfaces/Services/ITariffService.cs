using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace Interfaces.Services
{
    public interface ITariffService
    {
        bool CheckTariff(int bkId, int usId);
        void SetTariff(int usId, int trId);
        void CancelTariff(int usId, int trId);
        List<TariffPreview> GetTariffPreviews(int usId);
    }
}
