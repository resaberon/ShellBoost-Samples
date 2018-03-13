﻿using System;
using System.Collections.Generic;
using ShellBoost.Core;
using ShellBoost.Core.WindowsShell;
using ShellBoost.Samples.WebFolder.Api;

namespace ShellBoost.Samples.WebFolder.Folder
{
    public class WebShellFolder : ShellFolder
    {
        // we use a GuidKeyShellItemId for the IdList (it's an IDL wrapper on a Guid)
        public WebShellFolder(ShellFolder parent, Item folder)
            : base(parent, new GuidKeyShellItemId(folder.Id))
        {
            CanCopy = true;
            CanDelete = true;
            CanLink = true;
            CanMove = true;
            CanPaste = true;
            CanRename = true;
            DateModified = folder.LastWriteTimeUtc.ToLocalTime();
            DateCreated = folder.CreationTimeUtc.ToLocalTime();
            Folder = folder;
            DisplayName = folder.Name;
        }

        public Item Folder { get; }

        // same function used for all folders, including root folder
        public static Item[] EnumItems(Guid itemId, bool includeFolders, bool includeItems)
        {
            if (includeFolders && includeItems)
                return Api.Api.GetChildren(itemId);

            if (includeFolders)
                return Api.Api.GetFolders(itemId);

            return Api.Api.GetItems(itemId);
        }

        public override IEnumerable<ShellItem> EnumItems(SHCONTF options)
        {
            foreach (var item in EnumItems(Folder.Id, options.HasFlag(SHCONTF.SHCONTF_FOLDERS), options.HasFlag(SHCONTF.SHCONTF_NONFOLDERS)))
            {
                if (item.Type == Api.ItemType.Folder)
                {
                    yield return new WebShellFolder(this, item);
                }
                else
                {
                    yield return new WebShellItem(this, item);
                }
            }
        }
    }
}