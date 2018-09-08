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
        //string ipaddress = "127.0.0.1";
        string ipaddress = "172.30.1.14";
        //byte[] privateKey = Encoding.UTF8.GetBytes(private_Key);
        //byte[] publicKey = Encoding.UTF8.GetBytes(public_Key);
        byte[] privateKey = CryptoUtils.GeneratePrivateKey();   //64개의 byte배열
        byte[] publicKey = CryptoUtils.PublicKeyFromPrivateKey(privateKey); //32개의 byte배열
        Address callerAddr = Address.FromPublicKey(publicKey);

        writer = RpcClientFactory.Configure()
            .WithLogger(Debug.unityLogger)
            //.WithWebSocket("ws://127.0.0.1:46657/websocket")
            .WithWebSocket("ws://" + ipaddress + ":46657/websocket")
            .Create();
        /*172.30.1.41*/
        reader = RpcClientFactory.Configure()
            .WithLogger(Debug.unityLogger)
            //.WithWebSocket("ws://127.0.0.1:9999/queryws")
            .WithWebSocket("ws://"+ ipaddress+":9999/queryws")
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

        string abi = "[{\"constant\":false,\"inputs\":[{\"name\":\"_tileState\",\"type\":\"string\"}],\"name\":\"SetTileMapState\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"GetTileMapState\",\"outputs\":[{\"name\":\"\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"name\":\"state\",\"type\":\"string\"}],\"name\":\"OnTileMapStateUpdate\",\"type\":\"event\"}]";
        //Address contractAddr = await client.ResolveContractAddressAsync("TilesChain");
        var contractAddr = Address.FromHexString("0x6fbd04200Edc1579BDe307A76E1aAc6ef96ACE5A");

        evmContract = new EvmContract(client, contractAddr, callerAddr, abi);
        evmContract.EventReceived += (sender, e) =>
        {
            if (e.EventName == "OnTileMapStateUpdate")
                Debug.Log("OnTileMapStateUpdate Event Get");

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