using RS3SampleCode.DTOs;
using RS3SampleCode.Service;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace RS3SampleCode.UIHandler
{
    public class UIFactory : IUIFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public UIFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowUI(UI ui)
        {
            switch (ui)
            {
                case UI.EMVCONFIG_TOKEN:
                    ShowGenerateEmvConfigToken();
                    break;
                case UI.EMVCONFIG_TRANSFORM:
                    ShowTransformEmvConfig();
                    break;
                case UI.KEY_RETRIEVE:
                    ShowKeyList();
                    break;
                case UI.KEY_TOKEN:
                    ShowGenerateKeyUpdateToken();
                    break;
            }
        }

        private void ShowGenerateEmvConfigToken()
        {
            EmvConfigTokenRequest input = new EmvConfigTokenRequest();

            try
            {
                input.Protocol = Read_String_Input("Enter Protocol:", false);
                input.KSI = Read_String_Input("Enter KSI:", false);
                input.KeyDerivationData = Read_String_Input("Enter KeyDerivationData:", false);
                input.KeyDerivationAlgorithm = Read_String_Input("Enter KeyDerivationAlgorithm:", false);
                input.DeviceSN = Read_String_Input("Enter DeviceSN:", false);
                input.EMVConfigData = Read_LongString_Input("Enter EMVConfigData:", false);
                Console.WriteLine("Please wait...");
                var svc = _serviceProvider.GetService<IRS3Client>();                
                var result = svc.GenerateEMVConfigToken(input).Result;
                if (result.EmvConfigTokenResp is null)
                {
                    if (!string.IsNullOrWhiteSpace(result.RespHelpers.RSErrors.MagTranID))
                    {
                        Console.WriteLine("=====================Response with ERROR Start======================");
                        Console.WriteLine("CustomerTransactionID: " + result.RespHelpers.RSErrors.CustomerTransactionID);
                        Console.WriteLine("MagTranID: " + result.RespHelpers.RSErrors.MagTranID);
                        Console.WriteLine("Error Code: " + result.RespHelpers.RSErrors.Code);
                        Console.WriteLine("Error Code: " + result.RespHelpers.RSErrors.Message);
                        Console.WriteLine("=====================Response with ERROR End======================");
                    }                    
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(result.EmvConfigTokenResp.MagTranID))
                    {
                        Console.WriteLine("=====================Response Start======================");
                        Console.WriteLine("CustomerTransactionID: " + result.EmvConfigTokenResp.CustomerTransactionID);
                        Console.WriteLine("MagTranID: " + result.EmvConfigTokenResp.MagTranID);
                        Console.WriteLine("Token: " + result.EmvConfigTokenResp.Token);
                        Console.WriteLine("=====================Response End======================");
                    }
                        
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error while Generating EMVConfig token : " + ex.Message.ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }

        }

        private void ShowTransformEmvConfig()
        {
            TransformEMVConfigRequest input = new TransformEMVConfigRequest();
            
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            
            try
            {
                input.Protocol = Read_String_Input("Enter Protocol:", false);
                // FileBase64 is base64 encoded excel files
                input.FileBase64 = Read_LongString_Input("Enter FileBase64:", false);
                input.IsLegacyExcel = Convert.ToBoolean(Read_String_Input("Enter IsLegacyExcel:", false));
                            
                Console.WriteLine("Please wait...");
                var svc = _serviceProvider.GetService<IRS3Client>();
                var result = svc.TransformEMVConfig(input).Result;
                if (result.TransformEMVConfigResp is null)
                {
                    if (!string.IsNullOrWhiteSpace(result.RespHelpers.RSErrors.MagTranID))
                    {
                        Console.WriteLine("=====================Response with ERROR Start======================");
                        Console.WriteLine("CustomerTransactionID: " + result.RespHelpers.RSErrors.CustomerTransactionID);
                        Console.WriteLine("MagTranID: " + result.RespHelpers.RSErrors.MagTranID);
                        Console.WriteLine("Error Code: " + result.RespHelpers.RSErrors.Code);
                        Console.WriteLine("Error Code: " + result.RespHelpers.RSErrors.Message);
                        Console.WriteLine("=====================Response with ERROR End======================");
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(result.TransformEMVConfigResp.MagTranID))
                    {
                        Console.WriteLine("=====================Response Start======================");
                        Console.WriteLine("CustomerTransactionID: " + result.TransformEMVConfigResp.CustomerTransactionID);
                        Console.WriteLine("MagTranID: " + result.TransformEMVConfigResp.MagTranID);
                        Console.WriteLine("ConfigName: " + result.TransformEMVConfigResp.ConfigName);
                        Console.WriteLine("Version: " + result.TransformEMVConfigResp.Version);
                        foreach (var item in result.TransformEMVConfigResp.Bins)
                        {
                            Console.WriteLine("Config: " + item.Config);
                            Console.WriteLine("ConfigId: " + item.ConfigId);
                            Console.WriteLine("HashId: " + item.HashId);
                            Console.WriteLine("TimeStamp: " + item.TimeStamp);
                        }

                        Console.WriteLine("=====================Response End======================");
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error while transforming EMVConfig : " + ex.Message.ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void ShowKeyList()
        {
            try
            {
                string protocol = Read_String_Input("Enter Protocol:", false);

                Console.WriteLine("Please wait...");
                var svc = _serviceProvider.GetService<IRS3Client>();
                var result = svc.GetKeyList(protocol).Result;
                if (result.KeyList is null)
                {
                    if (!string.IsNullOrWhiteSpace(result.RespHelpers.RSErrors.MagTranID))
                    {
                        Console.WriteLine("=====================Response with ERROR Start======================");
                        Console.WriteLine("CustomerTransactionID: " + result.RespHelpers.RSErrors.CustomerTransactionID);
                        Console.WriteLine("MagTranID: " + result.RespHelpers.RSErrors.MagTranID);
                        Console.WriteLine("Error Code: " + result.RespHelpers.RSErrors.Code);
                        Console.WriteLine("Error Code: " + result.RespHelpers.RSErrors.Message);
                        Console.WriteLine("=====================Response with ERROR End======================");
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(result.KeyList.MagTranID))
                    {
                        Console.WriteLine("=====================Response Start======================");
                        Console.WriteLine("CustomerTransactionID: " + result.KeyList.CustomerTransactionID);
                        Console.WriteLine("MagTranID: " + result.KeyList.MagTranID);
                        foreach (var item in result.KeyList.Keys)
                        {
                            Console.WriteLine("\t--------------------------------------");
                            Console.WriteLine("\tID: " + item.ID);
                            Console.WriteLine("\tKeyName: " + item.KeyName);
                            Console.WriteLine("\tDescription: " + item.Description);
                            Console.WriteLine("\tKeySlotNamePrefix: " + item.KeySlotNamePrefix);
                            Console.WriteLine("\tKSI: " + item.KSI);
                            Console.WriteLine("\tProtocol: " + item.Protocol);
                            Console.WriteLine("\tHSM: " + item.HSM);
                            Console.WriteLine("\tDerivedKeyType: " + item.DerivedKeyType);
                            Console.WriteLine("\tKeyTypeRestrictionBitmask: " + item.KeyTypeRestrictionBitmask);
                            Console.WriteLine("\tDUKPTDataTypeRestrictionBitmask: " + item.DUKPTDataTypeRestrictionBitmask);
                            Console.WriteLine("\tDateCreated: " + item.DateCreated);
                            Console.WriteLine("\tDateModified: " + item.DateModified);
                        }

                        Console.WriteLine("=====================Response End======================");
                    }
                }                
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error while retrieving list of key : " + ex.Message.ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void ShowGenerateKeyUpdateToken()
        {
            KeyUpdateTokenRequest input = new KeyUpdateTokenRequest();

            try
            {
                input.Protocol = Read_String_Input("Enter Protocol:", false);
                input.ProductName = Read_String_Input("Enter ProductName:", false);
                input.KeyDerivationData = Read_String_Input("Enter KeyDerivationData:", false);
                input.KeyRestriction = Read_String_Input("Enter KeyRestriction:", false);
                input.TransportKeyID = Read_String_Input("Enter TransportKeyID:", false);
                input.DeviceChallenge = Read_String_Input("Enter DeviceChallenge:", false);
                input.DeviceSN = Read_String_Input("Enter DeviceSN:", false);
                input.KeySlotID = Read_String_Input("Enter KeySlotID:", false);
                input.CurrentKSN = Read_String_Input("Enter CurrentKSN:", false);
                input.TargetKSI = Read_String_Input("Enter TargetKSI:", false);               

                Console.WriteLine("Please wait...");
                var svc = _serviceProvider.GetService<IRS3Client>();
                var result = svc.GenerateKeyToken(input).Result;
                if (result.KeyTokenResp is null)
                {
                    if (!string.IsNullOrWhiteSpace(result.RespHelpers.RSErrors.MagTranID))
                    {
                        Console.WriteLine("=====================Response with ERROR Start======================");
                        Console.WriteLine("CustomerTransactionID: " + result.RespHelpers.RSErrors.CustomerTransactionID);
                        Console.WriteLine("MagTranID: " + result.RespHelpers.RSErrors.MagTranID);
                        Console.WriteLine("Error Code: " + result.RespHelpers.RSErrors.Code);
                        Console.WriteLine("Error Code: " + result.RespHelpers.RSErrors.Message);
                        Console.WriteLine("=====================Response with ERROR End======================");
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(result.KeyTokenResp.MagTranID))
                    {
                        Console.WriteLine("=====================Response Start======================");
                        Console.WriteLine("CustomerTransactionID: " + result.KeyTokenResp.CustomerTransactionID);
                        Console.WriteLine("MagTranID: " + result.KeyTokenResp.MagTranID);
                        Console.WriteLine("UpdateToken: " + result.KeyTokenResp.UpdateToken);
                        Console.WriteLine("IsRawCommand: " + result.KeyTokenResp.IsRawCommand);
                        Console.WriteLine("=====================Response End======================");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error while Generating a token for initial DUKPT TR31 Key Block: " + ex.Message.ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        #region Helper Functions

        /// <summary>
        /// accepts default string input. For large string input, use the Read_LongString_Input
        /// </summary>
        /// <param name="question"></param>
        /// <param name="isOptional"></param>
        /// <returns></returns>
        private static string Read_String_Input(string question, bool isOptional)
        {
            Console.WriteLine(question);
            var ans = Console.ReadLine();
            if ((!isOptional) && string.IsNullOrWhiteSpace(ans))
            {
                return Read_String_Input(question, isOptional);
            }
            return ans;
        }
        /// <summary>
        /// Accepts large string input, as the default string implemenattion has limitations.
        /// </summary>
        /// <param name="userMessage"></param>
        /// <param name="isOptional"></param>
        /// <returns></returns>
        private static string Read_LongString_Input(string userMessage, bool isOptional)
        {
            Console.WriteLine(userMessage);
            byte[] inputBuffer = new byte[262144];
            Stream inputStream = Console.OpenStandardInput(262144);
            Console.SetIn(new StreamReader(inputStream, Console.InputEncoding, false, inputBuffer.Length));
            string strInput = Console.ReadLine();
            if ((!isOptional) && string.IsNullOrWhiteSpace(strInput))
            {
                return Read_LongString_Input(userMessage, isOptional);
            }
            return strInput;
        }

        #endregion
    }

}
