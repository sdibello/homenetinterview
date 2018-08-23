using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using apiClient.dto;

namespace apiClient
{
    public interface IApi
    {
        Task<Tuple<string, int>> GetDataset();

        Task<Tuple<int[], int>> Getvehicles(string datasetId);

        Task<Tuple<rdo.VehicleDetailsRepsonse, int>> GetvehiclesDetails(string datasetId, int vechileId);

        Task<Tuple<rdo.DealerDetailResponse, int>> GetDealerDetail(string datasetId, int dealerId);

        int PostAnswer(string datasetId, Answer answer);

        void setBaseAddress(string baseAddress);

        Task<int?> PostAnswer(dto.Answer answer);
    }
}
