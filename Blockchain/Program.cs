using Ers;

IHashCalculator hashCalculator = new HashCalculator();
IGenesisBlock genesisBlock = new GenesisBlock(hashCalculator);
IBlockchain blockchain = Blockchain.GetInstance(genesisBlock);
IBlockConfirmation blockConfirmation = new BlockConfirmation(genesisBlock);
IRegistrationService registrationService = new RegistrationService();
INotificationService notificationService = new NotificationService(registrationService, blockConfirmation);
IMiningService miningService = new MiningService(hashCalculator, notificationService);
IBlockValidator blockValidator = new BlockValidator();
ITaskAssignmentService taskAssignmentService = new TaskAssignmentService(registrationService,miningService);
IDataHandlingService dataHandlingService = new DataHandlingService(genesisBlock, taskAssignmentService, hashCalculator);

IMiner miner1 = new Miner("Miner 1", miningService, blockValidator, blockConfirmation);
IMiner miner2 = new Miner("Miner 2", miningService, blockValidator, blockConfirmation);
IMiner miner3 = new Miner("Miner 3", miningService, blockValidator, blockConfirmation);
IMiner miner4 = new Miner("Miner 4", miningService, blockValidator, blockConfirmation);

Console.WriteLine("[ REGISTRATION OF MINERS ]\n");
registrationService.RegisterMiner(miner1);
registrationService.RegisterMiner(miner2);
registrationService.RegisterMiner(miner3);
registrationService.RegisterMiner(miner4);

IClient client1 = new Client(123, dataHandlingService);
IClient client2 = new Client(357, dataHandlingService);
IClient client3 = new Client(192, dataHandlingService);

Console.WriteLine("\n\n[ REGISTRATION OF CLIENTS ]\n");
registrationService.RegisterClient(client1);
registrationService.RegisterClient(client2);
registrationService.RegisterClient(client3);

Console.WriteLine("\n\n[ DATA SENDING AND VALIDATION ]\n");
client1.SendData("Some transaction data");
client2.SendData("Some transaction blabla");
client1.SendData("Some blabla data");
client3.SendData("Some transaction example");
client3.SendData("Some some some some");
client2.SendData("Transaction data");


Console.WriteLine("\n[ BLOCKCHAIN STATUS ]\n\n");
foreach (var block in blockchain.GetChain())
{
    Console.WriteLine(block);
}

Console.WriteLine("\n[ REGISTERED MINERS ]\n");
foreach (var miner in registrationService.registeredMiners)
{
    Console.WriteLine(miner);
}

Console.ReadKey();
