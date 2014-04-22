using System;
using System.Collections.Generic;

namespace PdmReader.Models.PdmModels {
    /// <summary>  
    /// 关键字  
    /// </summary>  
    public class PdmKey {
        /// <summary>  
        /// 关键字标识  
        /// </summary>  
        public string KeyId {
            get;
            set;
        }

        /// <summary>  
        /// 对象Id  
        /// </summary>  
        public string ObjectID {
            get;
            set;
        }

        /// <summary>  
        /// Key名  
        /// </summary>  
        public string Name {
            get;
            set;
        }

        /// <summary>  
        /// Key代码,对应数据库中的Key.  
        /// </summary>  
        public string Code {
            get;
            set;
        }

        /// <summary>  
        /// 创建日期  
        /// </summary>  
        public DateTime CreationDate {
            get;
            set;
        }

        /// <summary>  
        /// 创建人  
        /// </summary>  
        public string Creator {
            get;
            set;
        }

        /// <summary>  
        /// 修改日期  
        /// </summary>  
        public DateTime ModificationDate {
            get;
            set;
        }

        /// <summary>  
        /// 修改人  
        /// </summary>  
        public string Modifier {
            get;
            set;
        }

        /// <summary>  
        /// Key涉及的列  
        /// </summary>  
        public IList<ColumnInfo> Columns {
            get;
            private set;
        }

        public void AddColumn(ColumnInfo mColumn) {
            if(Columns == null)
                Columns = new List<ColumnInfo>();
            Columns.Add(mColumn);
        }

        /// <summary>  
        /// Key涉及的列代码，根据辞可访问到列信息.对应列的ColumnId  
        /// </summary>  
        public List<string> ColumnObjCodes {
            get;
            private set;
        }

        public void AddColumnObjCode(string objCode) {
            ColumnObjCodes.Add(objCode);
        }

        private TableInfo _ownerTable = null;

        public PdmKey(TableInfo table) {
            ColumnObjCodes = new List<string>();
            _ownerTable = table;
        }
    }

}