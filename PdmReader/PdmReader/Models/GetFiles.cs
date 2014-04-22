using System;
using System.Collections.Generic;
using System.IO;

namespace PdmReader.Models {
    public static class GetFiles {
        public static IEnumerable<string> GetPdmFiles(string dir) {
            if(!Directory.Exists(dir)) {
                throw new Exception("文件夹不存在！");
            }
            foreach(var file in Directory.GetFileSystemEntries(dir)) {
                if(File.Exists(file)) {
                    yield return file;
                } else {
                    GetPdmFiles(file);
                }
            }
        }
    }
}
