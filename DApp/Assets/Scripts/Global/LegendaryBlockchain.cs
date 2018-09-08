using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loom.Client;
using Loom.Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Text;

namespace LegendFramework
{
    public class LegendaryClient
    {
        static DAppChainClient client;
        static IRpcClient writer;
        static IRpcClient reader;
        static EvmContract evmContract;

        static byte[] PrivateKey;
        static byte[] PublicKey;
        
        public static async void SignIn()
        {
            string ipAddress = "172.30.1.2";

            if (1 == PlayerPrefs.GetInt("PrivateKeyExist"))
            {
                PrivateKey = new byte[64];
                PrivateKey = Encoding.UTF8.GetBytes(PlayerPrefs.GetString("PrivateKey"));
            }
            else
            {
                PrivateKey = CryptoUtils.GeneratePrivateKey();
                PlayerPrefs.SetInt("PrivateKeyExist", 1);
                PlayerPrefs.SetString("PrivateKey", Encoding.UTF8.GetString(PrivateKey));
            }
            if (1 == PlayerPrefs.GetInt("PublicKeyExist"))
            {
                PublicKey = new byte[32];
                PublicKey = Encoding.UTF8.GetBytes(PlayerPrefs.GetString("PublicKey"));
            }
            else
            {
                PublicKey = CryptoUtils.PublicKeyFromPrivateKey(PrivateKey);
                PlayerPrefs.SetInt("PublicKeyExist", 1);
                PlayerPrefs.SetString("PublicKey", Encoding.UTF8.GetString(PublicKey));
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

            string abi = "[{\"constant\":false," +
                  "\"inputs\":[{\"name\":\"_tileState\",\"type\":\"string\"}],\"name\":\"SetTileMapState\"," +
                  "\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"GetTileMapState\",\"outputs\":[{\"name\":\"\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"name\":\"state\",\"type\":\"string\"}],\"name\":\"OnTestUpdate\",\"type\":\"event\"}]\r\n";


            Address contractAddr = await client.ResolveContractAddressAsync("TilesChain");

            //abi = "[{\"constant\":false,\"inputs\":[{\"name\":\"_value\",\"type\":\"uint256\"}],\"name\":\"set\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"get\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"name\":\"_value\",\"type\":\"uint256\"}],\"name\":\"NewValueSet\",\"type\":\"event\"}]";
            evmContract = new EvmContract(client, contractAddr, callerAddr, abi);
            evmContract.EventReceived += (sender, e) =>
            {
                if (e.EventName == "OnTestUpdate")
                    Debug.Log("OnTestUpdate Event Get");

                if (e.EventName == "NewValueSet")
                    Debug.Log("NewValueSet Event Get");

            };
        }
    }
}