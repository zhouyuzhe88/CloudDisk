using System.Collections.Generic;

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
