using System.Security.Cryptography;
using System.Text;

string doorID = "uqwqemis";
int index = 0;
string password1 = "";
string[] password2 = new string[8];
bool processed = false;

do
{
    string input = doorID + index;
    byte[] data = Encoding.ASCII.GetBytes(input);
    byte[] hash = MD5.HashData(data);
    string result = Convert.ToHexString(hash);

    if (args.Length == 0 || args[0] == "-first")
        UpdatePassword1(result);
    else if (args[0] == "-second")
        UpdatePassword2(result);

    index++;
} while (password1.Length != 8 && !processed);

System.Console.WriteLine(password1);
System.Console.WriteLine(string.Join("", password2));

void UpdatePassword1(string result)
{
    if (result.StartsWith("00000"))
        password1 += result[5];
}

void UpdatePassword2(string result)
{
    if (result.StartsWith("00000") && int.TryParse(char.ToString(result[5]), out int index))
    {
        if (index >= password2.Length || password2[index] != null)
            return;

        password2[index] = char.ToString(result[6]);
    }

    if (!password2.Contains(null))
        processed = true;
}