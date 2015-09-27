using Renci.SshNet;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace SshPublisher
{
    internal class Program
    {
        [STAThread()]
        private static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Missing path argument!");
                Console.Read();
                return;
            }

            string filePath = args[0];

            string remoteHostAddress = ConfigurationManager.AppSettings["RemoteHost"];
            string username = ConfigurationManager.AppSettings["Username"];
            string privateKeyPath = ConfigurationManager.AppSettings["PrivateKeyPath"];
            string privateKeyPassphrase = ConfigurationManager.AppSettings["PrivateKeyPassphrase"];
            string destinationPath = ConfigurationManager.AppSettings["RemoteHostDestinationPath"];

            ScpUpload(remoteHostAddress, username, privateKeyPath, privateKeyPassphrase, filePath, destinationPath);
        }

        private static void ManageClipboard(string filePath)
        {
            var urlPrefix = ConfigurationManager.AppSettings["ClipboardUrlPrefix"];
            if (!string.IsNullOrEmpty(urlPrefix))
            {
                Clipboard.SetText(urlPrefix.TrimEnd('/') + "/" + new FileInfo(filePath).Name);
            }
        }

        private static void ScpUpload(string host, string username, string privateKeyPath, string privateKeyPassphrase, string filePath, string destinationPath)
        {
            ScpUpload(host, 22, username, privateKeyPath, privateKeyPassphrase, filePath, destinationPath);
        }

        private static void ScpUpload(string host, int port, string username, string privateKeyPath, string privateKeyPassphrase, string filePath, string destinationFilePath)
        {
            ConnectionInfo connInfo = new ConnectionInfo(host, username, new AuthenticationMethod[] {
                new PrivateKeyAuthenticationMethod(username, new PrivateKeyFile[] {
                    new PrivateKeyFile(privateKeyPath, privateKeyPassphrase)
                })
            });

            using (var scp = new ScpClient(connInfo))
            {
                scp.Connect();
                scp.Upload(new FileInfo(filePath), destinationFilePath);
                scp.Disconnect();
                ManageClipboard(filePath);
            }
        }
    }
}