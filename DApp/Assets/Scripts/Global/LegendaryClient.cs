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
        public EvmContract evmContract;

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
            string abi = "[{\"constant\":true,\"inputs\":[],\"name\":\"userCount\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"qid\",\"type\":\"uint256\"},{\"name\":\"periodTime\",\"type\":\"uint256\"}],\"name\":\"createQuest\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"getReadyQuestCount\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"questSlotId\",\"type\":\"uint256\"}],\"name\":\"deleteQuest\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"quest1\",\"outputs\":[{\"name\":\"inReady\",\"type\":\"bool\"},{\"name\":\"inRunning\",\"type\":\"bool\"},{\"name\":\"qid\",\"type\":\"uint256\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"endTime\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"modulus\",\"type\":\"uint256\"}],\"name\":\"rand\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"dummy\",\"outputs\":[{\"name\":\"inReady\",\"type\":\"bool\"},{\"name\":\"inRunning\",\"type\":\"bool\"},{\"name\":\"qid\",\"type\":\"uint256\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"endTime\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"nowTime\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"users\",\"outputs\":[{\"name\":\"username\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"username\",\"type\":\"string\"}],\"name\":\"createUser\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"quest8\",\"outputs\":[{\"name\":\"inReady\",\"type\":\"bool\"},{\"name\":\"inRunning\",\"type\":\"bool\"},{\"name\":\"qid\",\"type\":\"uint256\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"endTime\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"ping\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"pure\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"getUserInfo\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"},{\"name\":\"\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"getRunningQuestCount\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"idx\",\"type\":\"uint256\"}],\"name\":\"showQuest\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"quest11\",\"outputs\":[{\"name\":\"inReady\",\"type\":\"bool\"},{\"name\":\"inRunning\",\"type\":\"bool\"},{\"name\":\"qid\",\"type\":\"uint256\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"endTime\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"runningQuestCount\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"readyQuestCount\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"questSlotId\",\"type\":\"uint256\"}],\"name\":\"resultQuest\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"name\":\"\",\"type\":\"address\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"quest10\",\"outputs\":[{\"name\":\"inReady\",\"type\":\"bool\"},{\"name\":\"inRunning\",\"type\":\"bool\"},{\"name\":\"qid\",\"type\":\"uint256\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"endTime\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"questSlotId\",\"type\":\"uint256\"}],\"name\":\"startQuest\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"quest5\",\"outputs\":[{\"name\":\"inReady\",\"type\":\"bool\"},{\"name\":\"inRunning\",\"type\":\"bool\"},{\"name\":\"qid\",\"type\":\"uint256\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"endTime\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"quest2\",\"outputs\":[{\"name\":\"inReady\",\"type\":\"bool\"},{\"name\":\"inRunning\",\"type\":\"bool\"},{\"name\":\"qid\",\"type\":\"uint256\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"endTime\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"quest9\",\"outputs\":[{\"name\":\"inReady\",\"type\":\"bool\"},{\"name\":\"inRunning\",\"type\":\"bool\"},{\"name\":\"qid\",\"type\":\"uint256\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"endTime\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"getUserCount\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[],\"name\":\"showQuestList\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"quest4\",\"outputs\":[{\"name\":\"inReady\",\"type\":\"bool\"},{\"name\":\"inRunning\",\"type\":\"bool\"},{\"name\":\"qid\",\"type\":\"uint256\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"endTime\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"quest6\",\"outputs\":[{\"name\":\"inReady\",\"type\":\"bool\"},{\"name\":\"inRunning\",\"type\":\"bool\"},{\"name\":\"qid\",\"type\":\"uint256\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"endTime\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"ownerQuestCount\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"quest3\",\"outputs\":[{\"name\":\"inReady\",\"type\":\"bool\"},{\"name\":\"inRunning\",\"type\":\"bool\"},{\"name\":\"qid\",\"type\":\"uint256\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"endTime\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"ownerUid\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"whoAmI\",\"outputs\":[{\"name\":\"\",\"type\":\"address\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"quest0\",\"outputs\":[{\"name\":\"inReady\",\"type\":\"bool\"},{\"name\":\"inRunning\",\"type\":\"bool\"},{\"name\":\"qid\",\"type\":\"uint256\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"endTime\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"quest7\",\"outputs\":[{\"name\":\"inReady\",\"type\":\"bool\"},{\"name\":\"inRunning\",\"type\":\"bool\"},{\"name\":\"qid\",\"type\":\"uint256\"},{\"name\":\"periodTime\",\"type\":\"uint256\"},{\"name\":\"endTime\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"transferOwnership\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"name\":\"success\",\"type\":\"bool\"},{\"indexed\":false,\"name\":\"qid\",\"type\":\"uint256\"},{\"indexed\":false,\"name\":\"questSlotId\",\"type\":\"uint256\"},{\"indexed\":false,\"name\":\"reason\",\"type\":\"string\"}],\"name\":\"NewQuest\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"name\":\"uid\",\"type\":\"uint256\"},{\"indexed\":false,\"name\":\"title\",\"type\":\"string\"}],\"name\":\"NewUser\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"name\":\"questSlotId\",\"type\":\"uint256\"},{\"indexed\":false,\"name\":\"endTime\",\"type\":\"uint256\"}],\"name\":\"StartQuest\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"name\":\"status\",\"type\":\"string\"},{\"indexed\":false,\"name\":\"questSlotId\",\"type\":\"uint256\"},{\"indexed\":false,\"name\":\"qid\",\"type\":\"uint256\"},{\"indexed\":false,\"name\":\"remainTime\",\"type\":\"uint256\"}],\"name\":\"ResultQuest\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"name\":\"success\",\"type\":\"bool\"},{\"indexed\":false,\"name\":\"questSlotId\",\"type\":\"uint256\"}],\"name\":\"DeleteQuest\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"name\":\"idx\",\"type\":\"uint256\"},{\"indexed\":false,\"name\":\"inReady\",\"type\":\"bool\"},{\"indexed\":false,\"name\":\"inRunning\",\"type\":\"bool\"},{\"indexed\":false,\"name\":\"qid\",\"type\":\"uint256\"},{\"indexed\":false,\"name\":\"periodTime\",\"type\":\"uint256\"},{\"indexed\":false,\"name\":\"endTime\",\"type\":\"uint256\"}],\"name\":\"ReportQuest\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"name\":\"previousOwner\",\"type\":\"address\"},{\"indexed\":true,\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"OwnershipTransferred\",\"type\":\"event\"}]";


            Address contractAddr = await client.ResolveContractAddressAsync("Legendary");
            //var contractAddr = Address.FromHexString("0x6fbd04200Edc1579BDe307A76E1aAc6ef96ACE5A");

            evmContract = new EvmContract(client, contractAddr, callerAddr, abi);
            evmContract.EventReceived += this.EventReceivedHandler;
        }

        private void EventReceivedHandler(object sender, EvmChainEventArgs e)
        {
            if ("NewUser" == e.EventName)
            {
                newUserEvent result = e.DecodeEventDto<newUserEvent>();
                Debug.Log("NewUser result: " + result.state + ":" + result.title);
            }
            else if ("NewQuest" == e.EventName)
            {
                // CreateQuest 리퀘스트 시 응답
                newQuestEvent result = e.DecodeEventDto<newQuestEvent>();
                Debug.Log("NewQuest result: " + result.success + ":" + result.qid + ":" + result.questSlotId + ":" + result.reason);
            }
            else if ("StartQuest" == e.EventName)
            {
                // StartQuest 리퀘스트 시 응답
                startQuestEvent result = e.DecodeEventDto<startQuestEvent>();
                Debug.Log("StartQuest result: " + result.questSlotId + ":" + result.endTime);
            }
            else if ("ResultQuest" == e.EventName)
            {
                // ResultQuest 리퀘스트 시 응답
                resultQuestEvent result = e.DecodeEventDto<resultQuestEvent>();
                Debug.Log("ResultQuest result: " + result.status + ":" + result.questSlotId + ":" + result.qid + ":" + result.remainTime);
            }
            else if ("DeleteQuest" == e.EventName)
            {
                // DeleteQuest 리퀘스트 시 응답
                deleteQuestEvent result = e.DecodeEventDto<deleteQuestEvent>();
                Debug.Log("ResultQuest result: " + result.success + ":" + result.questSlotId);
            }
            else if ("ReportQuest" == e.EventName)
            {
                // ShowQuest 리퀘스트 시 응답하
                reportQuestEvent result = e.DecodeEventDto<reportQuestEvent>();
                Debug.Log("ReportQuest result: " + result.idx + ":" + result.isReady + ":" + result.isRunning + ":" + result.qid + ":" + result.periodTime + ":" + result.endTime);
            }
            else
                Debug.Log(e.EventName);
        }

        private bool _checkClient()
        {
            if (null == this.evmContract)
            {
                Debug.Log("Not signed in!");
                return false;
            }

            return true;
        }

        public async void ShowQuest(int slotId)
        {
            if (!_checkClient())
                return;

            try
            {
                output result = await this.evmContract.CallDtoTypeOutputAsync<output>("showQuest", slotId);
            }
            catch(Exception ex)
            {
                Debug.LogWarning(ex);
            }
        }
        public async void CreateQuest(int questId, int time)
        {
            if (!_checkClient())
                return;

            try
            {
                await this.evmContract.CallAsync("createQuest", questId, time);
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
            }
        }
        public async void StartQuest(int slotId)
        {
            if (!_checkClient())
                return;

            try
            {
                await this.evmContract.CallAsync("startQuest", slotId);
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
            }
        }
        public async void ResultQuest(int slotId)
        {
            if (!_checkClient())
                return;

            try
            {
                await this.evmContract.CallAsync("resultQuest", slotId);
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
            }
        }
        public async void DeleteQuest(int slotId)
        {
            if (!_checkClient())
                return;

            try
            {
                await this.evmContract.CallAsync("deleteQuest", slotId);
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
            }
        }

        public async void f6_press()
        {
            if (null == this.evmContract)
            {
                Debug.Log("Not signed in!");
                return;
            }
            //showQuestList
            output result3 = await this.evmContract.CallDtoTypeOutputAsync<output>("showQuest", 0);
            Debug.Log("result3: " + result3.State);
        }
        public async void f5_press()
        {
            if (null == this.evmContract)
            {
                Debug.Log("Not signed in!");
                return;
            }

            //output result = await this.evmContract.StaticCallDtoTypeOutputAsync<output>("GetTileMapState");
            //Debug.Log(result.State);
            await this.evmContract.CallAsync("resultQuest", 1);
        }
        public async void f1_press()
        {
            output2 result = await this.evmContract.StaticCallDtoTypeOutputAsync<output2>("nowTime");
            Debug.Log("nowTime: " + result.State);

            output2 result2 = await this.evmContract.StaticCallDtoTypeOutputAsync<output2>("ping");
            Debug.Log("Ping: " + result2.State);
        }
        public async void f3_press()
        {
            if (null == this.evmContract)
            {
                Debug.Log("Not signed in!");
                return;
            }

            await this.evmContract.CallAsync("startQuest", 1);
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
                await this.evmContract.CallAsync("createQuest", 10, 60);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }

        [FunctionOutput]
        public class output
        {
            [Parameter("uint", "state", 1)]
            public BigInteger State { get; set; }
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

        public class newUserEvent
        {
            [Parameter("uint", "uid", 1)]
            public uint state { get; set; }

            [Parameter("string", "title", 2)]
            public string title { get; set; }
        }

        public class newQuestEvent
        {
            [Parameter("bool", "success", 1)]
            public bool success { get; set; }

            [Parameter("uint", "qid", 2)]
            public uint qid { get; set; }

            [Parameter("uint", "questSlotId", 3)]
            public uint questSlotId { get; set; }

            [Parameter("string", "reason", 4)]
            public string reason { get; set; }
        }

        public class startQuestEvent
        {
            [Parameter("uint", "questSlotId", 1)]
            public uint questSlotId { get; set; }

            [Parameter("uint", "endTime", 2)]
            public uint endTime { get; set; }
        }

        public class resultQuestEvent
        {
            [Parameter("string", "status", 1)]
            public string status { get; set; }

            [Parameter("uint", "questSlotId", 2)]
            public uint questSlotId { get; set; }

            [Parameter("uint", "qid", 3)]
            public uint qid { get; set; }

            [Parameter("uint", "remainTime", 4)]
            public uint remainTime { get; set; }
        }

        public class deleteQuestEvent
        {
            [Parameter("bool", "success", 1)]
            public bool success { get; set; }

            [Parameter("uint", "questSlotId", 2)]
            public uint questSlotId { get; set; }
        }

        public class reportQuestEvent
        {
            [Parameter("uint", "idx", 1)]
            public uint idx { get; set; }

            [Parameter("bool", "inReady", 2)]
            public bool isReady { get; set; }

            [Parameter("bool", "inRunning", 3)]
            public bool isRunning { get; set; }

            [Parameter("uint", "qid", 4)]
            public uint qid { get; set; }

            [Parameter("uint", "periodTime", 5)]
            public uint periodTime { get; set; }

            [Parameter("uint", "endTime", 6)]
            public uint endTime { get; set; }
        }
    }
}