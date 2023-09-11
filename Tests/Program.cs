using System.Text;
using BLL.Services.DataCoder;

var key = "peeubdnabceybxte";
var keyBytes = Encoding.UTF8.GetBytes(key);
var byteCoder = new ByteDataCoder(keyBytes);
var coder = new GuidStringCoder(new ByteStringCoder(), byteCoder);

while (true)
{
    var originalBytes = Guid.NewGuid();

    var encryptedBytes = coder.Encrypt(originalBytes);

    var decryptedBytes = coder.Decrypt(encryptedBytes);

    Console.WriteLine("Исходные байты: " + originalBytes);
    Console.WriteLine("Закодированные байты: " + encryptedBytes);
    Console.WriteLine("Декодированные байты: " + decryptedBytes);

    if (originalBytes != decryptedBytes)
        throw new AggregateException();
}

Console.ReadLine();
