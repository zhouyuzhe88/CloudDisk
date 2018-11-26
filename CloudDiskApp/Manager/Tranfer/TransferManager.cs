using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDiskApp
{
    class TransferManager
    {
        private List<TransferTask> TaskList { get; set; }

        private TransferManager()
        {
            TaskList = new List<TransferTask>();
        }

        private static TransferManager instance = new TransferManager();

        internal static TransferManager Instance
        {
            get
            {
                return instance;
            }
        }

        public void AddTask(TransferTask task)
        {
            TaskList.Add(task);
            UIController.Instance.UpdateTransferList(TaskList);
        }

        public void StartTask(TransferTask task)
        {
            
        }
    }
}
