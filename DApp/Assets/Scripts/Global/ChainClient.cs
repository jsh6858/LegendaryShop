using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Loom.Client;
using Loom.Nethereum.ABI.FunctionEncoding.Attributes;
using Loom.Newtonsoft.Json;
using UnityEngine;
using LegendFramework;

public class ChainClient : MonoBehaviour {
   
    private DAppChainClient client;
    IRpcClient writer;
    IRpcClient reader;
    private EvmContract evmContract;
    
    private void Start()
    {
        Application.runInBackground = true;
        SignIn();

        LegendaryFramework.Make_Hunter();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2)
            && RpcConnectionState.Connected == this.reader.ConnectionState)
        {
            SetTileMapState();
        }

        if (Input.GetKeyDown(KeyCode.F3)
            && RpcConnectionState.Connected == this.reader.ConnectionState)
        {
            GetTileMapState();
        }
    }

    public async void SignIn()
    {
        //byte[] privateKey = Encoding.UTF8.GetBytes(private_Key);
        //byte[] publicKey = Encoding.UTF8.GetBytes(public_Key);
        byte[] privateKey = CryptoUtils.GeneratePrivateKey();   //64개의 byte배열
        byte[] publicKey = CryptoUtils.PublicKeyFromPrivateKey(privateKey); //32개의 byte배열
        Address callerAddr = Address.FromPublicKey(publicKey);

        writer = RpcClientFactory.Configure()
            .WithLogger(Debug.unityLogger)
            .WithWebSocket("ws://172.30.1.2:46657/websocket")
            .Create();
        /*172.30.1.41*/
        reader = RpcClientFactory.Configure()
            .WithLogger(Debug.unityLogger)
            .WithWebSocket("ws://172.30.1.2:9999/queryws")
            .Create();

        client = new DAppChainClient(writer, reader)
        {
            Logger = Debug.unityLogger
        };

        client.TxMiddleware = new TxMiddleware(new ITxMiddlewareHandler[]
        {
            new NonceTxMiddleware(publicKey, client),
            new SignedTxMiddleware(privateKey)
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

        string str = "Test";
        await this.evmContract.CallAsync("SetTileMapState", str);
        //await this.evmContract.CallAsync("set", 0);
    }

    [FunctionOutput]
    public class output
    {
        [Parameter("string", "state", 1)]
        public string State { get; set; }
    }
}