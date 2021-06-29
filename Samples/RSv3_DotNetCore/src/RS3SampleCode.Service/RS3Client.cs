using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RS3SampleCode.DTOs;
using System;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RS3SampleCode.Service
{
    public class RS3Client : IRS3Client
    {
        private readonly IConfiguration _config;
        private static CredentialModel _credential;

        public RS3Client(IConfiguration config, CredentialModel credential)
        {
            _config = config;
            _credential = credential;
        }

        private static HttpClient GenerateHttpClient()
        {
            string UsernamePassword = string.Format("{0}/{1}:{2}", _credential.CustomerCode, _credential.Username, _credential.Password);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var credentialBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(UsernamePassword));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentialBase64);
            client.DefaultRequestHeaders.Add("TransactionID", _credential.CustomerTransactionID);
            client.DefaultRequestHeaders.Add("BillingLabel", _credential.BillingLabel);
            return client;
        }

        public async Task<EmvConfigTokenFullResponse> GenerateEMVConfigToken(EmvConfigTokenRequest input)
        {                        
            Uri Host = new Uri(_config.GetValue<string>(Constants.RS3_URL_EMVCONFIG_TOKEN));
            HttpClient client = GenerateHttpClient();
            
            EmvConfigTokenFullResponse emvConfigTokenFullResp = new EmvConfigTokenFullResponse();
            emvConfigTokenFullResp.RespHelpers = new ResponseHelpers();
            emvConfigTokenFullResp.RespHelpers.Success = false;
            try
            {
                string sc = JsonConvert.SerializeObject(input);

                using (StringContent strContent = new StringContent(sc, Encoding.UTF8, "application/json"))
                {
                    emvConfigTokenFullResp.RespHelpers.Resp = client.PostAsync(Host, strContent).Result;
                    PrintOutRequestResponse(emvConfigTokenFullResp.RespHelpers.Resp, sc);
                    if (emvConfigTokenFullResp.RespHelpers.Resp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        HttpContent content = emvConfigTokenFullResp.RespHelpers.Resp.Content;
                        emvConfigTokenFullResp.RespHelpers.RespContent = await content.ReadAsStringAsync();
                        //Console.WriteLine(TryFormatJson(pinOffset));
                        if (emvConfigTokenFullResp.RespHelpers.RespContent.Contains("\\", StringComparison.Ordinal))
                            emvConfigTokenFullResp.RespHelpers.RespContent = JsonConvert.DeserializeObject<string>(emvConfigTokenFullResp.RespHelpers.RespContent);
                        var emvConfigTokenResp = JsonConvert.DeserializeObject(emvConfigTokenFullResp.RespHelpers.RespContent, typeof(EmvConfigTokenResponse)) as EmvConfigTokenResponse;
                        emvConfigTokenFullResp.EmvConfigTokenResp = emvConfigTokenResp;                        
                        emvConfigTokenFullResp.RespHelpers.Success = true;
                    }
                    else
                    {
                        emvConfigTokenFullResp.RespHelpers.RespContent = emvConfigTokenFullResp.RespHelpers.Resp.Content.ReadAsStringAsync().Result;
                        emvConfigTokenFullResp.RespHelpers.RSErrors = JsonConvert.DeserializeObject(emvConfigTokenFullResp.RespHelpers.RespContent, typeof(RSError)) as RSError;
                    }
                }
            }
            catch (Exception ex) when (ex is CommunicationException || ex is ProtocolException || ex is FaultException || ex is Exception)
            {
                throw ex;
            }
            finally
            {
                client.Dispose();
            }
            return emvConfigTokenFullResp;
        }

        public async Task<TransformEMVConfigFullResponse> TransformEMVConfig(TransformEMVConfigRequest input)
        {                        
            Uri Host = new Uri(_config.GetValue<string>(Constants.RS3_URL_EMVCONFIG_TRANSFORM));
            HttpClient client = GenerateHttpClient();
            
            TransformEMVConfigFullResponse transformEmvConfigFullResp = new TransformEMVConfigFullResponse();
            transformEmvConfigFullResp.RespHelpers = new ResponseHelpers();
            transformEmvConfigFullResp.RespHelpers.Success = false;
            try
            {
                string sc = JsonConvert.SerializeObject(input);

                using (StringContent strContent = new StringContent(sc, Encoding.UTF8, "application/json"))
                {
                    transformEmvConfigFullResp.RespHelpers.Resp = client.PostAsync(Host, strContent).Result;
                    PrintOutRequestResponse(transformEmvConfigFullResp.RespHelpers.Resp, sc);
                    if (transformEmvConfigFullResp.RespHelpers.Resp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        HttpContent content = transformEmvConfigFullResp.RespHelpers.Resp.Content;
                        transformEmvConfigFullResp.RespHelpers.RespContent = await content.ReadAsStringAsync();                        
                        if (transformEmvConfigFullResp.RespHelpers.RespContent.Contains("\\", StringComparison.Ordinal))
                            transformEmvConfigFullResp.RespHelpers.RespContent = JsonConvert.DeserializeObject<string>(transformEmvConfigFullResp.RespHelpers.RespContent);
                        var transformEmvConfigResp = JsonConvert.DeserializeObject(transformEmvConfigFullResp.RespHelpers.RespContent, typeof(TransformEMVConfigResponse)) as TransformEMVConfigResponse;
                        transformEmvConfigFullResp.TransformEMVConfigResp = transformEmvConfigResp;
                        transformEmvConfigFullResp.RespHelpers.Success = true;
                    }
                    else
                    {
                        transformEmvConfigFullResp.RespHelpers.RespContent = transformEmvConfigFullResp.RespHelpers.Resp.Content.ReadAsStringAsync().Result;
                        transformEmvConfigFullResp.RespHelpers.RSErrors = JsonConvert.DeserializeObject(transformEmvConfigFullResp.RespHelpers.RespContent, typeof(RSError)) as RSError;                        
                    }
                }
            }
            catch (Exception ex) when (ex is CommunicationException || ex is ProtocolException || ex is FaultException || ex is Exception)
            {
                throw ex;
            }
            finally
            {
                client.Dispose();
            }
            return transformEmvConfigFullResp;
        }

        public async Task<GetKeyListFullResponse> GetKeyList(string protocol)
        {            
            string urlName = _config.GetValue<string>(Constants.RS3_URL_KEY_RETRIEVE) + "?protocol=" + protocol;
            Uri Host = new Uri(urlName);
            HttpClient client = GenerateHttpClient();
            GetKeyListFullResponse keyListFullResp = new GetKeyListFullResponse();
            keyListFullResp.RespHelpers = new ResponseHelpers();
            keyListFullResp.RespHelpers.Success = false;
            try
            {
                keyListFullResp.RespHelpers.Resp = client.GetAsync(Host).Result;
                PrintOutResponse(keyListFullResp.RespHelpers.Resp);
                if (keyListFullResp.RespHelpers.Resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = keyListFullResp.RespHelpers.Resp.Content;
                    keyListFullResp.RespHelpers.RespContent = await content.ReadAsStringAsync();
                    if (keyListFullResp.RespHelpers.RespContent.Contains("\\", StringComparison.Ordinal))
                        keyListFullResp.RespHelpers.RespContent = JsonConvert.DeserializeObject<string>(keyListFullResp.RespHelpers.RespContent);
                    var getKeyListResp = JsonConvert.DeserializeObject(keyListFullResp.RespHelpers.RespContent, typeof(GetKeyListResponse)) as GetKeyListResponse;
                    keyListFullResp.KeyList = getKeyListResp;
                    keyListFullResp.RespHelpers.Success = true;
                }
                else
                {
                    keyListFullResp.RespHelpers.RespContent = keyListFullResp.RespHelpers.Resp.Content.ReadAsStringAsync().Result;
                    keyListFullResp.RespHelpers.RSErrors = JsonConvert.DeserializeObject(keyListFullResp.RespHelpers.RespContent, typeof(RSError)) as RSError;
                }                
            }
            catch (Exception ex) when (ex is CommunicationException || ex is ProtocolException || ex is FaultException || ex is Exception)
            {
                throw ex;
            }
            finally
            {
                client.Dispose();
            }
            return keyListFullResp;
        }

        public async Task<KeyUpdateTokenFullResponse> GenerateKeyToken(KeyUpdateTokenRequest input)
        {                        
            Uri Host = new Uri(_config.GetValue<string>(Constants.RS3_URL_KEY_TOKEN));
            HttpClient client = GenerateHttpClient();
            
            KeyUpdateTokenFullResponse genKeyTokenFullResp = new KeyUpdateTokenFullResponse();
            genKeyTokenFullResp.RespHelpers = new ResponseHelpers();
            genKeyTokenFullResp.RespHelpers.Success = false;
            try
            {
                string sc = JsonConvert.SerializeObject(input);

                using (StringContent strContent = new StringContent(sc, Encoding.UTF8, "application/json"))
                {
                    genKeyTokenFullResp.RespHelpers.Resp = client.PostAsync(Host, strContent).Result;
                    PrintOutRequestResponse(genKeyTokenFullResp.RespHelpers.Resp, sc);
                    if (genKeyTokenFullResp.RespHelpers.Resp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        HttpContent content = genKeyTokenFullResp.RespHelpers.Resp.Content;
                        genKeyTokenFullResp.RespHelpers.RespContent = await content.ReadAsStringAsync();
                        if (genKeyTokenFullResp.RespHelpers.RespContent.Contains("\\", StringComparison.Ordinal))
                            genKeyTokenFullResp.RespHelpers.RespContent = JsonConvert.DeserializeObject<string>(genKeyTokenFullResp.RespHelpers.RespContent);
                        var genKeyTokenResp = JsonConvert.DeserializeObject(genKeyTokenFullResp.RespHelpers.RespContent, typeof(KeyUpdateTokenResponse)) as KeyUpdateTokenResponse;
                        genKeyTokenFullResp.KeyTokenResp = genKeyTokenResp;
                        genKeyTokenFullResp.RespHelpers.Success = true;
                    }
                    else
                    {
                        genKeyTokenFullResp.RespHelpers.RespContent = genKeyTokenFullResp.RespHelpers.Resp.Content.ReadAsStringAsync().Result;
                        genKeyTokenFullResp.RespHelpers.RSErrors = JsonConvert.DeserializeObject(genKeyTokenFullResp.RespHelpers.RespContent, typeof(RSError)) as RSError;
                    }
                }
            }
            catch (Exception ex) when (ex is CommunicationException || ex is ProtocolException || ex is FaultException || ex is Exception)
            {
                throw ex;
            }
            finally
            {
                client.Dispose();
            }
            return genKeyTokenFullResp;
        }

        public string TryFormatJson(string json)
        {
            try
            {
                dynamic parsedJson = JsonConvert.DeserializeObject(json);
                return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
            }
            catch (Exception)
            {
                return json;
            }
        }

        private void PrintOutResponse(HttpResponseMessage response)
        {
            Console.WriteLine("--- RESPONSE ---");
            Console.WriteLine("HTTP/{0} {1} {2}", response?.Version, (int)response?.StatusCode, response?.ReasonPhrase);
            foreach (var header in response?.Headers)
                Console.WriteLine("{0}: {1}", header.Key, string.Join(',', header.Value));
            Console.WriteLine();
        }

        private void PrintOutRequestResponse(HttpResponseMessage response, string sc)
        {
            Console.WriteLine("--- REQUEST ---");
            Console.WriteLine("{0} {1} HTTP/{2}", response?.RequestMessage?.Method, response?.RequestMessage?.RequestUri, response?.RequestMessage?.Version);
            foreach (var header in response?.RequestMessage?.Headers)
                Console.WriteLine("{0}: {1}", header.Key, string.Join(',', header.Value));
            Console.WriteLine();
            Console.WriteLine(TryFormatJson(sc));
            PrintOutResponse(response);            
        }
    }
}

