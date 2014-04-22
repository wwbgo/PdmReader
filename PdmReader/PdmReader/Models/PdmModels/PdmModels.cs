using System.Collections.Generic;

namespace PdmReader.Models.PdmModels {
    /// <summary>  
    /// PDM实体集合  
    /// </summary>  
    public class PdmModels {
        public PdmModels() {
            Tables = new List<TableInfo>();
            Views = new List<ViewInfo>();
        }
        /// <summary>  
        /// 表集合  
        /// </summary>  
        public IList<TableInfo> Tables {
            get;
            private set;
        }
        /// <summary>  
        /// 视图集合  
        /// </summary>  
        public IList<ViewInfo> Views {
            get;
            private set;
        }
    }
}