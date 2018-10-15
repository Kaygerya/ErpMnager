using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Entities.Model.Base
{
    public class BaseModel
    {
        public BaseModel()
        {
            Errors = new List<string>();
        }
        public string Id { get; set; }
        public string SuccessMessage { get; set; }
        public List<string> Errors { get; set; }
        public bool IsSuccess { get
            {
                return Errors.Count == 0;
            } }

    }
}
