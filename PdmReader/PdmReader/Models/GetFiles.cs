using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace PdmReader.Models {
    public static class GetFiles {
        /// <summary>
        /// 获取指定文件夹内所有文件
        /// </summary>
        /// <param name="dir">文件夹路径</param>
        /// <param name="searchPattern">模式匹配正则表达式</param>
        /// <returns>返回文件路径</returns>
        public static IEnumerable<string> GetPdmFiles(string dir, string searchPattern) {
            if(!Directory.Exists(dir)) {
                //throw new Exception("文件夹不存在！");
                MessageBox.Show("指定文件夹不存在！");
                yield break;
            }
            foreach(var file in Directory.GetFileSystemEntries(dir, searchPattern, SearchOption.AllDirectories).Where(File.Exists)) {
                yield return file;
            }
        }

        /// <summary>
        /// 获取指定文件夹内所有文件
        /// </summary>
        /// <param name="dir">文件夹路径</param>
        /// <returns>返回文件路径</returns>
        public static IEnumerable<string> GetPdmFiles(string dir) {
            return GetPdmFiles(dir, "*");
        }
    }
}
