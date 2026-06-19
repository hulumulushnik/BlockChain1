using BlockChainP411NEW.Services;
using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

var walletService = new WalletService();
var vanityService = new VanityWalletService(walletService);

Console.WriteLine("==================================================");
Console.WriteLine(" 🏫 ЧАСТИНА 1: VANITY WALLET GENERATOR (КЛАС) ");
Console.WriteLine("==================================================\n");

string targetPrefix = "777";
Console.WriteLine($"Пошук красивої адреси з префіксом '0x{targetPrefix}'...");

var (myVanityWallet, attempts) = vanityService.MineWallet(targetPrefix);

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine($"[Успіх] Знайдено адресу: {myVanityWallet.Address}");
Console.WriteLine($"[Статистика] Витрачено спроб генерації: {attempts}");
Console.ResetColor();


Console.WriteLine("\n==================================================");
Console.WriteLine(" 🏠 ЧАСТИНА 2: WEB3 АВТОРИЗАЦІЯ ТА ЗАХИСТ (ДІМ) ");
Console.WriteLine("==================================================\n");

string authMessage = "This is my custom wallet!";

Console.WriteLine("--- ТЕСТ 1: Нормальна авторизація (Справжній власник) ---");

byte[] validSignature = walletService.SignMessage(myVanityWallet, authMessage);

bool isAuthSuccess = walletService.VerifyMessage(
    claimedAddress: myVanityWallet.Address,
    publicKey: myVanityWallet.PublicKey,
    message: authMessage,
    signature: validSignature
);

if (isAuthSuccess)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Авторизація успішна.\n");
    Console.ResetColor();
}

Console.WriteLine("--- ТЕСТ 2: Атака підміни публічного ключа (Хакер) ---");
var hackerWallet = walletService.CreateWallet("Hacker");

byte[] hackerSignature = walletService.SignMessage(hackerWallet, authMessage);

Console.WriteLine($"Хакер заявляє, що він власник адреси: {myVanityWallet.Address}");
Console.WriteLine("Він підставляє свій публічний ключ і свій підпис, сподіваючись обдурити систему...");

bool isHackerSuccess = walletService.VerifyMessage(
    claimedAddress: myVanityWallet.Address,
    publicKey: hackerWallet.PublicKey,
    message: authMessage,
    signature: hackerSignature
);

if (!isHackerSuccess)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\n[ЗАХИСТ СПРАЦЮВАВ] Сервер відхилив хакерський запит!");
    Console.ResetColor();
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\n[УВАГА] Хакер зміг обійти авторизацію!");
    Console.ResetColor();
}

Console.ReadLine();