using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    public static class CommonTools
    {
        private static readonly TextEditor copyTool = new TextEditor();

        /// <summary>
        /// 拷贝Hierarchy中的物体路径 - 有根节点
        /// </summary>
        [MenuItem("GameObject/CopyPathToRoot", priority = 1024)]
        static void CopyPath_Root()
        {
            Transform trans = Selection.activeTransform;
            if (null == trans) return;
            string copyText = GetPath(trans);
            copyTool.text = copyText;
            copyTool.SelectAll();
            copyTool.Copy();
        }

        /// <summary>
        /// 拷贝Hierarchy中的物体路径 - 舍弃根节点(比如新手引导步骤表中点击控件路径使用)
        /// </summary>
        [MenuItem("GameObject/CopyPathNoRoot", priority = 1025)]
        static void CopyPath_NoRoot()
        {
            Transform trans = Selection.activeTransform;
            if (null == trans) return;
            string copyText = GetPath(trans);
            string rootName = trans.root.name;
            copyText = copyText.Replace(rootName + "/", "");
            copyTool.text = copyText;
            copyTool.SelectAll();
            copyTool.Copy();
        }

        public static string GetPath(Transform trans)
        {
            if (null == trans) return string.Empty;
            if (null == trans.parent) return trans.name;
            return GetPath(trans.parent) + "/" + trans.name;
        }
    }
}