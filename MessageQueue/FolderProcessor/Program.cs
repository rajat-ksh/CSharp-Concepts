// See https://aka.ms/new-console-template for more information
using MessageReceiver;
using MessageSender;

var cts = new CancellationTokenSource();
CancellationToken token = cts.Token;
Console.WriteLine("Hello, World!");
string connectionString = "<YOUR-NAMESPACE-CONNECTION-STRING>";
string queueName = "<YOUR-QUEUE-NAME>";
Task sendMessage = Task.Factory.StartNew(() =>
{
    _ = SendMessages(connectionString, queueName);
});

Task reciveMessage = Task.Factory.StartNew(() => ReceiveMessages(connectionString, queueName));

Task.WaitAll(sendMessage, reciveMessage);

Console.WriteLine("Task Completed");
Console.ReadLine();

static async Task<int> SendMessages(string connectionString, string queueName)
{
    string sourceFolderPath = @"..\FolderProcessor\RawData";
    Console.WriteLine("Sending Messages");
    SenderUtility sender = new SenderUtility(connectionString, queueName);
    var message = await sender.SendMessageAsync(sourceFolderPath);
    Console.WriteLine($"messages has been published to the queue. Name of the file{0}", message.Subject);
    return 0;
}

static async void ReceiveMessages(string connectionString, string queueName)
{
    string destinationFolder = @"..\FolderProcessor\ProcessedData";
    Console.WriteLine("Reciving Messages");
    ReceiverUtility reciver = new ReceiverUtility(connectionString, queueName);
    var messageCount = await reciver.ReceiveMessageAsync(destinationFolder);
    Console.WriteLine("All Message proccessed.");
    //Console.WriteLine($"Number of message Processed:- {0}", messageCount);
    Console.WriteLine($"Number of message Processed:-" + messageCount);

}