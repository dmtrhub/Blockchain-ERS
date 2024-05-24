using Ers;

SmartContract smartContract = SmartContract.Instance;
Blockchain blockchain = Blockchain.Instance;

Miner miner1 = new Miner("Miner 1");
Miner miner2 = new Miner("Miner 2");
Miner miner3 = new Miner("Miner 3");
Miner miner4 = new Miner("Miner 4");

Console.WriteLine("[ REGISTRATION OF MINERS ]\n");
miner1.RegisterWithSmartContract(smartContract);
miner2.RegisterWithSmartContract(smartContract);
miner3.RegisterWithSmartContract(smartContract);
miner4.RegisterWithSmartContract(smartContract);

Client client1 = new Client(123);
Client client2 = new Client(357);
Client client3 = new Client(192);

Console.WriteLine("\n\n[ REGISTRATION OF CLIENTS ]\n");
smartContract.RegisterClient(client1);
smartContract.RegisterClient(client2);
smartContract.RegisterClient(client3);

Console.WriteLine("\n\n[ DATA SENDING AND VALIDATION ]\n");
client1.SendData(smartContract, "Some transaction data");
client2.SendData(smartContract, "Some transaction blabla");
client1.SendData(smartContract, "Some blabla data");
client3.SendData(smartContract, "Some transaction example");
client3.SendData(smartContract, "Some some some some");
client2.SendData(smartContract, "Transaction data");

Console.WriteLine("\n[ BLOCKCHAIN STATUS ]\n\n");
foreach (var block in blockchain.Chain)
{
    Console.WriteLine(block);
}

Console.WriteLine("\n[ REGISTERED MINERS ]\n");
foreach (var miner in smartContract.registeredMiners)
{
    Console.WriteLine(miner);
}

Console.ReadKey();
