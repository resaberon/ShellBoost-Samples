﻿using ShellBoost.Core;

namespace ShellBoost.Samples.MultiHost
{
    public class OverviewShellFolderServer : ShellFolderServer
    {
        private RootFolder _root;

        protected override RootShellFolder GetRootFolder(ShellItemIdList idl)
        {
            if (_root == null)
            {
                _root = new RootFolder(this, idl);
            }
            return _root;
        }
    }
}
