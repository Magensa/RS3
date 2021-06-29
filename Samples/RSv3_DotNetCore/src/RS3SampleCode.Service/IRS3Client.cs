using RS3SampleCode.DTOs;
using System;
using System.Threading.Tasks;

namespace RS3SampleCode.Service
{
    public interface IRS3Client
    {
        Task<EmvConfigTokenFullResponse> GenerateEMVConfigToken(EmvConfigTokenRequest input);
        Task<TransformEMVConfigFullResponse> TransformEMVConfig(TransformEMVConfigRequest input);
        Task<GetKeyListFullResponse> GetKeyList(string protocol);
        Task<KeyUpdateTokenFullResponse> GenerateKeyToken(KeyUpdateTokenRequest input);
        string TryFormatJson(string json);
    }
}
