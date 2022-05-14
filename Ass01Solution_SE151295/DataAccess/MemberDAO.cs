using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore; 

namespace DataAccess
{ 
    public class MemberDAO {
        
        private static MemberDAO instance = null; 
        private static readonly object instanceLock = new object(); 
        private MemberDAO() { } 
        public static MemberDAO Instance
        { 
            get{ 
                lock (instanceLock) 
                { 
                    if (instance == null){
                        instance = new MemberDAO();
                    }
                    return Instance;
                }
            }
        }
    }
}

 
