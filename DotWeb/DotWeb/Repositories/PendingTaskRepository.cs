using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotWeb.Models;
using System.Data.SqlClient;
using System.Data;

namespace DotWeb.Repositories
{
    public class PendingTaskRepository
    {
        public static bool ChangePendingTask(int Id)
        {
            bool exe = false;
            DateTime date = DateTime.Now;
            try
            {
                using (AppDb context = new AppDb())
                {
                    var update = context.PendingTasks.Where(a=> a.Id == Id).FirstOrDefault();
                    update.Pending = true;
                    context.SaveChanges();
                    exe = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return exe;
        }
        
        public static bool SavePendingTask(string userName, string task, string link)
        {
            bool exe = false;
            DateTime date = DateTime.Now;
            int userID = UserRepository.RetrieveUserIdByUserName(userName);

            try
            {
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(task) || string.IsNullOrEmpty(link))
                    return exe;

                using (AppDb context = new AppDb())
                {
                    PendingTasks data = new PendingTasks()
                    {
                        UserId = userID,
                        Task = task,
                        Link = link,
                        Date = date,
                        Pending = false
                    };

                    context.PendingTasks.Add(data);
                    context.SaveChanges();
                    exe = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return exe;
        }
        
        public static List<PendingTasks> getPendingTask(string userName)
        {
            //DataTable Data = new DataTable();
            int userID = UserRepository.RetrieveUserIdByUserName(userName);
            using (AppDb context = new AppDb())
            {
                return context.PendingTasks.Where(p => p.Pending == false && p.UserId == userID).ToList();
            }
        }
    }
}
