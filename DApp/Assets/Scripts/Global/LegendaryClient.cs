using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loom.Client;
using Loom.Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Text;
using System.IO;
using System.Numerics;

namespace LegendFramework
{
    public class LegendaryClient
    {
        DAppChainClient client;
        IRpcClient writer;
        IRpcClient reader;
        EvmContract evmContract;

        byte[] PrivateKey;
        byte[] PublicKey;
        
        public async void SignIn(string ipAddress)
        {
            if (1 == PlayerPrefs.GetInt("PrivateKeyExist"))
            {
                PrivateKey = File.ReadAllBytes("Private.db");
            }
            else
            {
                PrivateKey = CryptoUtils.GeneratePrivateKey();
                PlayerPrefs.SetInt("PrivateKeyExist", 1);
                File.WriteAllBytes("Private.db", PrivateKey);
            }
            if (1 == PlayerPrefs.GetInt("PublicKeyExist"))
            {
                PublicKey = File.ReadAllBytes("Public.db");
            }
            else
            {
                PublicKey = CryptoUtils.PublicKeyFromPrivateKey(PrivateKey);
                PlayerPrefs.SetInt("PublicKeyExist", 1);
                File.WriteAllBytes("Public.db", PublicKey);
            }
            Address callerAddr = Address.FromPublicKey(PublicKey);

            writer = RpcClientFactory.Configure()
                .WithLogger(Debug.unityLogger)
                .WithWebSocket("ws://" + ipAddress + ":46657/websocket")
                .Create();

            reader = RpcClientFactory.Configure()
                .WithLogger(Debug.unityLogger)
                .WithWebSocket("ws://" + ipAddress + ":9999/queryws")
                .Create();

            client = new DAppChainClient(writer, reader)
            {
                Logger = Debug.unityLogger
            };

            client.TxMiddleware = new TxMiddleware(new ITxMiddlewareHandler[]
            {
            new NonceTxMiddleware(PublicKey, client),
            new SignedTxMiddleware(PrivateKey)
            });

            //string abi = "[{\"constant\":false,\"inputs\":[{\"name\":\"title\",\"type\":\"string\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"monsterName\",\"type\":\"string\"},{\"name\":\"monsterPower\",\"type\":\"uint256\"}],\"name\":\"createQuest\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"users\",\"outputs\":[{\"name\":\"username\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"username\",\"type\":\"string\"}],\"name\":\"createUser\",\"outputs\":[{\"name\":\"uid\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"_uid\",\"type\":\"uint256\"}],\"name\":\"openRandomQuest\",\"outputs\":[{\"name\":\"uid\",\"type\":\"uint256\"},{\"name\":\"qid\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"owner\",\"type\":\"address\"}],\"name\":\"getUserInfo\",\"outputs\":[{\"name\":\"username\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"getQuestCount\",\"outputs\":[{\"name\":\"count\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"name\":\"\",\"type\":\"address\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"getUserCount\",\"outputs\":[{\"name\":\"count\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"ownerUid\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"quests\",\"outputs\":[{\"name\":\"key\",\"type\":\"uint256\"},{\"name\":\"title\",\"type\":\"string\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"endTime\",\"type\":\"uint256\"},{\"name\":\"monsterName\",\"type\":\"string\"},{\"name\":\"monsterPower\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"questToOwner\",\"outputs\":[{\"name\":\"\",\"type\":\"address\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"transferOwnership\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"name\":\"uid\",\"type\":\"uint256\"},{\"indexed\":false,\"name\":\"title\",\"type\":\"string\"}],\"name\":\"NewQuest\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"name\":\"previousOwner\",\"type\":\"address\"},{\"indexed\":true,\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"OwnershipTransferred\",\"type\":\"event\"}]";
            string abi = "[{\"constant\":false,\"inputs\":[{\"name\":\"title\",\"type\":\"string\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"monsterName\",\"type\":\"string\"},{\"name\":\"monsterPower\",\"type\":\"uint256\"}],\"name\":\"createQuest\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"users\",\"outputs\":[{\"name\":\"username\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"username\",\"type\":\"string\"}],\"name\":\"createUser\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"_uid\",\"type\":\"uint256\"}],\"name\":\"openRandomQuest\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"},{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"ping\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"pure\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"owner\",\"type\":\"address\"}],\"name\":\"getUserInfo\",\"outputs\":[{\"name\":\"\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"getQuestCount\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"name\":\"\",\"type\":\"address\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"getUserCount\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"ownerUid\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"whoAmI\",\"outputs\":[{\"name\":\"\",\"type\":\"address\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"quests\",\"outputs\":[{\"name\":\"key\",\"type\":\"uint256\"},{\"name\":\"title\",\"type\":\"string\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"endTime\",\"type\":\"uint256\"},{\"name\":\"monsterName\",\"type\":\"string\"},{\"name\":\"monsterPower\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"questToOwner\",\"outputs\":[{\"name\":\"\",\"type\":\"address\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"transferOwnership\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"name\":\"uid\",\"type\":\"uint256\"},{\"indexed\":false,\"name\":\"title\",\"type\":\"string\"}],\"name\":\"NewQuest\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"name\":\"previousOwner\",\"type\":\"address\"},{\"indexed\":true,\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"OwnershipTransferred\",\"type\":\"event\"}]";
            Address contractAddr = await client.ResolveContractAddressAsync("Legendary");
            //var contractAddr = Address.FromHexString("0x6fbd04200Edc1579BDe307A76E1aAc6ef96ACE5A");

            evmContract = new EvmContract(client, contractAddr, callerAddr, abi);
            //evmContract.EventReceived += (sender, e) =>
            //{
            //    if (e.EventName == "OwnershipTransferred")
            //        Debug.Log("OwnershipTransferred Event Get");
            //};

            //Debug.Log(callerAddr);
        }

        public async void GetTileMapState()
        {
            if (null == this.evmContract)
            {
                Debug.Log("Not signed in!");
                return;
            }

            output result = await this.evmContract.StaticCallDtoTypeOutputAsync<output>("GetTileMapState");
            Debug.Log(result.State);
        }

        public async void SetTileMapState()
        {
            if (null == this.evmContract)
            {
                Debug.Log("Not signed in!");
                return;
            }

            try
            {
                string str = "abcded";
                //await this.evmContract.CallAsync("createUser", str);
                output result = await this.evmContract.CallDtoTypeOutputAsync<output>("users", 0);
                Debug.Log("result: " + result.State);

                //output3 result2 = await this.evmContract.StaticCallDtoTypeOutputAsync<output3>("whoAmI");
                //Debug.Log("result2: " + result2.State);

                //output2 result3 = await this.evmContract.StaticCallDtoTypeOutputAsync<output2>("ping");
                //Debug.Log("result3: " + result3.State);
            }
            catch(Exception ex)
            {
                Debug.Log(ex);
            }
        }

        [FunctionOutput]
        public class output
        {
            [Parameter("string", "state", 1)]
            public string State { get; set; }
        }

        [FunctionOutput]
        public class output2
        {
            [Parameter("uint", "state", 1)]
            public BigInteger State { get; set; }
        }

        [FunctionOutput]
        public class output3
        {
            [Parameter("address", "state", 1)]
            public string State { get; set; }
        }
    }
}