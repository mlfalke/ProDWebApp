using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain.Models
{
    public class Company {
            public string name;
            public List<Permission> permissions;

            public List<Permission> GetAllPermissions (){
                return this.permissions;
            }
            public List<Permission> GetTruePermissions (){
                List<Permission> permissions = new List<Permission>();
                foreach(Permission p in this.permissions){
                    if(p.value){
                        permissions.Add(p);
                    }
                }
                return(permissions);
            }
    }

    public class Permission {
        public string name;
        public bool value;
        }
}