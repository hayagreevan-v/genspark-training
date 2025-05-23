using ProxyDesignPattern_SecureFileManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ProxyDesignPattern_SecureFileManagement.Models
{
    public class ProxyFile : IFile
    {
        private readonly IFile _file;
        private readonly Logger _logger;

        public ProxyFile(IFile file, Logger logger)
        {
            _file = file;
            _logger = logger;
        }

        public string Filepath { get; } = string.Empty;

        public string Read(User user)
        {

            if (user.UserRole == Role.Admin)
            {
                _logger.Log($"ADMIN\t {user.Name}\t SUCCESS\t"+DateTime.Now);
                return "[Access Granted] Reading sensitive file content...\n" + _file.Read(user);
            }
            else if(user.UserRole == Role.User)
            {
                _logger.Log($"USER\t {user.Name}\t RESTRICTED\t" + DateTime.Now);

                FileInfo fileInfo = new FileInfo(_file.Filepath);
                string str = "";
                str += "File Name: " + fileInfo.Name + "\n";
                str += "File Extension: " + fileInfo.Extension +"\n";
                str += "File Size (bytes): " + fileInfo.Length +"\n";
                str += "Creation Time: " + fileInfo.CreationTime +"\n";
                str += "Last Access Time: " + fileInfo.LastAccessTime +"\n";
                str += "Last Write Time: " + fileInfo.LastWriteTime +"\n";
                str += "Is Read-Only: " + fileInfo.IsReadOnly +"\n";
                return "File Description: \n"+ str+"\nUser is not allowed to Read!";

            }
            else
            {
                _logger.Log($"GUEST\t {user.Name}\t DENIED\t" + DateTime.Now);
                return "[Access Denied] You do not have permission to read this file.";
            }

        }
    }
}
